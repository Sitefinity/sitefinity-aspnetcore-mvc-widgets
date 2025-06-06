﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Primitives;
using Progress.Sitefinity.AspNetCore.Configuration;
using Progress.Sitefinity.AspNetCore.RestSdk;
using Progress.Sitefinity.AspNetCore.Web;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Common;
using Progress.Sitefinity.AspNetCore.Widgets.Models.LoginForm.Dto;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Registration.Dto;
using Progress.Sitefinity.AspNetCore.Widgets.ViewComponents.Common;
using Progress.Sitefinity.RestSdk;
using Progress.Sitefinity.RestSdk.Clients.Pages.Dto;
using Progress.Sitefinity.RestSdk.Dto;
using Progress.Sitefinity.RestSdk.Exceptions;
using Progress.Sitefinity.RestSdk.OData;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Registration
{
    /// <inheritdoc/>
    public class RegistrationModel : IRegistrationModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationModel"/> class.
        /// </summary>
        /// <param name="config">The Sitefinity config.</param>
        /// <param name="restService">The client for Sitefinity web services.</param>
        /// <param name="styles">The html classes for styling provider.</param>
        /// <param name="renderContext">The render context.</param>
        /// <param name="httpContextAccessor">The http context accessor.</param>
        /// <param name="localizer">The localizer.</param>
        public RegistrationModel(
            ISitefinityConfig config,
            IODataRestClient restService,
            IStyleClassesProvider styles,
            IRenderContext renderContext,
            IHttpContextAccessor httpContextAccessor,
            IStringLocalizer<RegistrationModel> localizer)
        {
            this.config = config;
            this.restService = restService;
            this.styles = styles;
            this.renderContext = renderContext;
            this.httpContextAccessor = httpContextAccessor;
            this.localizer = localizer;
        }

        /// <inheritdoc/>
        public virtual async Task<RegistrationViewModel> InitializeViewModel(RegistrationEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var currentCulture = CultureInfo.CurrentCulture;

            var viewModel = new RegistrationViewModel()
            {
                RegistrationHandlerPath = $"/{this.config.WebServicePath}/Registration?sf_culture={currentCulture.IetfLanguageTag}",
                ResendConfirmationEmailHandlerPath = $"/{this.config.WebServicePath}/ResendConfirmationEmail?sf_culture={currentCulture.IetfLanguageTag}",
                ExternalLoginHandlerPath = "/sitefinity/external-login-handler",
                Attributes = entity.Attributes,
            };

            if (entity.ExternalProviders != null && entity.ExternalProviders.Any())
            {
                var externalProviders = await this.restService.ExecuteUnboundAction<ODataWrapper<IEnumerable<ExternalProviderItemDto>>>(new BoundActionArgs()
                {
                    Name = "Default.GetExternalProviders",
                });
                viewModel.ExternalProviders = externalProviders.Value.Where(p => entity.ExternalProviders.Contains(p.Name));
            }

            viewModel.Labels.Header = entity.Header;
            viewModel.Labels.FirstNameLabel = entity.FirstNameLabel;
            viewModel.Labels.LastNameLabel = entity.LastNameLabel;
            viewModel.Labels.EmailLabel = entity.EmailLabel;
            viewModel.Labels.PasswordLabel = entity.PasswordLabel;
            viewModel.Labels.RepeatPasswordLabel = entity.RepeatPasswordLabel;
            viewModel.Labels.SecretQuestionLabel = entity.SecretQuestionLabel;
            viewModel.Labels.SecretAnswerLabel = entity.SecretAnswerLabel;
            viewModel.Labels.RegisterButtonLabel = entity.RegisterButtonLabel;
            viewModel.Labels.PleaseCheckYourEmailHeader = entity.PleaseCheckYourEmailHeader;
            viewModel.Labels.PleaseCheckYourEmailMessage = entity.PleaseCheckYourEmailMessage;
            viewModel.Labels.ActivateAccountLink = entity.ActivateAccountLink;
            viewModel.Labels.SendAgainLink = entity.SendAgainLink;
            viewModel.Labels.PleaseCheckYourEmailAnotherMessage = entity.PleaseCheckYourEmailAnotherMessage;
            viewModel.Labels.SuccessHeader = entity.SuccessHeader;
            viewModel.Labels.SuccessLabel = entity.SuccessLabel;
            viewModel.Labels.LoginLabel = entity.LoginLabel;
            viewModel.Labels.LoginLink = entity.LoginLink;
            viewModel.Labels.ExternalProvidersHeader = entity.ExternalProvidersHeader;
            viewModel.Labels.ValidationRequiredMessage = entity.ValidationRequiredMessage;
            viewModel.Labels.ValidationMismatchMessage = entity.ValidationMismatchMessage;
            viewModel.Labels.ValidationInvalidEmailMessage = entity.ValidationInvalidEmailMessage;
            viewModel.VisibilityClasses = this.styles.StylingConfig.VisibilityClasses;
            viewModel.InvalidClass = this.styles.StylingConfig.InvalidClass;

            viewModel.LoginPageUrl = this.GetPageNodeUrl(entity.LoginPage);
            if (this.renderContext.IsLive)
            {
                var request = this.httpContextAccessor.HttpContext.Request;
                viewModel.ActivationPageUrl = $"{request.Scheme}://{request.Host}{request.Path}";
            }

            if (this.IsAccountActivationRequest(out string encryptedEmail))
            {
                this.httpContextAccessor.HttpContext.DisableCache();
                viewModel.IsAccountActivationRequest = true;
                viewModel.Labels.ActivationMessage = entity.ActivationMessage;

                try
                {
                    var requestArgs = new BoundActionArgs()
                    {
                        Name = "Default.AccountActivation",
                    };

                    requestArgs.AdditionalQueryParams.Add(EncryptedParam, HttpUtility.UrlEncode(encryptedEmail));

                    await this.restService.ExecuteUnboundAction(requestArgs);
                }
                catch (ErrorCodeException ex)
                {
                    viewModel.ActivationFailed = true;

                    if (ex.Code.Contains(Enum.GetName(typeof(HttpStatusCode), HttpStatusCode.Gone), StringComparison.OrdinalIgnoreCase))
                    {
                        viewModel.Email = ex.Message;

                        viewModel.Labels.ActivateAccountTitle = entity.ActivationLinkExpiredTitle;
                        viewModel.Labels.ActivateAccountMessage = entity.ActivationLinkExpiredMessage;
                    }
                    else
                    {
                        viewModel.Labels.ActivateAccountTitle = entity.ActivationFailTitle;
                        viewModel.Labels.ActivateAccountMessage = entity.ActivationFailMessage;
                    }
                }
            }
            else
            {
                if (entity.PostRegistrationAction == PostRegistrationAction.RedirectToPage)
                {
                    viewModel.RedirectUrl = this.GetPageNodeUrl(entity.PostRegistrationRedirectPage);
                    viewModel.PostRegistrationAction = PostRegistrationAction.RedirectToPage;
                }

                var requestArgs = new BoundActionArgs()
                {
                    Name = "Default.RegistrationSettings",
                };

                var result = await this.restService.ExecuteUnboundAction<RegistrationSettingsDto>(requestArgs);

                if (!result.RegistrationEnabled)
                {
                    viewModel.Warning = this.localizer.GetString("Registration is disabled. To enable registration on your site go to Administration > Advanced settings > Security > User registration settings, or contact your administrator for assistance.");
                }
                else if (result.ActivationMethod == "AfterConfirmation" && !result.SmtpConfigured)
                {
                    viewModel.Warning = this.localizer.GetString("Confirmation email cannot be sent because the system has not been configured to send emails. Configure SMTP settings or contact your administrator for assistance.");
                }

                viewModel.RequiresQuestionAndAnswer = result.RequiresQuestionAndAnswer;
                viewModel.ActivationMethod = result.ActivationMethod;
            }

            var margins = this.styles.GetMarginsClasses(entity);
            viewModel.CssClass = (entity.CssClass + " " + margins.Trim()).Trim();

            this.httpContextAccessor.HttpContext.AddVaryByQueryParams(ExternalLoginBase.ShowSuccessMessageQueryKey);
            return viewModel;
        }

        private bool IsAccountActivationRequest(out string encryptedParam)
        {
            encryptedParam = null;

            if (this.renderContext.IsLive)
            {
                if (this.httpContextAccessor.HttpContext.Request.Query.TryGetValue(EncryptedParam, out StringValues queryString))
                {
                    encryptedParam = queryString.ToString();
                    return true;
                }
            }

            return false;
        }

        private string GetPageNodeUrl(Sitefinity.Renderer.Entities.Content.MixedContentContext context)
        {
            if (context?.Content?[0]?.Variations?.Length != 0)
            {
                var pageNodes = this.restService.GetItems<PageNodeDto>(context, new GetAllArgs() { Fields = new[] { nameof(PageNodeDto.ViewUrl) } }).Result;

                var items = pageNodes.Items;

                if (items.Count == 1)
                {
                    return items[0].ViewUrl;
                }
            }

            return string.Empty;
        }

        private const string EncryptedParam = "qs";
        private readonly ISitefinityConfig config;
        private readonly IODataRestClient restService;
        private readonly IStyleClassesProvider styles;
        private readonly IRenderContext renderContext;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IStringLocalizer<RegistrationModel> localizer;
    }
}
