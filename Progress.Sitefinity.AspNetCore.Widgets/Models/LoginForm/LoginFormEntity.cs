using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Progress.Sitefinity.AspNetCore.Models;
using Progress.Sitefinity.AspNetCore.ViewComponents.AttributeConfigurator.Attributes;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Common;
using Progress.Sitefinity.Renderer.Designers;
using Progress.Sitefinity.Renderer.Designers.Attributes;
using Progress.Sitefinity.Renderer.Entities.Content;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.LoginForm
{
    /// <summary>
    /// The entity for the Login form widget. Contains all of the data persited in the database.
    /// </summary>
    [SectionsOrder(SelectPages, LoginWithExternalProviders, Constants.ContentSectionTitles.DisplaySettings)]
    public class LoginFormEntity : IHasMargins<MarginStyle>
    {
        /// <summary>
        /// Gets or sets post login action.
        /// </summary>
        [ContentSection(SelectPages, 1)]
        [DisplayName("After login users will...")]
        [DataType(customDataType: KnownFieldTypes.RadioChoice)]
        public PostLoginAction PostLoginAction { get; set; }

        /// <summary>
        /// Gets or sets the redirect url.
        /// </summary>
        [ContentSection(SelectPages, 1)]
        [DisplayName("")]
        [Content(Type = KnownContentTypes.Pages, AllowMultipleItemsSelection = false)]
        [ConditionalVisibility("{\"conditions\":[{\"fieldName\":\"PostLoginAction\",\"operator\":\"Equals\",\"value\":\"RedirectToPage\"}],\"inline\":\"true\"}")]
        public MixedContentContext PostLoginRedirectPage { get; set; }

        /// <summary>
        /// Gets or sets the login page.
        /// </summary>
        [ContentSection(SelectPages, 1)]
        [DisplayName("Registration page")]
        [Description("This is the page where you have dropped the Registration form widget. If you leave this field empty, a link to the Registration page will not be displayed in the Login form widget.")]
        [Content(Type = KnownContentTypes.Pages, AllowMultipleItemsSelection = false)]
        public MixedContentContext RegistrationPage { get; set; }

        /// <summary>
        /// Gets or sets the login page.
        /// </summary>
        [ContentSection(SelectPages, 1)]
        [DisplayName("Reset password page")]
        [Description("This is the page where you have dropped the Reset password widget. If you leave this field empty, a link to the Reset password page will not be displayed in the Login form widget.")]
        [Content(Type = KnownContentTypes.Pages, AllowMultipleItemsSelection = false)]
        public MixedContentContext ResetPasswordPage { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the remember me checkbox is visible.
        /// </summary>
        [ContentSection(SelectPages, 1)]
        [DataType(customDataType: KnownFieldTypes.CheckBox)]
        [Group("Login form options")]
        [DisplayName("Show \"Remember me\" checkbox")]
        public bool RememberMe { get; set; }

        /// <summary>
        /// Gets or sets the external providers.
        /// </summary>
        /// <value>
        /// The external providers.
        /// </value>
        [ContentSection(LoginWithExternalProviders, 1)]
        [DisplayName("Allow users to log in with...")]
        [DataType(customDataType: KnownFieldTypes.MultipleChoiceChip)]
        [Choice(ServiceUrl = "/Default.GetExternalProviders()", ButtonTitle = "Add", ActionTitle = "Select external providers")]
        public IEnumerable<string> ExternalProviders { get; set; }

        /// <summary>
        /// Gets or sets the view name.
        /// </summary>
        [ViewSelector]
        [ContentSection(Constants.ContentSectionTitles.DisplaySettings, 1)]
        [DisplayName("Login form template")]
        public string SfViewName { get; set; }

        /// <summary>
        /// Gets or sets the margins.
        /// </summary>
        [ContentSection(Constants.ContentSectionTitles.DisplaySettings, 1)]
        [DisplayName("Margins")]
        [TableView("Login form")]
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
        /// Gets or sets the header.
        /// </summary>
        /// <value>
        /// The header.
        /// </value>
        [DisplayName("Login form header")]
        [Category(PropertyCategory.Advanced)]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages)]
        [DefaultValue("Login")]
        public string Header { get; set; }

        /// <summary>
        /// Gets or sets the email label.
        /// </summary>
        /// <value>
        /// The email label.
        /// </value>
        [DisplayName("Email field label")]
        [Category(PropertyCategory.Advanced)]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages)]
        [DefaultValue("Email / Username")]
        public string EmailLabel { get; set; }

        /// <summary>
        /// Gets or sets the password label.
        /// </summary>
        /// <value>
        /// The password label.
        /// </value>
        [DisplayName("Password field label")]
        [Category(PropertyCategory.Advanced)]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages)]
        [DefaultValue("Password")]
        public string PasswordLabel { get; set; }

        /// <summary>
        /// Gets or sets the submit button label.
        /// </summary>
        /// <value>
        /// The submit button label.
        /// </value>
        [DisplayName("Login form button")]
        [Category(PropertyCategory.Advanced)]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages)]
        [DefaultValue("Log in")]
        public string SubmitButtonLabel { get; set; }

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        /// <value>
        /// The error message.
        /// </value>
        [DisplayName("Incorrect credentials message")]
        [Category(PropertyCategory.Advanced)]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages)]
        [DefaultValue("Incorrect credentials.")]
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Gets or sets the remember me label.
        /// </summary>
        /// <value>
        /// The remember me label.
        /// </value>
        [DisplayName("Remember me checkbox")]
        [Category(PropertyCategory.Advanced)]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages)]
        [DefaultValue("Remember me")]
        public string RememberMeLabel { get; set; }

        /// <summary>
        /// Gets or sets the forgotten password link label.
        /// </summary>
        /// <value>
        /// The forgotten password link label.
        /// </value>
        [DisplayName("Forgotten password link")]
        [Category(PropertyCategory.Advanced)]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages)]
        [DefaultValue("Forgotten password")]
        public string ForgottenPasswordLinkLabel { get; set; }

        /// <summary>
        /// Gets or sets the not registered label.
        /// </summary>
        /// <value>
        /// The not registered label.
        /// </value>
        [DisplayName("Not registered label")]
        [Category(PropertyCategory.Advanced)]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages)]
        [DefaultValue("Not registered yet?")]
        public string NotRegisteredLabel { get; set; }

        /// <summary>
        /// Gets or sets the register link text.
        /// </summary>
        /// <value>
        /// The register link text.
        /// </value>
        [DisplayName("Register link")]
        [Category(PropertyCategory.Advanced)]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages)]
        [DefaultValue("Register now")]
        public string RegisterLinkText { get; set; }

        /// <summary>
        /// Gets or sets the external providers header.
        /// </summary>
        /// <value>
        /// The external providers header.
        /// </value>
        [DisplayName("External providers header")]
        [Category(PropertyCategory.Advanced)]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages)]
        [DefaultValue("or use account in...")]
        public string ExternalProvidersHeader { get; set; }

        /// <summary>
        /// Gets or sets the validation required message.
        /// </summary>
        /// <value>
        /// The validation required message.
        /// </value>
        [DisplayName("Required fields error message")]
        [Category(PropertyCategory.Advanced)]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages)]
        [DefaultValue("All fields are required.")]
        public string ValidationRequiredMessage { get; set; }

        /// <summary>
        /// Gets or sets the invalid email error message.
        /// </summary>
        /// <value>
        /// The invalid email error message.
        /// </value>
        [DisplayName("Invalid email error message")]
        [Category(PropertyCategory.Advanced)]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages)]
        [DefaultValue("Invalid email format.")]
        public string ValidationInvalidEmailMessage { get; set; }

        /// <summary>
        /// Gets or sets the attributes for the widget.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [ContentSection(Constants.ContentSectionTitles.Attributes)]
        [DisplayName("Attributes for...")]
        [DataType(customDataType: KnownFieldTypes.Attributes)]
        [LengthDependsOn(null, "", " ", ExtraRecords = "[{\"Name\": \"LoginForm\", \"Title\": \"Login form\"}]")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Must be able to set in property editor.")]
        public IDictionary<string, IList<AttributeModel>> Attributes { get; set; }

        private const string SelectPages = "Select pages";
        private const string LoginWithExternalProviders = "Login with external providers";
    }
}
