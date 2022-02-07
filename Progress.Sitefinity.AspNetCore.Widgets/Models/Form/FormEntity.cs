using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Progress.Sitefinity.AspNetCore.Models;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Common;
using Progress.Sitefinity.Renderer.Designers;
using Progress.Sitefinity.Renderer.Designers.Attributes;
using Progress.Sitefinity.Renderer.Entities.Content;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Form
{
    /// <summary>
    /// The entity for the Form widget. Contains all of the data persited in the database.
    /// </summary>
    public class FormEntity : IHasMargins<MarginStyle>
    {
        /// <summary>
        /// Gets or sets the selected form info.
        /// </summary>
        [ContentSection("Form setup", 0)]
        [DisplayName("Select a form")]
        [Content(Type = KnownContentTypes.Forms, AllowMultipleItemsSelection = false)]
        public MixedContentContext SelectedItems { get; set; }

        /// <summary>
        /// Gets or sets the action to be executed on form submit.
        /// </summary>
        [ContentSection("Form setup", 1)]
        [DisplayName("Confirmation on submit")]
        [DataType(customDataType: KnownFieldTypes.RadioChoice)]
        public FormSubmitAction FormSubmitAction { get; set; }

        /// <summary>
        /// Gets or sets the confirmation message.
        /// </summary>
        [ContentSection("Form setup", 1)]
        [DisplayName("")]
        [DataType(customDataType: KnownFieldTypes.TextArea)]
        [DefaultValue("Thank you for filling out our form.")]
        [ConditionalVisibility("{\"conditions\":[{\"fieldName\":\"FormSubmitAction\",\"operator\":\"Equals\",\"value\":\"Message\"}],\"inline\":\"true\"}")]
        public string SuccessMessage { get; set; }

        /// <summary>
        /// Gets or sets the redirect page.
        /// </summary>
        [ContentSection("Form setup", 1)]
        [DisplayName("")]
        [Content(Type = KnownContentTypes.Pages, AllowMultipleItemsSelection = false)]
        [ConditionalVisibility("{\"conditions\":[{\"fieldName\":\"FormSubmitAction\",\"operator\":\"Equals\",\"value\":\"Redirect\"}],\"inline\":\"true\"}")]
        [Required]
        public MixedContentContext RedirectPage { get; set; }

        /// <summary>
        /// Gets or sets the margins.
        /// </summary>
        [ContentSection(Constants.ContentSectionTitles.DisplaySettings, 0)]
        [DisplayName("Margins")]
        [TableView("Form")]
        public MarginStyle Margins { get; set; }

        /// <summary>
        /// Gets or sets the CSS class of the wrapper.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [DisplayName("CSS class")]
        public string CssClass { get; set; }

        /// <summary>
        /// Gets or sets the attributes for the form.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [ContentSection(Constants.ContentSectionTitles.Attributes)]
        [DisplayName("Attributes for...")]
        [LengthDependsOn(null, "", "", ExtraRecords = "[{\"Name\": \"Form\", \"Title\": \"Form\"}]")]
        [DataType(customDataType: KnownFieldTypes.Attributes)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Must be able to set in property editor.")]
        public IDictionary<string, IList<AttributeModel>> Attributes { get; set; }
    }
}
