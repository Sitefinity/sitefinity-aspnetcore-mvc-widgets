using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Progress.Sitefinity.AspNetCore.Configuration;
using Progress.Sitefinity.AspNetCore.RestSdk;
using Progress.Sitefinity.AspNetCore.Web;
using Progress.Sitefinity.AspNetCore.Widgets.Models.ResetPassword.Dto;
using Progress.Sitefinity.AspNetCore.Widgets.ViewComponents.Common;
using Progress.Sitefinity.RestSdk;
using Progress.Sitefinity.RestSdk.Clients.Pages.Dto;
using Progress.Sitefinity.RestSdk.OData;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.ResetPassword
{
    /// <inheritdoc/>
    public class ResetPasswordModel : IResetPasswordModel
    {
        private const string PasswordRecoveryQueryStringKey = "vk";

        private readonly ISitefinityConfig config;
        private readonly IODataRestClient restService;
        private readonly IStyleClassesProvider styles;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IRenderContext renderContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResetPasswordModel"/> class.
        /// </summary>
        /// <param name="config">Sitefinity configuration settings.</param>
        /// <param name="renderContext">The render context.</param>
        /// <param name="restService">The client for Sitefinity web services.</param>
        /// <param name="httpContextAccessor">The http context accessor.</param>
        /// <param name="styles">The html classes for styling provider.</param>
        public ResetPasswordModel(
            ISitefinityConfig config,
            IRenderContext renderContext,
            IODataRestClient restService,
            IHttpContextAccessor httpContextAccessor,
            IStyleClassesProvider styles)
        {
            this.config = config;
            this.renderContext = renderContext;
            this.restService = restService;
            this.httpContextAccessor = httpContextAccessor;
            this.styles = styles;
        }

        /// <inheritdoc/>
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Intended. Security reasons.")]
        public async Task<ResetPasswordViewModel> InitializeViewModel(ResetPasswordEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var viewModel = new ResetPasswordViewModel();
            viewModel.MembershipProviderName = entity.MembershipProviderName;
            viewModel.ResetUserPasswordHandlerPath = $"/{this.config.WebServicePath}/ResetUserPassword";
            viewModel.Attributes = entity.Attributes;
            viewModel.Labels.ResetPasswordHeader = entity.ResetPasswordHeader;
            viewModel.Labels.NewPasswordLabel = entity.NewPasswordLabel;
            viewModel.Labels.RepeatNewPasswordLabel = entity.RepeatNewPasswordLabel;
            viewModel.Labels.SecurityQuestionLabel = entity.SecurityQuestionLabel;
            viewModel.Labels.SaveButtonLabel = entity.SaveButtonLabel;
            viewModel.Labels.BackLinkLabel = entity.BackLinkLabel;
            viewModel.Labels.SuccessMessage = entity.SuccessMessage;
            viewModel.Labels.ErrorMessage = entity.ErrorMessage;
            viewModel.Labels.AllFieldsAreRequiredErrorMessage = entity.AllFieldsAreRequiredErrorMessage;
            viewModel.Labels.PasswordsMismatchErrorMessage = entity.PasswordsMismatchErrorMessage;

            viewModel.SendResetPasswordEmailHandlerPath = $"/{this.config.WebServicePath}/SendResetPasswordEmail";
            viewModel.Labels.ForgottenPasswordHeader = entity.ForgottenPasswordHeader;
            viewModel.Labels.EmailLabel = entity.EmailLabel;
            viewModel.Labels.ForgottenPasswordLinkMessage = entity.ForgottenPasswordLinkMessage;
            viewModel.Labels.ForgottenPasswordSubmitMessage = entity.ForgottenPasswordSubmitMessage;
            viewModel.Labels.SendButtonLabel = entity.SendButtonLabel;
            viewModel.Labels.BackLinkLabel = entity.BackLinkLabel;
            viewModel.Labels.ForgottenPasswordLabel = entity.ForgottenPasswordLabel;
            viewModel.Labels.InvalidEmailFormatMessage = entity.InvalidEmailFormatMessage;
            viewModel.Labels.FieldIsRequiredMessage = entity.FieldIsRequiredMessage;
            viewModel.VisibilityClasses = this.styles.StylingConfig.VisibilityClasses;
            viewModel.InvalidClass = this.styles.StylingConfig.InvalidClass;

            if (entity.LoginPage?.Content?[0]?.Variations?.Length != 0)
            {
                var pageNodes = await this.restService.GetItems<PageNodeDto>(entity.LoginPage, new GetAllArgs() { Fields = new[] { nameof(PageNodeDto.ViewUrl) } });
                var items = pageNodes.Items;
                if (items.Count == 1)
                    viewModel.LoginPageLink = items[0].ViewUrl;
            }

            this.httpContextAccessor.HttpContext.AddVaryByQueryParams(PasswordRecoveryQueryStringKey);
            if (this.IsResetPasswordRequest())
            {
                viewModel.IsResetPasswordRequest = true;
                this.httpContextAccessor.HttpContext.DisableCache();

                try
                {
                    var resetPasswordModel = await this.restService.ExecuteUnboundAction<ResetPasswordModelDto>(new BoundActionArgs()
                    {
                        Name = "Default.GetResetPasswordModel",
                        Data = new
                        {
                            securityToken = this.httpContextAccessor.HttpContext.Request.QueryString.Value,
                        },
                    });

                    viewModel.RequiresQuestionAndAnswer = resetPasswordModel.RequiresQuestionAndAnswer;
                    viewModel.SecurityQuestion = resetPasswordModel.SecurityQuestion;
                }
                catch (Exception)
                {
                    // In terms of security, if there is some error with the user get, we display common error message to the user.
                    viewModel.Error = true;
                }
            }
            else
            {
                if (this.renderContext.IsLive)
                {
                    var request = this.httpContextAccessor.HttpContext.Request;
                    viewModel.ResetPasswordUrl = $"{request.Scheme}://{request.Host}{request.Path}";
                }
            }

            var margins = this.styles.GetMarginsClasses(entity);
            viewModel.CssClass = (entity.CssClass + " " + margins.Trim()).Trim();

            return viewModel;
        }

        private bool IsResetPasswordRequest()
        {
            return this.renderContext.IsLive && this.httpContextAccessor.HttpContext.Request.Query.ContainsKey(PasswordRecoveryQueryStringKey);
        }
    }
}
