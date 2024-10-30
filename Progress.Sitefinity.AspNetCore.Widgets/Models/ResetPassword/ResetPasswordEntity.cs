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

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.ResetPassword
{
    /// <summary>
    /// The entity for the Reset password widget. Contains all of the data persited in the database.
    /// </summary>
    [SectionsOrder(SelectPages, Constants.ContentSectionTitles.DisplaySettings)]
    public class ResetPasswordEntity : IHasMargins<MarginStyle>
    {
        /// <summary>
        /// Gets or sets the login page.
        /// </summary>
        [ContentSection(SelectPages, 1)]
        [DisplayName("Login page")]
        [Description("This is the page where you have dropped login form widget.")]
        [Content(Type = KnownContentTypes.Pages, AllowMultipleItemsSelection = false)]
        public MixedContentContext LoginPage { get; set; }

        /// <summary>
        /// Gets or sets the registration page.
        /// </summary>
        [ContentSection(SelectPages, 1)]
        [DisplayName("Registration page")]
        [Description("This is the page where you have dropped registration form widget.")]
        [Content(Type = KnownContentTypes.Pages, AllowMultipleItemsSelection = false)]
        public MixedContentContext RegistrationPage { get; set; }

        /// <summary>
        /// Gets or sets the view name.
        /// </summary>
        [ViewSelector]
        [ContentSection(Constants.ContentSectionTitles.DisplaySettings, 1)]
        [DisplayName("Reset password template")]
        public string SfViewName { get; set; }

        /// <summary>
        /// Gets or sets the margins.
        /// </summary>
        [ContentSection(Constants.ContentSectionTitles.DisplaySettings, 1)]
        [DisplayName("Margins")]
        [TableView("Reset password")]
        public MarginStyle Margins { get; set; }

        /// <summary>
        /// Gets or sets the CSS class of the button.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [DisplayName("CSS class")]
        public string CssClass { get; set; }

        /// <summary>
        /// Gets or sets the membership provider name.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [DisplayName("Membership Provider")]
        public string MembershipProviderName { get; set; }

        /// <summary>
        /// Gets or sets the reset password header.
        /// </summary>
        /// <value>
        /// The reset password header.
        /// </value>
        [DisplayName("Reset password header")]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages)]
        [Category(PropertyCategory.Advanced)]
        [DefaultValue("Reset password")]
        public string ResetPasswordHeader { get; set; }

        /// <summary>
        /// Gets or sets the new password label.
        /// </summary>
        /// <value>
        /// The new password label.
        /// </value>
        [DisplayName("Password field label")]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages)]
        [Category(PropertyCategory.Advanced)]
        [DefaultValue("New password")]
        public string NewPasswordLabel { get; set; }

        /// <summary>
        /// Gets or sets the repeat password field label.
        /// </summary>
        /// <value>
        /// The repeat password field label.
        /// </value>
        [DisplayName("Repeat password field label")]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages)]
        [Category(PropertyCategory.Advanced)]
        [DefaultValue("Repeat new password")]
        public string RepeatNewPasswordLabel { get; set; }

        /// <summary>
        /// Gets or sets the security question field label.
        /// </summary>
        /// <value>
        /// The security question field label.
        /// </value>
        [DisplayName("Security question field label")]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages)]
        [Category(PropertyCategory.Advanced)]
        [DefaultValue("Secret question:")]
        public string SecurityQuestionLabel { get; set; }

        /// <summary>
        /// Gets or sets the save button label.
        /// </summary>
        /// <value>
        /// The save button label.
        /// </value>
        [DisplayName("Save button")]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages)]
        [Category(PropertyCategory.Advanced)]
        [DefaultValue("Save")]
        public string SaveButtonLabel { get; set; }

        /// <summary>
        /// Gets or sets the success message.
        /// </summary>
        /// <value>
        /// The success message.
        /// </value>
        [DisplayName("Success message")]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages)]
        [Category(PropertyCategory.Advanced)]
        [DefaultValue("Your password is successfully changed.")]
        public string SuccessMessage { get; set; }

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        /// <value>
        /// The error message.
        /// </value>
        [DisplayName("Error message")]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages)]
        [Category(PropertyCategory.Advanced)]
        [DefaultValue("You are unable to reset password. Contact your administrator for assistance.")]
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Gets or sets the back link label.
        /// </summary>
        /// <value>
        /// The back link label.
        /// </value>
        [DisplayName("Back link")]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages)]
        [Category(PropertyCategory.Advanced)]
        [DefaultValue("Back to login")]
        public string BackLinkLabel { get; set; }

        /// <summary>
        /// Gets or sets the required fields error message.
        /// </summary>
        /// <value>
        /// The required fields error message.
        /// </value>
        [DisplayName("Required fields error message.")]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages)]
        [Category(PropertyCategory.Advanced)]
        [DefaultValue("All fields are required.")]
        public string AllFieldsAreRequiredErrorMessage { get; set; }

        /// <summary>
        /// Gets or sets the passwords mismatch error message.
        /// </summary>
        /// <value>
        /// The passwords mismatch error message.
        /// </value>
        [DisplayName("Passwords mismatch error message.")]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages)]
        [Category(PropertyCategory.Advanced)]
        [DefaultValue("New password and repeat password don't match.")]
        public string PasswordsMismatchErrorMessage { get; set; }

        /// <summary>
        /// Gets or sets the forgotten password header.
        /// </summary>
        /// <value>
        /// The forgotten password header.
        /// </value>
        [DisplayName("Forgotten password header")]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages)]
        [Category(PropertyCategory.Advanced)]
        [DefaultValue("Forgot your password?")]
        public string ForgottenPasswordHeader { get; set; }

        /// <summary>
        /// Gets or sets the Forgotten password label.
        /// </summary>
        /// <value>
        /// The Forgotten password label.
        /// </value>
        [DisplayName("Forgotten password label")]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages)]
        [Category(PropertyCategory.Advanced)]
        [DefaultValue("Enter your login email address and you will receive an email with a link to reset your password.")]
        public string ForgottenPasswordLabel { get; set; }

        /// <summary>
        /// Gets or sets the email label.
        /// </summary>
        /// <value>
        /// The email label.
        /// </value>
        [DisplayName("Email field label")]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages)]
        [Category(PropertyCategory.Advanced)]
        [DefaultValue("Email")]
        public string EmailLabel { get; set; }

        /// <summary>
        /// Gets or sets the send button label.
        /// </summary>
        /// <value>
        /// The submit button label.
        /// </value>
        [DisplayName("Send button")]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages)]
        [Category(PropertyCategory.Advanced)]
        [DefaultValue("Send")]
        public string SendButtonLabel { get; set; }

        /// <summary>
        /// Gets or sets the Forgotten password submit message.
        /// </summary>
        /// <value>
        /// The Forgotten password submit message.
        /// </value>
        [DisplayName("Forgotten password submit message")]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages)]
        [Category(PropertyCategory.Advanced)]
        [DefaultValue("You sent a request to reset your password to {0}")]
        public string ForgottenPasswordSubmitMessage { get; set; }

        /// <summary>
        /// Gets or sets the Forgotten password link message.
        /// </summary>
        /// <value>
        /// The Forgotten password link message.
        /// </value>
        [DisplayName("Forgotten password link message")]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages)]
        [Category(PropertyCategory.Advanced)]
        [DefaultValue("Use the link provided in your email to reset the password for your account.")]
        public string ForgottenPasswordLinkMessage { get; set; }

        /// <summary>
        /// Gets or sets the invalid email format message.
        /// </summary>
        /// <value>
        /// The invalid email format message.
        /// </value>
        [DisplayName("Invalid email format message")]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages)]
        [Category(PropertyCategory.Advanced)]
        [DefaultValue("Invalid email format.")]
        public string InvalidEmailFormatMessage { get; set; }

        /// <summary>
        /// Gets or sets the field is required message.
        /// </summary>
        /// <value>
        /// The field is required message.
        /// </value>
        [DisplayName("Field is required message")]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages)]
        [Category(PropertyCategory.Advanced)]
        [DefaultValue("Field is required.")]
        public string FieldIsRequiredMessage { get; set; }

        /// <summary>
        /// Gets or sets the attributes for the widget.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [ContentSection(Constants.ContentSectionTitles.Attributes)]
        [DisplayName("Attributes for...")]
        [DataType(customDataType: KnownFieldTypes.Attributes)]
        [LengthDependsOn(null, "", " ", ExtraRecords = "[{\"Name\": \"ResetPassword\", \"Title\": \"Reset password\"}]")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Must be able to set in property editor.")]
        public IDictionary<string, IList<AttributeModel>> Attributes { get; set; }

        private const string SelectPages = "Select pages";
    }
}
