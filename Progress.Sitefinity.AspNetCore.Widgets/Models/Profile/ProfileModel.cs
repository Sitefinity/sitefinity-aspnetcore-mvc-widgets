using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using Progress.Sitefinity.AspNetCore.Configuration;
using Progress.Sitefinity.AspNetCore.RestSdk;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Profile.Dto;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Registration;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Registration.Dto;
using Progress.Sitefinity.AspNetCore.Widgets.ViewComponents.Common;
using Progress.Sitefinity.RestSdk;
using Progress.Sitefinity.RestSdk.Clients.Pages.Dto;
using Progress.Sitefinity.RestSdk.Clients.Users.Dto;
using Progress.Sitefinity.RestSdk.Dto;
using Progress.Sitefinity.RestSdk.Exceptions;
using Progress.Sitefinity.RestSdk.OData;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Profile
{
    /// <inheritdoc/>
    public class ProfileModel : IProfileModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileModel"/> class.
        /// </summary>
        /// <param name="config">The config.</param>
        /// <param name="restService">The rest service.</param>
        /// <param name="styles">The styles.</param>
        /// <param name="httpContextAccessor">The http context accessor.</param>
        /// <param name="localizer">The localizer.</param>
        public ProfileModel(ISitefinityConfig config, IODataRestClient restService, IStyleClassesProvider styles, IHttpContextAccessor httpContextAccessor, IStringLocalizer<ProfileModel> localizer)
        {
            this.config = config;
            this.restService = restService;
            this.styles = styles;
            this.httpContextAccessor = httpContextAccessor;
            this.localizer = localizer;
        }

        /// <inheritdoc/>
        public virtual async Task<ProfileViewModel> InitializeViewModel(ProfileEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            UserDto user = null;

            var viewModel = new ProfileViewModel()
            {
                UpdateProfileHandlerPath = $"/{this.config.WebServicePath}/users/updateProfile",
                SendAgainActivationLinkUrl = $"/{this.config.WebServicePath}/users/sendAgain",
                Attributes = entity.Attributes,
            };

            var registrationSettingsRequestArgs = new BoundActionArgs()
            {
                Name = "Default.RegistrationSettings",
            };

            var registrationSettings = await this.restService.ExecuteUnboundAction<RegistrationSettingsDto>(registrationSettingsRequestArgs);

            var confirmEmailChangeTitleLabel = string.Empty;
            var confirmEmailChangeDescriptionLabel = string.Empty;

            viewModel.ActivationMethod = registrationSettings.ActivationMethod;
            if (registrationSettings.ActivationMethod == "AfterConfirmation" && this.httpContextAccessor.HttpContext.Request.Query.TryGetValue(EncryptedParam, out var encryptedParam))
            {
                try
                {
                    viewModel.ConfirmEmailChangeRequest = true;

                    var changeMailRequestArgs = new BoundFunctionArgs()
                    {
                        Name = "users/changeEmail",
                    };

                    changeMailRequestArgs.AdditionalQueryParams.Add(EncryptedParam, HttpUtility.UrlEncode(encryptedParam));

                    user = await this.restService.ExecuteUnboundFunction<ODataWrapper<UserDto>>(changeMailRequestArgs).ContinueWith(
                        result =>
                        {
                            return result.Result.Value;
                        });

                    viewModel.Id = user.Id;
                    viewModel.Email = user.Email;
                    confirmEmailChangeTitleLabel = entity.ConfirmEmailChangeTitleLabel;
                    confirmEmailChangeDescriptionLabel = entity.ConfirmEmailChangeDescriptionLabel;
                }
                catch (Exception ex)
                {
                    viewModel.ConfirmEmailChangeError = true;
                    if (ex is AggregateException aggregateException
                        && aggregateException.InnerException is ErrorCodeException errorCodeException
                        && HttpStatusCode.Gone.ToString() == errorCodeException.Code)
                    {
                        var emailChange = JsonConvert.DeserializeObject<EmailChangeDto>(errorCodeException.Message);

                        viewModel.Id = emailChange.Id;
                        viewModel.Email = emailChange.Email;
                        viewModel.ProviderName = emailChange.ProviderName;
                        viewModel.ShowSendAgainActivationLink = true;
                        confirmEmailChangeTitleLabel = entity.ConfirmEmailChangeTitleExpiredLabel;
                        confirmEmailChangeDescriptionLabel = entity.ConfirmEmailChangeDescriptionExpiredLabel;
                    }
                    else
                    {
                        confirmEmailChangeTitleLabel = entity.ConfirmEmailChangeTitleErrorLabel;
                        confirmEmailChangeDescriptionLabel = entity.ConfirmEmailChangeDescriptionErrorLabel;
                    }
                }
            }
            else
            {
                confirmEmailChangeTitleLabel = entity.ConfirmEmailChangeTitleLabel;
                confirmEmailChangeDescriptionLabel = entity.ConfirmEmailChangeDescriptionLabel;
            }

            if (user == null)
            {
                user = await this.restService.Users().GetCurrentUser();
            }

            viewModel.Labels.FirstNameLabel = entity.FirstNameLabel;
            viewModel.Labels.LastNameLabel = entity.LastNameLabel;
            viewModel.Labels.NicknameLabel = entity.NicknameLabel;
            viewModel.Labels.AboutLabel = entity.AboutLabel;
            viewModel.Labels.PasswordLabel = entity.PasswordLabel;
            viewModel.Labels.EditProfileHeader = entity.EditProfileHeaderLabel;
            viewModel.Labels.EditProfileLink = entity.EditProfileLinkLabel;
            viewModel.Labels.EmailLabel = entity.EmailLabel;
            viewModel.Labels.SaveChangesLabel = entity.SaveButtonLabel;
            viewModel.Labels.ChangePhotoLabel = entity.ChangePhotoLabel;
            viewModel.Labels.SuccessNotification = entity.SuccessNotification;
            viewModel.Labels.ValidationRequiredMessage = entity.ValidationRequiredMessage;
            viewModel.Labels.InvalidEmailErrorMessage = entity.InvalidEmailErrorMessage;
            viewModel.Labels.InvalidPhotoErrorMessage = entity.InvalidPhotoErrorMessage;
            viewModel.Labels.InvalidPasswordErrorMessage = entity.InvalidPasswordErrorMessage;
            viewModel.Labels.ChangeEmailLabel = entity.ChangeEmailLabel;
            viewModel.Labels.ConfirmEmailChangeTitleLabel = confirmEmailChangeTitleLabel;
            viewModel.Labels.ConfirmEmailChangeDescriptionLabel = confirmEmailChangeDescriptionLabel;
            viewModel.Labels.SendActivationLinkLabel = entity.SendActivationLink;
            viewModel.Labels.SendConfirmationLinkSuccessTitleLabel = entity.SendConfirmationLinkSuccessTitle;
            viewModel.Labels.SendConfirmationLinkSuccessDescriptionLabel = entity.SendConfirmationLinkSuccessMessage;
            viewModel.Labels.SendAgainActivationLinkLabel = entity.SendAgainActivationLink;
            viewModel.CssClass = entity.CssClass;
            viewModel.Attributes = entity.Attributes;
            viewModel.ViewMode = entity.ViewMode;
            viewModel.EditModeRedirectUrl = await this.GetPageNodeUrl(entity.EditModeRedirectPage);
            viewModel.ReadEditModeRedirectUrl = await this.GetPageNodeUrl(entity.ReadEditModeRedirectPage);

            viewModel.EditModeAction = entity.EditModeAction;
            viewModel.ReadEditModeAction = entity.ReadEditModeAction;

            viewModel.VisibilityClasses = this.styles.StylingConfig.VisibilityClasses;
            viewModel.InvalidClass = this.styles.StylingConfig.InvalidClass;
            viewModel.CustomFields = user.CustomFields;

            if (string.IsNullOrEmpty(viewModel.Id))
            {
                viewModel.Id = user.Id;
            }

            if (string.IsNullOrEmpty(viewModel.Email))
            {
                viewModel.Email = user.Email;
            }

            viewModel.FirstName = user.FirstName;
            viewModel.LastName = user.LastName;
            viewModel.Nickname = user.Nickname;
            viewModel.About = user.About;
            viewModel.AvatarUrl = user.AvatarUrl;

            viewModel.ReadOnlyFields = user.ReadOnlyFields;
            viewModel.AllowedAvatarFormat = user.AllowedAvatarFormats;

            var margins = this.styles.GetMarginsClasses(entity);
            viewModel.CssClass = (entity.CssClass + " " + margins.Trim()).Trim();

            if (!registrationSettings.SmtpConfigured && viewModel.ActivationMethod == "AfterConfirmation")
            {
                viewModel.Warning = this.localizer.GetString("Confirmation email cannot be sent because the system has not been configured to send emails. Configure SMTP settings or contact your administrator for assistance.");
            }

            return viewModel;
        }

        /// <summary>
        /// Gets the PageNodeUrl.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>Returns the url of the page.</returns>
        private async Task<string> GetPageNodeUrl(Sitefinity.Renderer.Entities.Content.MixedContentContext context)
        {
            if (context?.Content?[0]?.Variations?.Length != 0)
            {
                var pageNodes = await this.restService.GetItems<PageNodeDto>(context, new GetAllArgs() { Fields = new[] { nameof(PageNodeDto.ViewUrl) } });

                var items = pageNodes.Items;

                if (items.Count == 1)
                {
                    return items[0].ViewUrl;
                }
            }

            return string.Empty;
        }

        private const string EncryptedParam = "qs";
        private ISitefinityConfig config;
        private IODataRestClient restService;
        private IStyleClassesProvider styles;
        private IHttpContextAccessor httpContextAccessor;
        private IStringLocalizer<ProfileModel> localizer;
    }
}
