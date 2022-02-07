using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Progress.Sitefinity.AspNetCore.FormWidgets.Entities.Common;
using Progress.Sitefinity.AspNetCore.ViewComponents.AttributeConfigurator.Attributes;
using Progress.Sitefinity.Renderer.Contracts.Forms;
using Progress.Sitefinity.Renderer.Designers;
using Progress.Sitefinity.Renderer.Designers.Attributes;
using Progress.Sitefinity.Renderer.Entities;

namespace Progress.Sitefinity.AspNetCore.FormWidgets.Entities.FileField
{
    /// <summary>
    /// Entity for holding the text field specific settings.
    /// </summary>
    [SectionsOrder(Constants.ContentSectionTitles.LabelsAndContent, Constants.ContentSectionTitles.Limitations, Constants.ContentSectionTitles.DisplaySettings)]
    public class FileFieldEntity : IFileFieldContract
    {
        /// <inheritdoc/>
        [ContentSection(Constants.ContentSectionTitles.LabelsAndContent, 1)]
        [DefaultValue("Upload file")]
        public string Label { get; set; }

        /// <summary>
        /// Gets or sets the instructional text.
        /// </summary>
        [Description("Suitable for giving examples how the entered value will be used.")]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndContent, 2)]
        [DisplayName("Instructional text")]
        public string InstructionalText { get; set; }

        /// <inheritdoc/>
        [ContentSection(Constants.ContentSectionTitles.LabelsAndContent, 3)]
        [DataType(customDataType: KnownFieldTypes.CheckBox)]
        [DisplayName("Upload multiple files")]
        [Group("Options")]
        public bool AllowMultipleFiles { get; set; }

        /// <inheritdoc/>
        [ContentSection(Constants.ContentSectionTitles.LabelsAndContent, 4)]
        [DisplayName("Required field")]
        [DataType(customDataType: KnownFieldTypes.CheckBox)]
        [Group("Options")]
        public bool Required { get; set; }

        /// <inheritdoc/>
        [ContentSection(Constants.ContentSectionTitles.LabelsAndContent, 5)]
        [DisplayName("Hide field initially (use form rules to display it)")]
        [DataType(customDataType: KnownFieldTypes.CheckBox)]
        [Group("Options")]
        public bool Hidden { get; set; }

        /// <inheritdoc/>
        [DisplayName("Error message if the field is empty")]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndContent, 7)]
        [DefaultValue("{0} field is required")]
        [ConditionalVisibility("{\"conditions\":[{\"fieldName\":\"Required\",\"operator\":\"Equals\",\"value\":true}]}")]
        public string RequiredErrorMessage { get; set; }

        /// <inheritdoc/>
        [ContentSection(Constants.ContentSectionTitles.Limitations)]
        [DataType(customDataType: KnownFieldTypes.Range)]
        [Suffix("MB")]
        public NumericRange Range { get; set; }

        /// <inheritdoc/>
        [ContentSection(Constants.ContentSectionTitles.Limitations)]
        [DisplayName("Error message if file size is out of range")]
        [DefaultValue("The size of the selected file is too large")]
        public string FileSizeViolationMessage { get; set; }

        /// <inheritdoc/>
        [ContentSection(Constants.ContentSectionTitles.Limitations)]
        [DataType(customDataType: KnownFieldTypes.FileTypes)]
        [DisplayName("File types")]
        public FileTypes FileTypes { get; set; }

        /// <inheritdoc/>
        [ContentSection(Constants.ContentSectionTitles.Limitations)]
        [DisplayName("Error message if file type is not allowed")]
        [DefaultValue("File type is not allowed to upload")]
        [ConditionalVisibility("{\"conditions\":[{\"fieldName\":\"FileTypes\",\"operator\":\"NotEquals\",\"value\":null}]}")]
        public string FileTypeViolationMessage { get; set; }

        /// <summary>
        /// Gets or sets the view name.
        /// </summary>
        [ViewSelector]
        [ContentSection(Constants.ContentSectionTitles.DisplaySettings)]
        [DisplayName("Template")]
        public string SfViewName { get; set; }

        /// <summary>
        /// Gets or sets the CSS class.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [ContentSection("AdvancedMain", 2)]
        [DisplayName("CSS class")]
        public string CssClass { get; set; }

        /// <inheritdoc/>
        [Browsable(false)]
        public string SfFieldType { get; set; }

        /// <inheritdoc/>
        [Browsable(false)]
        public string SfFieldName { get; set; }
    }
}
