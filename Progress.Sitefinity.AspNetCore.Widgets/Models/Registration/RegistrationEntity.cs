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

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Registration
{
    /// <summary>
    /// The entity for the Registration widget. Contains all of the data persited in the database.
    /// </summary>
    public class RegistrationEntity : IHasMargins<MarginStyle>
    {
        /// <summary>
        /// Gets or sets post registration action.
        /// </summary>
        [ContentSection(SelectPages, 1)]
        [DisplayName("If form is submitted successfully, users will...")]
        [DataType(customDataType: KnownFieldTypes.RadioChoice)]
        public PostRegistrationAction PostRegistrationAction { get; set; }

        /// <summary>
        /// Gets or sets the redirect url.
        /// </summary>
        [ContentSection(SelectPages, 1)]
        [DisplayName("")]
        [Content(Type = KnownContentTypes.Pages, AllowMultipleItemsSelection = false)]
        [ConditionalVisibility("{\"conditions\":[{\"fieldName\":\"PostRegistrationAction\",\"operator\":\"Equals\",\"value\":\"RedirectToPage\"}],\"inline\":\"true\"}")]
        public MixedContentContext PostRegistrationRedirectPage { get; set; }

        /// <summary>
        /// Gets or sets the login page.
        /// </summary>
        [ContentSection(SelectPages, 1)]
        [DisplayName("Login page")]
        [Description("This is the page where you have dropped the Login form widget. If you leave this field empty, a link to the Login page will not be displayed in the Registration widget.")]
        [Content(Type = KnownContentTypes.Pages, AllowMultipleItemsSelection = false)]
        public MixedContentContext LoginPage { get; set; }

        /// <summary>
        /// Gets or sets the external providers.
        /// </summary>
        /// <value>
        /// The external providers.
        /// </value>
        [ContentSection(RegisterWithExternalProviders, 1)]
        [DisplayName("Allow users to log in with...")]
        [DataType(customDataType: KnownFieldTypes.MultipleChoiceChip)]
        [Choice(ServiceUrl = "/Default.GetExternalProviders()", ButtonTitle = "Add", ActionTitle = "Select external providers")]
        public IEnumerable<string> ExternalProviders { get; set; }

        /// <summary>
        /// Gets or sets the view name.
        /// </summary>
        [ViewSelector]
        [ContentSection(DisplaySettings, 1)]
        [DisplayName("Registration template")]
        public string SfViewName { get; set; }

        /// <summary>
        /// Gets or sets the margins.
        /// </summary>
        [ContentSection(DisplaySettings, 1)]
        [DisplayName("Margins")]
        [TableView("Registration")]
        public MarginStyle Margins { get; set; }

        /// <summary>
        /// Gets or sets the CSS class of the button.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [DisplayName("CSS class")]
        public string CssClass { get; set; }

        /// <summary>
        /// Gets or sets the header.
        /// </summary>
        /// <value>
        /// The header.
        /// </value>
        [DisplayName("Registration header")]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages)]
        [Category(PropertyCategory.Advanced)]
        [DefaultValue(DefaultHeader)]
        public string Header { get; set; }

        /// <summary>
        /// Gets or sets the first name label.
        /// </summary>
        /// <value>
        /// The first name label.
        /// </value>
        [DisplayName("First name field label")]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages)]
        [Category(PropertyCategory.Advanced)]
        [DefaultValue(DefaultFirstNameLabel)]
        public string FirstNameLabel { get; set; }

        /// <summary>
        /// Gets or sets the last name label.
        /// </summary>
        /// <value>
        /// The last name label.
        /// </value>
        [DisplayName("Last name field label")]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages)]
        [Category(PropertyCategory.Advanced)]
        [DefaultValue(DefaultLastNameLabel)]
        public string LastNameLabel { get; set; }

        /// <summary>
        /// Gets or sets the email field label.
        /// </summary>
        /// <value>
        /// The email field label.
        /// </value>
        [DisplayName("Email field label")]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages)]
        [Category(PropertyCategory.Advanced)]
        [DefaultValue(DefaultEmailLabel)]
        public string EmailLabel { get; set; }

        /// <summary>
        /// Gets or sets the password label.
        /// </summary>
        /// <value>
        /// The password label.
        /// </value>
        [DisplayName("Password field label")]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages)]
        [Category(PropertyCategory.Advanced)]
        [DefaultValue(DefaultPasswordLabel)]
        public string PasswordLabel { get; set; }

        /// <summary>
        /// Gets or sets the repeat password field label.
        /// </summary>
        /// <value>
        /// The repeat password field label.
        /// </value>
        [DisplayName("Reset password field label")]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages)]
        [Category(PropertyCategory.Advanced)]
        [DefaultValue(DefaultRepeatPasswordLabel)]
        public string RepeatPasswordLabel { get; set; }

        /// <summary>
        /// Gets or sets the secret question field label.
        /// </summary>
        /// <value>
        /// The secret question field label.
        /// </value>
        [DisplayName("Secret question field label")]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages)]
        [Category(PropertyCategory.Advanced)]
        [DefaultValue(DefaultSecretQuestion)]
        public string SecretQuestionLabel { get; set; }

        /// <summary>
        /// Gets or sets the secret answer field label.
        /// </summary>
        /// <value>
        /// The secret answer field label.
        /// </value>
        [DisplayName("Secret answer field label")]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages)]
        [Category(PropertyCategory.Advanced)]
        [DefaultValue(DefaultSecretAnswer)]
        public string SecretAnswerLabel { get; set; }

        /// <summary>
        /// Gets or sets the register button.
        /// </summary>
        /// <value>
        /// The register button label.
        /// </value>
        [DisplayName("Register button")]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages)]
        [Category(PropertyCategory.Advanced)]
        [DefaultValue(DefaultRegisterButtonLabel)]
        public string RegisterButtonLabel { get; set; }

        /// <summary>
        /// Gets or sets the activation link header.
        /// </summary>
        /// <value>
        /// The activation link header.
        /// </value>
        [DisplayName("Activation link header")]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages)]
        [Category(PropertyCategory.Advanced)]
        [DefaultValue(DefaultPleaseCheckYourEmailHeader)]
        public string PleaseCheckYourEmailHeader { get; set; }

        /// <summary>
        /// Gets or sets the activation link label.
        /// </summary>
        /// <value>
        /// The activation link label.
        /// </value>
        [DisplayName("Activation link label")]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages)]
        [Category(PropertyCategory.Advanced)]
        [DefaultValue(DefaultPleaseCheckYourEmailMessage)]
        public string PleaseCheckYourEmailMessage { get; set; }

        /// <summary>
        /// Gets or sets the send again label.
        /// </summary>
        /// <value>
        /// The send again label.
        /// </value>
        [DisplayName("Send again label")]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages)]
        [Category(PropertyCategory.Advanced)]
        [DefaultValue(DefaultPleaseCheckYourEmailAnotherMessage)]
        public string PleaseCheckYourEmailAnotherMessage { get; set; }

        /// <summary>
        /// Gets or sets the send again link.
        /// </summary>
        /// <value>
        /// The send again link.
        /// </value>
        [DisplayName("Send again link")]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages)]
        [Category(PropertyCategory.Advanced)]
        [DefaultValue(DefaultSendAgainLink)]
        public string SendAgainLink { get; set; }

        /// <summary>
        /// Gets or sets the success header.
        /// </summary>
        /// <value>
        /// The success header.
        /// </value>
        [DisplayName("Success header")]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages)]
        [Category(PropertyCategory.Advanced)]
        [DefaultValue(DefaultSuccessHeader)]
        public string SuccessHeader { get; set; }

        /// <summary>
        /// Gets or sets the success label.
        /// </summary>
        /// <value>
        /// The success label.
        /// </value>
        [DisplayName("Success label")]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages)]
        [Category(PropertyCategory.Advanced)]
        [DefaultValue(DefaultSuccessLabel)]
        public string SuccessLabel { get; set; }

        /// <summary>
        /// Gets or sets the login label.
        /// </summary>
        /// <value>
        /// The login label.
        /// </value>
        [DisplayName("Login label")]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages)]
        [Category(PropertyCategory.Advanced)]
        [DefaultValue(DefaultLoginLabel)]
        public string LoginLabel { get; set; }

        /// <summary>
        /// Gets or sets the login link.
        /// </summary>
        /// <value>
        /// The login link.
        /// </value>
        [DisplayName("Login link")]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages)]
        [Category(PropertyCategory.Advanced)]
        [DefaultValue(DefaultLoginLink)]
        public string LoginLink { get; set; }

        /// <summary>
        /// Gets or sets the external providers header.
        /// </summary>
        /// <value>
        /// The external providers header.
        /// </value>
        [DisplayName("External providers header")]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages)]
        [Category(PropertyCategory.Advanced)]
        [DefaultValue(DefaultExternalProvidersHeader)]
        public string ExternalProvidersHeader { get; set; }

        /// <summary>
        /// Gets or sets the validation required message.
        /// </summary>
        /// <value>
        /// The validation required message.
        /// </value>
        [DisplayName("Required fields error message")]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages)]
        [Category(PropertyCategory.Advanced)]
        [DefaultValue(DefaultValidationRequiredMessage)]
        public string ValidationRequiredMessage { get; set; }

        /// <summary>
        /// Gets or sets the invalid email error message.
        /// </summary>
        /// <value>
        /// The invalid email error message.
        /// </value>
        [DisplayName("Invalid email error message")]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages)]
        [Category(PropertyCategory.Advanced)]
        [DefaultValue(DefaultValidationInvalidEmailMessage)]
        public string ValidationInvalidEmailMessage { get; set; }

        /// <summary>
        /// Gets or sets the validation mismatch message.
        /// </summary>
        /// <value>
        /// The validation mismatch message.
        /// </value>
        [DisplayName("Password mismatch error message")]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages)]
        [Category(PropertyCategory.Advanced)]
        [DefaultValue(DefaultValidationMismatchMessage)]
        public string ValidationMismatchMessage { get; set; }

        /// <summary>
        /// Gets or sets the activation message.
        /// </summary>
        /// <value>
        /// The activation message.
        /// </value>
        [DisplayName("Activation message")]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages)]
        [Category(PropertyCategory.Advanced)]
        [DefaultValue(DefaultActivationMessage)]
        public string ActivationMessage { get; set; }

        /// <summary>
        /// Gets or sets the Activation error title.
        /// </summary>
        /// <value>
        /// The Activation error title.
        /// </value>
        [DisplayName("Activation error title")]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages)]
        [Category(PropertyCategory.Advanced)]
        [DefaultValue(DefaultActivationFailTitle)]
        public string ActivationFailTitle { get; set; }

        /// <summary>
        /// Gets or sets the Activation error message.
        /// </summary>
        /// <value>
        /// The Activation error message.
        /// </value>
        [DisplayName("Activation error message")]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages)]
        [Category(PropertyCategory.Advanced)]
        [DefaultValue(DefaultActivationFailMessage)]
        public string ActivationFailMessage { get; set; }

        /// <summary>
        /// Gets or sets the activation expired link title.
        /// </summary>
        /// <value>
        /// The activation expired link title.
        /// </value>
        [DisplayName("Expired activation link title")]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages)]
        [Category(PropertyCategory.Advanced)]
        [DefaultValue(DefaultActivationLinkExpiredTitle)]
        public string ActivationLinkExpiredTitle { get; set; }

        /// <summary>
        /// Gets or sets the activation fail message.
        /// </summary>
        /// <value>
        /// The activation fail message.
        /// </value>
        [DisplayName("Expired activation link message")]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages)]
        [Category(PropertyCategory.Advanced)]
        [DefaultValue(DefaultActivationLinkExpiredDescription)]
        public string ActivationLinkExpiredMessage { get; set; }

        /// <summary>
        /// Gets or sets the send activation link label.
        /// </summary>
        /// <value>
        /// The send again link label.
        /// </value>
        [DisplayName("Send activation link")]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages)]
        [Category(PropertyCategory.Advanced)]
        [DefaultValue(DefaultActivateAccountLink)]
        public string ActivateAccountLink { get; set; }

        /// <summary>
        /// Gets or sets the attributes for the widget.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [ContentSection(Constants.ContentSectionTitles.Attributes)]
        [DisplayName("Attributes for...")]
        [DataType(customDataType: KnownFieldTypes.Attributes)]
        [LengthDependsOn(null, "", " ", ExtraRecords = "[{\"Name\": \"Registration\", \"Title\": \"Registration\"}]")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Must be able to set in property editor.")]
        public IDictionary<string, IList<AttributeModel>> Attributes { get; set; }

        private const string SelectPages = "Select pages";
        private const string RegisterWithExternalProviders = "Register with external providers";
        private const string DisplaySettings = "Display settings";

        private const string DefaultHeader = "Registration";
        private const string DefaultFirstNameLabel = "First name";
        private const string DefaultLastNameLabel = "Last name";
        private const string DefaultEmailLabel = "Email";
        private const string DefaultPasswordLabel = "Password";
        private const string DefaultRepeatPasswordLabel = "Repeat password";
        private const string DefaultSecretQuestion = "Secret question";
        private const string DefaultSecretAnswer = "Secret answer";
        private const string DefaultRegisterButtonLabel = "Register";
        private const string DefaultPleaseCheckYourEmailHeader = "Please check your email";
        private const string DefaultPleaseCheckYourEmailMessage = "An activation link has been sent to";
        private const string DefaultPleaseCheckYourEmailAnotherMessage = "Another activation link has been sent to {0}. If you have not received an email please check your spam box.";
        private const string DefaultSendAgainLink = "Send again";
        private const string DefaultActivateAccountLink = "Send activation link";
        private const string DefaultSuccessHeader = "Thank you!";
        private const string DefaultSuccessLabel = "You are successfully registered.";
        private const string DefaultLoginLabel = "Already registered?";
        private const string DefaultLoginLink = "Log in";
        private const string DefaultExternalProvidersHeader = "or connect with...";
        private const string DefaultValidationRequiredMessage = "All fields are required.";
        private const string DefaultValidationMismatchMessage = "Password and repeat password don't match.";
        private const string DefaultValidationInvalidEmailMessage = "Invalid email format.";
        private const string DefaultActivationMessage = "Your account is activated";
        private const string DefaultActivationFailTitle = "Error has occured";
        private const string DefaultActivationFailMessage = "We could not activate your account.";
        private const string DefaultTryAgainMessage = "Try again";
        private const string DefaultActivationLinkExpiredTitle = "Activation link has expired";
        private const string DefaultActivationLinkExpiredDescription = "To access your account resend activation link to {0}";
    }
}
