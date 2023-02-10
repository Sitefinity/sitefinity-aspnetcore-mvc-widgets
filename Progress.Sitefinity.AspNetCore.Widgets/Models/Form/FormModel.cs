using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Progress.Sitefinity.AspNetCore.Configuration;
using Progress.Sitefinity.AspNetCore.Models;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Progress.Sitefinity.AspNetCore.Web;
using Progress.Sitefinity.AspNetCore.Widgets.ViewComponents.Common;
using Progress.Sitefinity.Clients.LayoutService.Dto;
using Progress.Sitefinity.Renderer.Contracts.Forms;
using Progress.Sitefinity.RestSdk;
using Progress.Sitefinity.RestSdk.Clients.Forms;
using Progress.Sitefinity.RestSdk.Clients.LayoutEditor;
using Progress.Sitefinity.RestSdk.Clients.Pages.Dto;
using Progress.Sitefinity.RestSdk.Exceptions;
using Progress.Sitefinity.RestSdk.OData;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Form
{
    /// <summary>
    /// The model for the Form widget.
    /// </summary>
    public class FormModel : IFormModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormModel"/> class.
        /// </summary>
        /// <param name="restService">The HTTP client.</param>
        /// <param name="requestContext">The request context.</param>
        /// <param name="styles">The style classes provider.</param>
        /// <param name="renderContext">The render context.</param>
        /// <param name="localizer">The localizer.</param>
        /// <param name="viewComponentTreeBuilder">The view component tree builder.</param>
        /// <param name="sfConfig">Sitefinity configuration settings.</param>
        public FormModel(
            IODataRestClient restService,
            IRequestContext requestContext,
            IStyleClassesProvider styles,
            IViewComponentTreeBuilder viewComponentTreeBuilder,
            IRenderContext renderContext,
            IStringLocalizer<FormModel> localizer,
            ISitefinityConfig sfConfig)
        {
            this.restService = restService;
            this.styles = styles;
            this.viewComponentTreeBuilder = viewComponentTreeBuilder;
            this.renderContext = renderContext;
            this.localizer = localizer;
            this.sfConfig = sfConfig;
            this.requestContext = requestContext;
        }

        /// <inheritdoc/>
        public virtual async Task<FormViewModel> InitializeViewModel(FormEntity entity, IQueryCollection query)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            if (query == null)
                throw new ArgumentNullException(nameof(query));

            var viewModel = new FormViewModel();

            if (entity.SelectedItems?.ItemIdsOrdered?.Length == 1)
            {
                var formDto = await this.restService.Forms().GetItem<FormDto>(new GetItemArgs()
                {
                    Id = entity.SelectedItems.ItemIdsOrdered[0],
                    Provider = entity.SelectedItems.Content[0].Variations[0].Source,
                });

                var queryParams = new Dictionary<string, string>();
                if (query.ContainsKey("sf-content-action"))
                {
                    viewModel.SkipDataSubmission = true;
                    queryParams = query.ToDictionary(x => x.Key, y => HttpUtility.UrlEncode(y.Value.ToString()));
                }

                if (!this.renderContext.IsLive)
                {
                    viewModel.SkipDataSubmission = true;
                }

                Func<Task<PageModelDto>> getFormModel = () =>
                {
                    return this.restService.Forms().GetModel(formDto, queryParams);
                };

                PageModelDto formModel = null;

                try
                {
                    formModel = await getFormModel();
                }
                catch (ErrorCodeException err)
                {
                    if (err.Code == ErrorCodes.NotFound && this.renderContext.IsEdit)
                    {
                        queryParams.Add(Constants.QueryParams.Action, Constants.ActionValues.Edit);
                        formModel = await getFormModel();
                        viewModel.Warning = this.localizer.GetString("This form is a Draft and will not be displayed on the site until you publish the form.");
                    }
                    else
                    {
                        throw;
                    }
                }

                var args = new BuildModelArgs();
                viewModel.FormModel = this.viewComponentTreeBuilder.Build(formModel, args);
                viewModel.Rules = this.GetFormRulesViewModel(formDto);

                var margins = this.styles.GetMarginsClasses(entity);
                viewModel.CssClass = (entity.CssClass + " " + margins).Trim();
                viewModel.SubmitUrl = $"/forms/submit/{formDto.Name}/{this.requestContext.Culture}?{QueryParamNames.Site}={this.requestContext.Site.Id}&{QueryParamNames.SiteTempFlag}=true";

                if (entity.FormSubmitAction == FormSubmitAction.Redirect)
                {
                    viewModel.CustomSubmitAction = true;
                    var pageDto = await this.restService.Pages().GetItem<PageNodeDto>(new GetItemArgs()
                    {
                        Id = entity.RedirectPage.ItemIdsOrdered[0],
                        Provider = entity.RedirectPage.Content[0].Variations[0].Source,
                    });
                    viewModel.RedirectUrl = pageDto.ViewUrl;
                }
                else if (entity.FormSubmitAction == FormSubmitAction.Message)
                {
                    viewModel.CustomSubmitAction = true;
                    viewModel.SuccessMessage = entity.SuccessMessage;
                }

                viewModel.HiddenFields = string.Join(",", GetHiddenFields(viewModel.FormModel));
                viewModel.Attributes = entity.Attributes;
            }

            viewModel.VisibilityClasses = this.styles.StylingConfig.VisibilityClasses;
            viewModel.InvalidClass = this.styles.StylingConfig.InvalidClass;

            return viewModel;
        }

        private static string[] GetHiddenFields(PageModel formModel)
        {
            var hiddenFields = formModel.AllViewComponentsFlat
                .OfType<IViewComponentContext<IFormFieldContract>>()
                .Where(x => x.Entity.Hidden)
                .Select(x => x.Entity.SfFieldName)
                .ToArray();

            return hiddenFields;
        }

        private string GetFormRulesViewModel(FormDto form)
        {
            if (string.IsNullOrWhiteSpace(form.Rules))
            {
                return form.Rules;
            }

            var actionIndexList = this.formRuleActionsToEncrypt.ToDictionary(p => p, p => 0);
            var rules = JToken.Parse(form.Rules) as JArray;
            foreach (var rule in rules)
            {
                foreach (var action in rule["Actions"] as JArray)
                {
                    var ruleAction = action["Action"].ToObject<FormRuleAction>();
                    if (this.formRuleActionsToEncrypt.Contains(ruleAction))
                    {
                        action["Target"] = string.Concat(string.Format(CultureInfo.InvariantCulture, FormInputValueFormat, ruleAction), actionIndexList[ruleAction]);
                        actionIndexList[ruleAction]++;
                    }
                }
            }

            return JsonConvert.SerializeObject(rules);
        }

        private const string FormInputValueFormat = "sf_{0}_";

        private List<FormRuleAction> formRuleActionsToEncrypt = new List<FormRuleAction>() { FormRuleAction.ShowMessage, FormRuleAction.SendNotification };
        private IODataRestClient restService;
        private IRenderContext renderContext;
        private IStyleClassesProvider styles;
        private IViewComponentTreeBuilder viewComponentTreeBuilder;
        private IStringLocalizer<FormModel> localizer;
        private ISitefinityConfig sfConfig;
        private IRequestContext requestContext;

        [JsonConverter(typeof(StringEnumConverter))]
        private enum FormRuleAction
        {
            /// <summary>
            /// Used to determine if the field will be shown.
            /// </summary>
            Show,

            /// <summary>
            /// Used to determine if the field will be hidden
            /// </summary>
            Hide,

            /// <summary>
            /// Used for skipping steps.
            /// </summary>
            Skip,

            /// <summary>
            /// Used for redirect.
            /// </summary>
            GoTo,

            /// <summary>
            /// Used to show message after submit.
            /// </summary>
            ShowMessage,

            /// <summary>
            /// Used to send notification after submit
            /// </summary>
            SendNotification,
        }
    }
}
