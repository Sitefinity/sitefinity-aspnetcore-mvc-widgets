using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Progress.Sitefinity.AspNetCore.Models;
using Progress.Sitefinity.AspNetCore.ViewComponents.AttributeConfigurator.Attributes;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Common;
using Progress.Sitefinity.Renderer.Designers;
using Progress.Sitefinity.Renderer.Designers.Attributes;
using Progress.Sitefinity.Renderer.Entities.Content;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.ChangePassword
{
    /// <summary>
    /// The entity for the Change password widget. Contains all of the data persited in the database.
    /// </summary>
    public class ChangePasswordEntity : IHasMargins<MarginStyle>
    {
        /// <summary>
        /// Gets or sets post password change action.
        /// </summary>
        [ContentSection("Change password setup", 0)]
        [DisplayName("After change users will...")]
        [DataType(customDataType: KnownFieldTypes.RadioChoice)]
        public PostPasswordChangeAction PostPasswordChangeAction { get; set; }

        /// <summary>
        /// Gets or sets the post password change message.
        /// </summary>
        [ContentSection("Change password setup", 2)]
        [DisplayName("Message")]
        [DefaultValue("Your password was changed successfully!")]
        [ConditionalVisibility("{\"conditions\":[{\"fieldName\":\"PostPasswordChangeAction\",\"operator\":\"Equals\",\"value\":\"ViewAMessage\"}]}")]
        [DataType(customDataType: KnownFieldTypes.TextArea)]
        public string PostPasswordChangeMessage { get; set; }

        /// <summary>
        /// Gets or sets the redirect url.
        /// </summary>
        [ContentSection("Change password setup", 1)]
        [DisplayName("")]
        [Content(Type = KnownContentTypes.Pages, AllowMultipleItemsSelection = false)]
        [ConditionalVisibility("{\"conditions\":[{\"fieldName\":\"PostPasswordChangeAction\",\"operator\":\"Equals\",\"value\":\"RedirectToPage\"}],\"inline\":\"true\"}")]
        public MixedContentContext PostPasswordChangeRedirectPage { get; set; }

        /// <summary>
        /// Gets or sets the view name.
        /// </summary>
        [ViewSelector]
        [ContentSection("Display settings", 0)]
        [DisplayName("Change password template")]
        public string SfViewName { get; set; }

        /// <summary>
        /// Gets or sets the margins.
        /// </summary>
        [ContentSection("Display settings", 1)]
        [DisplayName("Margins")]
        [TableView("Change password")]
        public MarginStyle Margins { get; set; }

        /// <summary>
        /// Gets or sets the CSS class.
        /// </summary>
        /// <value>
        /// The CSS class.
        /// </value>
        [Category(PropertyCategory.Advanced)]
        [DisplayName("CSS class")]
        public string CssClass { get; set; }

        /// <summary>
        /// Gets or sets the header.
        /// </summary>
        /// <value>
        /// The header.
        /// </value>
        [DisplayName("Change password header")]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages)]
        [Category(PropertyCategory.Advanced)]
        [DefaultValue("Change password")]
        public string Header { get; set; }

        /// <summary>
        /// Gets or sets the current password label.
        /// </summary>
        /// <value>
        /// The current password label.
        /// </value>
        [DisplayName("Current password field label")]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages)]
        [Category(PropertyCategory.Advanced)]
        [DefaultValue("Current password")]
        public string CurrentPassword { get; set; }

        /// <summary>
        /// Gets or sets the new password label.
        /// </summary>
        /// <value>
        /// The new password label.
        /// </value>
        [DisplayName("New password field label")]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages)]
        [Category(PropertyCategory.Advanced)]
        [DefaultValue("New password")]
        public string NewPassword { get; set; }

        /// <summary>
        /// Gets or sets the confirm password label.
        /// </summary>
        /// <value>
        /// The confirm password label.
        /// </value>
        [DisplayName("Repeat password field label")]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages)]
        [Category(PropertyCategory.Advanced)]
        [DefaultValue("Repeat new password")]
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// Gets or sets the submit button label.
        /// </summary>
        /// <value>
        /// The submit button label.
        /// </value>
        [DisplayName("Submit button")]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages)]
        [Category(PropertyCategory.Advanced)]
        [DefaultValue("Save")]
        public string SubmitButtonLabel { get; set; }

        /// <summary>
        /// Gets or sets the login first message.
        /// </summary>
        /// <value>
        /// The Login first message.
        /// </value>
        [DisplayName("Login error message")]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages)]
        [Category(PropertyCategory.Advanced)]
        [DefaultValue("You need to be logged in to change your password.")]
        public string LoginFirstMessage { get; set; }

        /// <summary>
        /// Gets or sets the validation required message.
        /// </summary>
        /// <value>
        /// The validation required message.
        /// </value>
        [DisplayName("Required fields error message")]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages)]
        [Category(PropertyCategory.Advanced)]
        [DefaultValue("All fields are required.")]
        public string ValidationRequiredMessage { get; set; }

        /// <summary>
        /// Gets or sets the validation mismatch message.
        /// </summary>
        /// <value>
        /// The validation mismatch message.
        /// </value>
        [DisplayName("Password mismatch error message")]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages)]
        [Category(PropertyCategory.Advanced)]
        [DefaultValue("New password and repeat password don't match.")]
        public string ValidationMismatchMessage { get; set; }

        /// <summary>
        /// Gets or sets the external provider message.
        /// </summary>
        /// <value>
        /// The external provider message.
        /// </value>
        [DisplayName("External provider message")]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages)]
        [Category(PropertyCategory.Advanced)]
        [DefaultValue("Your profile does not store any passwords, because you are registered with {0}")]
        public string ExternalProviderMessageFormat { get; set; }

        /// <summary>
        /// Gets or sets the attributes for the widget.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [ContentSection(Constants.ContentSectionTitles.Attributes)]
        [DisplayName("Attributes for...")]
        [DataType(customDataType: KnownFieldTypes.Attributes)]
        [LengthDependsOn(null, "", " ", ExtraRecords = "[{\"Name\": \"ChangePassword\", \"Title\": \"Change password\"}]")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Must be able to set in property editor.")]
        public IDictionary<string, IList<AttributeModel>> Attributes { get; set; }
    }
}
