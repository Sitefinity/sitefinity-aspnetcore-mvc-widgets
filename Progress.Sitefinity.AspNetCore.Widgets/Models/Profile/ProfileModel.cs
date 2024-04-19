using System;
using System.Linq;
using System.Threading.Tasks;
using Progress.Sitefinity.AspNetCore.Configuration;
using Progress.Sitefinity.AspNetCore.RestSdk;
using Progress.Sitefinity.AspNetCore.Widgets.ViewComponents.Common;
using Progress.Sitefinity.RestSdk;
using Progress.Sitefinity.RestSdk.Clients.Pages.Dto;
using Progress.Sitefinity.RestSdk.Clients.Users.Dto;
using Progress.Sitefinity.RestSdk.OData;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Profile
{
    /// <inheritdoc/>
    public class ProfileModel : IProfileModel
    {
        private ISitefinityConfig config;
        private IODataRestClient restService;
        private IStyleClassesProvider styles;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileModel"/> class.
        /// </summary>
        /// <param name="config">The config.</param>
        /// <param name="restService">The rest service.</param>
        /// <param name="styles">The styles.</param>
        public ProfileModel(ISitefinityConfig config, IODataRestClient restService, IStyleClassesProvider styles)
        {
            this.config = config;
            this.restService = restService;
            this.styles = styles;
        }

        /// <inheritdoc/>
        public async Task<ProfileViewModel> InitializeViewModel(ProfileEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var user = await this.restService.Users().GetCurrentUser();

            var viewModel = new ProfileViewModel()
            {
                UpdateProfileHandlerPath = $"/{this.config.WebServicePath}/users/updateProfile",
                Attributes = entity.Attributes,
            };

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

            viewModel.Id = user.Id;
            viewModel.FirstName = user.FirstName;
            viewModel.LastName = user.LastName;
            viewModel.Email = user.Email;
            viewModel.Nickname = user.Nickname;
            viewModel.About = user.About;
            viewModel.AvatarUrl = user.AvatarUrl;

            viewModel.ReadOnlyFields = user.ReadOnlyFields;
            viewModel.AllowedAvatarFormat = user.AllowedAvatarFormats;

            var margins = this.styles.GetMarginsClasses(entity);
            viewModel.CssClass = (entity.CssClass + " " + margins.Trim()).Trim();

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
    }
}
