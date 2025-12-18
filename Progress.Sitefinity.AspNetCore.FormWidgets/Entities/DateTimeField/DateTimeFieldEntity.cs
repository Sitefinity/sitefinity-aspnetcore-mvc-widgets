using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Progress.Sitefinity.AspNetCore.ViewComponents.AttributeConfigurator.Attributes;
using Progress.Sitefinity.Renderer.Contracts.Forms;
using Progress.Sitefinity.Renderer.Designers;
using Progress.Sitefinity.Renderer.Designers.Attributes;
using Progress.Sitefinity.Renderer.Entities;

namespace Progress.Sitefinity.AspNetCore.FormWidgets.Entities.DateTimeField
{
    /// <summary>
    /// Entity for holding the date and time field specific settings.
    /// </summary>
    [SectionsOrder(Constants.ContentSectionTitles.LabelsAndContent, Constants.ContentSectionTitles.DisplaySettings)]
    public class DateTimeFieldEntity : IDateTimeContract
    {
        /// <inheritdoc />
        [ContentSection(Constants.ContentSectionTitles.LabelsAndContent, 1)]
        [DefaultValue("Untitled")]
        public string Label { get; set; }

        /// <summary>
        /// Gets or sets the instructional text.
        /// </summary>
        [ContentSection(Constants.ContentSectionTitles.LabelsAndContent, 2)]
        [DisplayName("Instructional text")]
        [Description("Suitable for giving examples how the entered value will be used.")]
        public string InstructionalText { get; set; }

        /// <summary>
        /// Gets or sets the type of the date and time.
        /// </summary>
        [ContentSection(Constants.ContentSectionTitles.LabelsAndContent, 3)]
        [DisplayName("Display")]
        [Description("This property can only be set initially. Saving the form makes the property read-only.")]
        [DataType(customDataType: KnownFieldTypes.Choices)]
        [DefaultValue(DateFieldType.DateOnly)]
        public DateFieldType FieldType { get; set; }

        /// <inheritdoc />
        [Group("Options")]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndContent, 4)]
        [DisplayName("Required field")]
        [DataType(customDataType: KnownFieldTypes.CheckBox)]
        public bool Required { get; set; }

        /// <inheritdoc />
        [Group("Options")]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndContent, 5)]
        [DisplayName("Hide field initially (use form rules to display it)")]
        [DataType(customDataType: KnownFieldTypes.CheckBox)]
        public bool Hidden { get; set; }

        /// <inheritdoc />
        [ContentSection(Constants.ContentSectionTitles.LabelsAndContent, 6)]
        [DisplayName("Error message if the field is empty")]
        [DefaultValue("{0} field is required")]
        [ConditionalVisibility("{\"conditions\":[{\"fieldName\":\"Required\",\"operator\":\"Equals\",\"value\":true}]}")]
        public string RequiredErrorMessage { get; set; }

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

        /// <inheritdoc />
        [Browsable(false)]
        public string SfFieldName { get; set; }

        /// <inheritdoc />
        [Browsable(false)]
        public string SfFieldType { get; set; }
    }
}
