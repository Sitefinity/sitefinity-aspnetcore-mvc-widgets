using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Progress.Sitefinity.AspNetCore.RestSdk;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Common;
using Progress.Sitefinity.AspNetCore.Widgets.Models.LoginForm.Dto;
using Progress.Sitefinity.AspNetCore.Widgets.ViewComponents.Common;
using Progress.Sitefinity.RestSdk;
using Progress.Sitefinity.RestSdk.Clients.Pages.Dto;
using Progress.Sitefinity.RestSdk.Dto;
using Progress.Sitefinity.RestSdk.OData;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.LoginForm
{
    /// <inheritdoc/>
    public class LoginFormModel : ILoginFormModel
    {
        private readonly IODataRestClient restService;
        private readonly IStyleClassesProvider styles;
        private readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginFormModel"/> class.
        /// </summary>
        /// <param name="restService">The client for Sitefinity web services.</param>
        /// <param name="styles">The html classes for styling provider.</param>
        /// <param name="httpContextAccessor">The http context accessor.</param>
        public LoginFormModel(IODataRestClient restService, IStyleClassesProvider styles, IHttpContextAccessor httpContextAccessor)
        {
            this.restService = restService;
            this.styles = styles;
            this.httpContextAccessor = httpContextAccessor;
        }

        /// <inheritdoc/>
        public virtual async Task<LoginFormViewModel> InitializeViewModel(LoginFormEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var viewModel = new LoginFormViewModel
            {
                LoginHandlerPath = "/sitefinity/login-handler",
                ExternalLoginHandlerPath = "/sitefinity/external-login-handler",
                RememberMe = entity.RememberMe,
                MembershipProviderName = entity.MembershipProviderName,
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

            viewModel.Labels.EmailLabel = entity.EmailLabel;
            viewModel.Labels.ErrorMessage = entity.ErrorMessage;
            viewModel.Labels.ExternalProvidersHeader = entity.ExternalProvidersHeader;
            viewModel.Labels.ForgottenPasswordLinkLabel = entity.ForgottenPasswordLinkLabel;
            viewModel.Labels.Header = entity.Header;
            viewModel.Labels.NotRegisteredLabel = entity.NotRegisteredLabel;
            viewModel.Labels.PasswordLabel = entity.PasswordLabel;
            viewModel.Labels.RegisterLinkText = entity.RegisterLinkText;
            viewModel.Labels.RememberMeLabel = entity.RememberMeLabel;
            viewModel.Labels.SubmitButtonLabel = entity.SubmitButtonLabel;
            viewModel.Labels.ValidationInvalidEmailMessage = entity.ValidationInvalidEmailMessage;
            viewModel.Labels.ValidationRequiredMessage = entity.ValidationRequiredMessage;
            viewModel.VisibilityClasses = this.styles.StylingConfig.VisibilityClasses;
            viewModel.InvalidClass = this.styles.StylingConfig.InvalidClass;

            if (entity.PostLoginAction == PostLoginAction.RedirectToPage && entity.PostLoginRedirectPage?.Content?[0]?.Variations?.Length != 0)
            {
                var pageNodes = await this.restService.GetItems<PageNodeDto>(entity.PostLoginRedirectPage, new GetAllArgs() { Fields = new[] { nameof(PageNodeDto.ViewUrl) } });
                var items = pageNodes.Items;
                if (items.Count == 1)
                    viewModel.RedirectUrl = items[0].ViewUrl;
            }

            if (entity.RegistrationPage?.Content?[0]?.Variations?.Length != 0)
            {
                var pageNodes = await this.restService.GetItems<PageNodeDto>(entity.RegistrationPage, new GetAllArgs() { Fields = new[] { nameof(PageNodeDto.ViewUrl) } });
                var items = pageNodes.Items;
                if (items.Count == 1)
                    viewModel.RegistrationLink = items[0].ViewUrl;
            }

            if (entity.ResetPasswordPage?.Content?[0]?.Variations?.Length != 0)
            {
                var pageNodes = await this.restService.GetItems<PageNodeDto>(entity.ResetPasswordPage, new GetAllArgs() { Fields = new[] { nameof(PageNodeDto.ViewUrl) } });
                var items = pageNodes.Items;
                if (items.Count == 1)
                    viewModel.ForgottenPasswordLink = items[0].ViewUrl;
            }

            this.httpContextAccessor.HttpContext.AddVaryByQueryParams(ExternalLoginBase.ErrorQueryKey);
            if (viewModel.IsError(this.httpContextAccessor.HttpContext))
            {
                this.httpContextAccessor.HttpContext.DisableCache();
            }

            var margins = this.styles.GetMarginsClasses(entity);
            viewModel.CssClass = (entity.CssClass + " " + margins.Trim()).Trim();
            return viewModel;
        }
    }
}
