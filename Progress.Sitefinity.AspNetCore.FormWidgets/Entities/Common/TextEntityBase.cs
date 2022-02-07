using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Progress.Sitefinity.AspNetCore.ViewComponents.AttributeConfigurator.Attributes;
using Progress.Sitefinity.Renderer.Contracts.Forms;
using Progress.Sitefinity.Renderer.Designers;
using Progress.Sitefinity.Renderer.Designers.Attributes;
using Progress.Sitefinity.Renderer.Entities;

namespace Progress.Sitefinity.AspNetCore.FormWidgets.Entities.Common
{
    /// <summary>
    /// Base entity that holds the proprties for text fields.
    /// </summary>
    [SectionsOrder(Constants.ContentSectionTitles.LabelsAndContent, Constants.ContentSectionTitles.Limitations, Constants.ContentSectionTitles.DisplaySettings)]
    public class TextEntityBase : ITextContract
    {
        /// <inheritdoc/>
        [ContentSection(Constants.ContentSectionTitles.LabelsAndContent, 1)]

        [DefaultValue("Untitled")]
        public string Label { get; set; }

        /// <summary>
        /// Gets or sets the instructional text.
        /// </summary>
        [Description("Suitable for giving examples how the entered value will be used.")]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndContent, 2)]
        [DisplayName("Instructional text")]
        public string InstructionalText { get; set; }

        /// <summary>
        /// Gets or sets the placeholder text.
        /// </summary>
        [ContentSection(Constants.ContentSectionTitles.LabelsAndContent, 4)]
        [DisplayName("Placeholder text")]
        public string PlaceholderText { get; set; }

        /// <summary>
        /// Gets or sets the predefined value.
        /// </summary>
        [DisplayName("Predefined value")]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndContent, 5)]
        public string PredefinedValue { get; set; }

        /// <inheritdoc/>
        [ContentSection(Constants.ContentSectionTitles.LabelsAndContent, 6)]
        [DisplayName("Required field")]
        [DataType(customDataType: KnownFieldTypes.CheckBox)]
        [Group("Options")]
        public bool Required { get; set; }

        /// <inheritdoc/>
        [ContentSection(Constants.ContentSectionTitles.LabelsAndContent, 7)]
        [DisplayName("Hide field initially (use form rules to display it)")]
        [DataType(customDataType: KnownFieldTypes.CheckBox)]
        [Group("Options")]
        public bool Hidden { get; set; }

        /// <inheritdoc/>
        [DisplayName("Error message if the field is empty")]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndContent, 8)]
        [DefaultValue("{0} field is required")]
        [ConditionalVisibility("{\"conditions\":[{\"fieldName\":\"Required\",\"operator\":\"Equals\",\"value\":true}]}")]
        public string RequiredErrorMessage { get; set; }

        /// <inheritdoc/>
        [ContentSection(Constants.ContentSectionTitles.Limitations)]
        [DataType(customDataType: KnownFieldTypes.Range)]
        [Suffix("characters")]
        public NumericRange Range { get; set; }

        /// <inheritdoc/>
        [ContentSection(Constants.ContentSectionTitles.Limitations)]
        [DisplayName("Error message displayed when the entry is out of range")]
        [DefaultValue("{0} field input is too long")]
        public string TextLengthViolationMessage { get; set; }

        /// <summary>
        /// Gets or sets the view name.
        /// </summary>
        [ViewSelector]
        [ContentSection(Constants.ContentSectionTitles.DisplaySettings)]
        [DisplayName("Template")]
        public string SfViewName { get; set; }

        /// <summary>
        /// Gets or sets the field size.
        /// </summary>
        [ContentSection(Constants.ContentSectionTitles.DisplaySettings)]
        [DisplayName("Field size")]
        [DataType(KnownFieldTypes.ChipChoice)]
        public FieldSize FieldSize { get; set; }

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
