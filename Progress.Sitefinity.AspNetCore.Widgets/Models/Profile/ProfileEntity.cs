using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Progress.Sitefinity.AspNetCore.Models;
using Progress.Sitefinity.AspNetCore.Models.Common;
using Progress.Sitefinity.AspNetCore.ViewComponents.AttributeConfigurator.Attributes;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Common;
using Progress.Sitefinity.Renderer.Designers;
using Progress.Sitefinity.Renderer.Designers.Attributes;
using Progress.Sitefinity.Renderer.Entities.Content;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Profile
{
    /// <summary>
    /// The entity for the Profile widget. Contains all of the data persited in the database.
    /// </summary>
    [SectionsOrder(SelectMode, Constants.ContentSectionTitles.DisplaySettings)]
    public class ProfileEntity : IHasMargins<MarginStyle>
    {
        /// <summary>
        /// Gets or sets the view name.
        /// </summary>
        [ViewSelector]
        [ContentSection(Constants.ContentSectionTitles.DisplaySettings, 1)]
        [DisplayName("Profile template")]
        public string SfViewName { get; set; }

        /// <summary>
        /// Gets or sets the margins.
        /// </summary>
        [ContentSection(Constants.ContentSectionTitles.DisplaySettings, 1)]
        [DisplayName("Margins")]
        [TableView("Profile")]
        public MarginStyle Margins { get; set; }

        /// <summary>
        /// Gets or sets Profile modes.
        /// </summary>
        [ContentSection(SelectMode, 1)]
        [DisplayName("Mode")]
        [DataType(customDataType: KnownFieldTypes.RadioChoice)]
        public ProfileViewMode ViewMode { get; set; }

        /// <summary>
        /// Gets or sets the redirect url.
        /// </summary>
        [ContentSection(SelectPages, 1)]
        [DisplayName("")]
        [Content(Type = KnownContentTypes.Pages, AllowMultipleItemsSelection = false)]
        [ConditionalVisibility("{\"conditions\":[{\"fieldName\":\"ReadEditModeAction\",\"operator\":\"Equals\",\"value\":\"RedirectToPage\"}],\"inline\":\"true\"}")]
        public MixedContentContext ReadEditModeRedirectPage { get; set; }

        /// <summary>
        /// Gets or sets the redirect url.
        /// </summary>
        [ContentSection(SelectPages, 1)]
        [DisplayName("")]
        [Content(Type = KnownContentTypes.Pages, AllowMultipleItemsSelection = false)]
        [ConditionalVisibility("{\"conditions\":[{\"fieldName\":\"EditModeAction\",\"operator\":\"Equals\",\"value\":\"RedirectToPage\"}],\"inline\":\"true\"}")]
        public MixedContentContext EditModeRedirectPage { get; set; }

        /// <summary>
        /// Gets or sets post saving action.
        /// </summary>
        [ContentSection(SelectMode, 1)]
        [DisplayName("After saving changes...")]
        [DataType(customDataType: KnownFieldTypes.RadioChoice)]
        [ConditionalVisibility("{\"conditions\":[{\"fieldName\":\"ViewMode\",\"operator\":\"Equals\",\"value\":\"ReadEdit\"}]}")]
        public ReadEditModeAction ReadEditModeAction { get; set; }

        /// <summary>
        /// Gets or sets post saving action.
        /// </summary>
        [ContentSection(SelectMode, 1)]
        [DisplayName("After saving changes...")]
        [DataType(customDataType: KnownFieldTypes.RadioChoice)]
        [ConditionalVisibility("{\"conditions\":[{\"fieldName\":\"ViewMode\",\"operator\":\"Equals\",\"value\":\"Edit\"}]}")]
        public EditModeAction EditModeAction { get; set; }

        /// <summary>
        /// Gets or sets the CSS class of the button.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [DisplayName("CSS class")]
        public string CssClass { get; set; }

        /// <summary>
        /// Gets or sets the link label.
        /// </summary>
        /// <value>
        /// The link label.
        /// </value>
        [DisplayName("Edit profile link label")]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages)]
        [Category(PropertyCategory.Advanced)]
        [DefaultValue("Edit profile")]
        public string EditProfileLinkLabel { get; set; }

        /// <summary>
        /// Gets or sets the header.
        /// </summary>
        /// <value>
        /// The header.
        /// </value>
        [DisplayName("Edit profile header")]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages)]
        [Category(PropertyCategory.Advanced)]
        [DefaultValue("Edit profile")]
        public string EditProfileHeaderLabel { get; set; }

        /// <summary>
        /// Gets or sets the FirstName field.
        /// </summary>
        /// <value>
        /// The FirstName field.
        /// </value>
        [DisplayName("First name field label")]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages)]
        [Category(PropertyCategory.Advanced)]
        [DefaultValue("First name")]
        public string FirstNameLabel { get; set; }

        /// <summary>
        /// Gets or sets the LastName field.
        /// </summary>
        /// <value>
        /// The LastName field.
        /// </value>
        [DisplayName("Last name field label")]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages)]
        [Category(PropertyCategory.Advanced)]
        [DefaultValue("Last name")]
        public string LastNameLabel { get; set; }

        /// <summary>
        /// Gets or sets the Username field.
        /// </summary>
        /// <value>
        /// The Username field.
        /// </value>
        [DisplayName("Username field label")]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages)]
        [Category(PropertyCategory.Advanced)]
        [DefaultValue("Nickname")]
        public string NicknameLabel { get; set; }

        /// <summary>
        /// Gets or sets the About field.
        /// </summary>
        /// <value>
        /// The About field.
        /// </value>
        [DisplayName("About field label")]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages)]
        [Category(PropertyCategory.Advanced)]
        [DefaultValue("About")]
        public string AboutLabel { get; set; }

        /// <summary>
        /// Gets or sets the Email field.
        /// </summary>
        /// <value>
        /// The Email field.
        /// </value>
        [DisplayName("Email field label")]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages)]
        [Category(PropertyCategory.Advanced)]
        [DefaultValue("Email")]
        public string EmailLabel { get; set; }

        /// <summary>
        /// Gets or sets the Password field.
        /// </summary>
        /// <value>
        /// The Password field.
        /// </value>
        [DisplayName("Password field label")]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages)]
        [Category(PropertyCategory.Advanced)]
        [DefaultValue("Password")]
        public string PasswordLabel { get; set; }

        /// <summary>
        /// Gets or sets the Save button label.
        /// </summary>
        /// <value>
        /// The Save button label.
        /// </value>
        [DisplayName("Save button")]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages)]
        [Category(PropertyCategory.Advanced)]
        [DefaultValue("Save changes")]
        public string SaveButtonLabel { get; set; }

        /// <summary>
        /// Gets or sets the Change photo link label.
        /// </summary>
        /// <value>
        /// Change photo link label.
        /// </value>
        [DisplayName("Change photo link label")]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages)]
        [Category(PropertyCategory.Advanced)]
        [DefaultValue("Change photo")]
        public string ChangePhotoLabel { get; set; }

        /// <summary>
        /// Gets or sets the Invalid photo error message.
        /// </summary>
        /// <value>
        /// Invalid photo error message.
        /// </value>
        [DisplayName("Invalid photo error message")]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages)]
        [Category(PropertyCategory.Advanced)]
        [DefaultValue("Select image no larger than {0} B and in one of the following formats {1}.")]
        public string InvalidPhotoErrorMessage { get; set; }

        /// <summary>
        /// Gets or sets the Validation Required Message.
        /// </summary>
        [DisplayName("Required field error message")]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages)]
        [Category(PropertyCategory.Advanced)]
        [DefaultValue("Field is required.")]
        public string ValidationRequiredMessage { get; set; }

        /// <summary>
        /// Gets or sets the Invalid email error message.
        /// </summary>
        /// <value>
        /// Invalid email error message label.
        /// </value>
        [DisplayName("Invalid email error message")]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages)]
        [Category(PropertyCategory.Advanced)]
        [DefaultValue("Invalid email format.")]
        public string InvalidEmailErrorMessage { get; set; }

        /// <summary>
        /// Gets or sets the Change email label.
        /// </summary>
        /// <value>
        /// Change email label.
        /// </value>
        [DisplayName("Change email label")]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages)]
        [Category(PropertyCategory.Advanced)]
        [DefaultValue("To change your email address, you are required to enter your password.")]
        public string ChangeEmailLabel { get; set; }

        /// <summary>
        /// Gets or sets the Invalid password error message.
        /// </summary>
        /// <value>
        /// Change Invalid password error message label.
        /// </value>
        [DisplayName("Invalid password error message")]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages)]
        [Category(PropertyCategory.Advanced)]
        [DefaultValue("Incorrect password.")]
        public string InvalidPasswordErrorMessage { get; set; }

        /// <summary>
        /// Gets or sets the Success notification.
        /// </summary>
        /// <value>
        /// Change Success notification label.
        /// </value>
        [DisplayName("Success notification")]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages)]
        [Category(PropertyCategory.Advanced)]
        [DefaultValue("Your changes are saved.")]
        public string SuccessNotification { get; set; }

        /// <summary>
        /// Gets or sets the Confirm email change title label.
        /// </summary>
        /// <value>
        /// Confirm email change title label.
        /// </value>
        [DisplayName("Confirm email change title")]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages)]
        [Category(PropertyCategory.Advanced)]
        [DefaultValue("Confirm email change")]
        public string ConfirmEmailChangeTitleLabel { get; set; }

        /// <summary>
        /// Gets or sets the Confirm email change description label.
        /// </summary>
        /// <value>
        /// Confirm email change description label.
        /// </value>
        [DisplayName("Confirm email change message")]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages)]
        [Category(PropertyCategory.Advanced)]
        [DefaultValue("To confirm email change for your account a message has been sent to your new email")]
        public string ConfirmEmailChangeDescriptionLabel { get; set; }

        /// <summary>
        /// Gets or sets the Confirm email change title label.
        /// </summary>
        /// <value>
        /// Confirm email change title label.
        /// </value>
        [DisplayName("Expired activation link title")]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages)]
        [Category(PropertyCategory.Advanced)]
        [DefaultValue("Activation link has expired")]
        public string ConfirmEmailChangeTitleExpiredLabel { get; set; }

        /// <summary>
        /// Gets or sets the Confirm email change description label.
        /// </summary>
        /// <value>
        /// Confirm email change description label.
        /// </value>
        [DisplayName("Expired activation link message")]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages)]
        [Category(PropertyCategory.Advanced)]
        [DefaultValue("To access your account resend activation link to {0}.")]
        public string ConfirmEmailChangeDescriptionExpiredLabel { get; set; }

        /// <summary>
        /// Gets or sets the Send activation link label.
        /// </summary>
        /// <value>
        /// Send activation link description label.
        /// </value>
        [DisplayName("Send activation link")]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages)]
        [Category(PropertyCategory.Advanced)]
        [DefaultValue("Send activation link")]
        public string SendActivationLink { get; set; }

        /// <summary>
        /// Gets or sets the Send again activation link label.
        /// </summary>
        /// <value>
        /// Send again activation link description label.
        /// </value>
        [DisplayName("Send activation link label")]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages)]
        [Category(PropertyCategory.Advanced)]
        [DefaultValue("Send again")]
        public string SendAgainActivationLink { get; set; }

        /// <summary>
        /// Gets or sets the Send activation link success title.
        /// </summary>
        /// <value>
        /// Send activation link success title.
        /// </value>
        [DisplayName("Activation link header")]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages)]
        [Category(PropertyCategory.Advanced)]
        [DefaultValue("Please check your email")]
        public string SendConfirmationLinkSuccessTitle { get; set; }

        /// <summary>
        /// Gets or sets the Send activation link success message.
        /// </summary>
        /// <value>
        /// Send activation link description success message.
        /// </value>
        [DisplayName("Activation link label")]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages)]
        [Category(PropertyCategory.Advanced)]
        [DefaultValue("An activation link has been sent to {0}")]
        public string SendConfirmationLinkSuccessMessage { get; set; }

        /// <summary>
        /// Gets or sets the Confirm email change title error label.
        /// </summary>
        /// <value>
        /// Confirm email change title error label.
        /// </value>
        [DisplayName("Activation error title")]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages)]
        [Category(PropertyCategory.Advanced)]
        [DefaultValue("Error has occured")]
        public string ConfirmEmailChangeTitleErrorLabel { get; set; }

        /// <summary>
        /// Gets or sets the Confirm email change description label.
        /// </summary>
        /// <value>
        /// Confirm email change description label.
        /// </value>
        [DisplayName("Activation error message")]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages)]
        [Category(PropertyCategory.Advanced)]
        [DefaultValue("We could not change your email")]
        public string ConfirmEmailChangeDescriptionErrorLabel { get; set; }

        /// <summary>
        /// Gets or sets the attributes for the widget.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [ContentSection(Constants.ContentSectionTitles.Attributes)]
        [DisplayName("Attributes for...")]
        [DataType(customDataType: KnownFieldTypes.Attributes)]
        [LengthDependsOn(null, "", " ", ExtraRecords = "[{\"Name\": \"Profile\", \"Title\": \"Profile\"}]")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Must be able to set in property editor.")]
        public IDictionary<string, IList<AttributeModel>> Attributes { get; set; }

        private const string SelectMode = "Select mode";
        private const string SelectPages = "Select pages";
    }
}
