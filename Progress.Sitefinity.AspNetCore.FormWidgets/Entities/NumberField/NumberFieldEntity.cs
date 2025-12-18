using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Progress.Sitefinity.AspNetCore.ViewComponents.AttributeConfigurator.Attributes;
using Progress.Sitefinity.Renderer.Contracts.Forms;
using Progress.Sitefinity.Renderer.Designers;
using Progress.Sitefinity.Renderer.Designers.Attributes;
using Progress.Sitefinity.Renderer.Entities;

namespace Progress.Sitefinity.AspNetCore.FormWidgets.Entities.NumberField
{
    /// <summary>
    /// Entity for holding the text field specific settings.
    /// </summary>
    [SectionsOrder(Constants.ContentSectionTitles.LabelsAndContent, Constants.ContentSectionTitles.Limitations, Constants.ContentSectionTitles.DisplaySettings)]
    public class NumberFieldEntity : INumberContract
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
        [ContentSection(Constants.ContentSectionTitles.LabelsAndContent, 3)]
        [DisplayName("Placeholder text")]
        public string PlaceholderText { get; set; }

        /// <summary>
        /// Gets or sets the predefined value.
        /// </summary>
        [DisplayName("Predefined value")]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndContent, 4)]
        public decimal PredefinedValue { get; set; }

        /// <summary>
        /// Gets or sets the prefix.
        /// </summary>
        [ContentSection(Constants.ContentSectionTitles.LabelsAndContent, 5)]
        [Choice(Choices = "[{\"Title\":\"- Select -\",\"Name\":\"default\",\"Value\": 0 },{\"Title\":\"Prefix\",\"Name\":\"prefix\",\"Value\":1}, {\"Title\":\"Suffix\",\"Name\":\"suffix\",\"Value\":2}]")]
        [DataType(customDataType: KnownFieldTypes.DropdownWithText)]
        [DisplayName("Field prefix or suffix")]
        [Description("Used to add text next to the field such as units, currency, etc.")]
        public ChoiceWithText PrefixOrSuffix { get; set; }

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
        [DisplayName("Allow decimals")]
        [Choice("[{\"Title\":\"Yes\",\"Name\":\"Yes\",\"Value\":true,\"Icon\":null},{\"Title\":\"No\",\"Name\":\"No\",\"Value\":false,\"Icon\":null}]")]
        [DataType(customDataType: KnownFieldTypes.ChipChoice)]
        [DefaultValue(false)]
        public bool AllowDecimals { get; set; }

        /// <inheritdoc/>
        [DisplayName("Number range")]
        [ContentSection(Constants.ContentSectionTitles.Limitations)]
        [DataType(customDataType: KnownFieldTypes.RangeLimitation)]
        [RangeLimitation(true, RangeLimitationRangeType.Between)]
        [Suffix("value")]
        public DecimalNumericRange ValueRange { get; set; }

        /// <inheritdoc/>
        [ContentSection(Constants.ContentSectionTitles.Limitations)]
        [DisplayName("Error message displayed when the entry is out of range")]
        [DefaultValue("Number is out of the allowed range")]
        public string ValueRangeViolationMessage { get; set; }

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
        [DefaultValue("XS")]
        [Choice(Choices = "[{ \"Title\": \"None\", \"Name\": \"None\", \"Value\": \"None\", \"Tooltip\": \"None. Display system default size for this field.\" }, { \"Title\": \"XS\", \"Name\": \"XS\", \"Value\": \"XS\", \"Tooltip\": \"Extra Small. Takes 25% of the container's width.\" }, { \"Title\": \"S\", \"Name\": \"S\", \"Value\": \"S\", \"Tooltip\": \"Small. Takes 50% of the container's width.\" }, { \"Title\": \"M\", \"Name\": \"M\", \"Value\": \"M\", \"Tooltip\": \"Medium. Takes 75% of the container's width.\" }, { \"Title\": \"L\", \"Name\": \"L\", \"Value\": \"L\", \"Tooltip\": \"Large. Takes 100% of the container's width.\" }]", ChipMaxThreshold = 5)]
        [DataType(KnownFieldTypes.ChipChoice)]
        public string FieldSize { get; set; }

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
