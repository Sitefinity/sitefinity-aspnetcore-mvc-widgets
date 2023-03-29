using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Progress.Sitefinity.AspNetCore.Configuration;
using Progress.Sitefinity.AspNetCore.Models;
using Progress.Sitefinity.AspNetCore.RestSdk;
using Progress.Sitefinity.AspNetCore.Widgets.ViewComponents.Common;
using Progress.Sitefinity.RestSdk;
using Progress.Sitefinity.RestSdk.Clients.Pages.Dto;
using Progress.Sitefinity.RestSdk.OData;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.ChangePassword
{
    /// <inheritdoc/>
    public class ChangePasswordModel : IChangePasswordModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChangePasswordModel"/> class.
        /// </summary>
        /// <param name="config">Sitefinity configuration settings.</param>
        /// <param name="restService">The client for Sitefinity web services.</param>
        /// <param name="styles">The html classes for styling provider.</param>
        /// <param name="httpContextAccessor">The http context accessor.</param>
        public ChangePasswordModel(ISitefinityConfig config, IODataRestClient restService, IStyleClassesProvider styles, IHttpContextAccessor httpContextAccessor)
        {
            this.config = config;
            this.restService = restService;
            this.styles = styles;
            this.httpContextAccessor = httpContextAccessor;
        }

        /// <inheritdoc/>
        public async Task<ChangePasswordViewModel> InitializeViewModel(ChangePasswordEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var viewModel = new ChangePasswordViewModel()
            {
                ChangePasswordHandlerPath = $"/{this.config.WebServicePath}/ChangePassword",
                Attributes = entity.Attributes,
            };

            viewModel.Labels.Header = entity.Header;
            viewModel.Labels.OldPassword = entity.CurrentPassword;
            viewModel.Labels.NewPassword = entity.NewPassword;
            viewModel.Labels.RepeatPassword = entity.ConfirmPassword;
            viewModel.Labels.SubmitButtonLabel = entity.SubmitButtonLabel;
            viewModel.Labels.LoginFirstMessage = entity.LoginFirstMessage;
            viewModel.Labels.ValidationRequiredMessage = entity.ValidationRequiredMessage;
            viewModel.Labels.ValidationMismatchMessage = entity.ValidationMismatchMessage;
            viewModel.Labels.ExternalProviderMessageFormat = entity.ExternalProviderMessageFormat;
            viewModel.VisibilityClasses = this.styles.StylingConfig.VisibilityClasses;
            viewModel.InvalidClass = this.styles.StylingConfig.InvalidClass;

            viewModel.PostPasswordChangeAction = entity.PostPasswordChangeAction;

            var margins = this.styles.GetMarginsClasses(entity);
            viewModel.CssClass = (entity.CssClass + " " + margins.Trim()).Trim();

            this.httpContextAccessor.HttpContext.DisableCache();

            var user = await this.restService.Users().GetCurrentUser();
            viewModel.ExternalProviderName = user?.ExternalProviderName;

            if (entity.PostPasswordChangeAction == PostPasswordChangeAction.RedirectToPage && entity.PostPasswordChangeRedirectPage?.Content?[0]?.Variations?.Length != 0)
            {
                var pageNodes = await this.restService.GetItems<PageNodeDto>(entity.PostPasswordChangeRedirectPage, new GetAllArgs() { Fields = new[] { nameof(PageNodeDto.ViewUrl) } });
                var items = pageNodes.Items;
                if (items.Count == 1)
                    viewModel.RedirectUrl = items[0].ViewUrl;
            }
            else
            {
                viewModel.PostPasswordChangeMessage = entity.PostPasswordChangeMessage;
            }

            return viewModel;
        }

        private readonly ISitefinityConfig config;
        private readonly IODataRestClient restService;
        private readonly IStyleClassesProvider styles;
        private readonly IHttpContextAccessor httpContextAccessor;
    }
}
