using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Progress.Sitefinity.AspNetCore.ViewComponents.AttributeConfigurator.Attributes;
using Progress.Sitefinity.Renderer.Contracts.Forms;
using Progress.Sitefinity.Renderer.Designers;
using Progress.Sitefinity.Renderer.Designers.Attributes;
using Progress.Sitefinity.Renderer.Entities.Content;
using static Progress.Sitefinity.AspNetCore.Constants;

namespace Progress.Sitefinity.AspNetCore.FormWidgets.Entities.Dropdown
{
    /// <summary>
    /// Entity for the dropdown widget.
    /// </summary>
    public class DynamicListEntity : IFormFieldContract
    {
        /// <inheritdoc/>
        [DefaultValue("Select a choice")]
        [ContentSection(ContentSectionTitles.LabelsAndContent, 1)]
        [DisplayName("Label")]
        public string Label { get; set; }

        /// <summary>
        /// Gets or sets the instructional text of the widget.
        /// </summary>
        [ContentSection(ContentSectionTitles.LabelsAndContent, 2)]
        [DisplayName("Instructional text")]
        [Description("Suitable for giving examples how the entered value will be used.")]
        public string InstructionalText { get; set; }

        /// <summary>
        /// Gets or sets the view name.
        /// </summary>
        [ViewSelector]
        [ContentSection(Constants.ContentSectionTitles.DisplaySettings, 1)]
        [DefaultValue("Dropdown")]
        [DisplayName("Template")]
        public string SfViewName { get; set; }

        /// <summary>
        /// Gets or sets the CSS class.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [ContentSection("AdvancedMain")]
        [DisplayName("CSS class")]
        public string CssClass { get; set; }

        /// <inheritdoc/>
        [Browsable(false)]
        public string SfFieldType { get; set; }

        /// <inheritdoc/>
        [Browsable(false)]
        public string SfFieldName { get; set; }

        /// <summary>
        /// Gets or sets the field size.
        /// </summary>
        [ContentSection(Constants.ContentSectionTitles.LabelsAndContent, 3)]
        [DisplayName("List type")]
        [DataType(KnownFieldTypes.ChipChoice)]
        public Selection ListType { get; set; }

        /// <summary>
        /// Gets or sets the default selection of items.
        /// </summary>
        [Content]
        [DisplayName("")]
        [ContentSection(ContentSectionTitles.LabelsAndContent, 4)]
        [ConditionalVisibility("{\"conditions\":[{\"fieldName\":\"ListType\",\"operator\":\"Equals\",\"value\":\"Content\"}]}")]
        public MixedContentContext SelectedContent { get; set; }

        /// <inheritdoc/>
        [ContentSection(ContentSectionTitles.LabelsAndContent, 8)]
        [DisplayName("Required field")]
        [DataType(customDataType: KnownFieldTypes.CheckBox)]
        [Group("Options")]
        public bool Required { get; set; }

        /// <inheritdoc/>
        [ContentSection(ContentSectionTitles.LabelsAndContent, 8)]
        [DisplayName("Hide field initially (use form rules to display it)")]
        [DataType(customDataType: KnownFieldTypes.CheckBox)]
        [Group("Options")]
        public bool Hidden { get; set; }

        /// <inheritdoc/>
        [ContentSection(ContentSectionTitles.LabelsAndContent, 9)]
        [DisplayName("Error message if choice is not selected")]
        [DefaultValue("{0} field is required")]
        [ConditionalVisibility("{\"conditions\":[{\"fieldName\":\"Required\",\"operator\":\"Equals\",\"value\":true}]}")]
        public string RequiredErrorMessage { get; set; }

        /// <summary>
        /// Gets or sets the order by info.
        /// </summary>
        [ContentSection(ContentSectionTitles.LabelsAndContent, 5)]
        [DisplayName("Sort items")]
        [DefaultValue("PublicationDate DESC")]
        [DataType(customDataType: "dynamicChoicePerItemType")]
        [Choice(ServiceUrl = "/Default.Sorters()?frontend=True")]
        [ConditionalVisibility("{\"conditions\":[{\"fieldName\":\"ListType\",\"operator\":\"Equals\",\"value\":\"Content\"}]}")]
        public string OrderByContent { get; set; }

        /// <summary>
        /// Gets or sets the default selection of classifications.
        /// </summary>
        [ContentSection(ContentSectionTitles.LabelsAndContent, 4)]
        [Placeholder("Select classification")]
        [DisplayName("Classification")]
        [DataType(customDataType: "taxonSelector")]
        [Required(ErrorMessage = "Please select a classification")]
        [ConditionalVisibility("{\"conditions\":[{\"fieldName\":\"ListType\",\"operator\":\"Equals\",\"value\":\"Classification\"}]}")]
        public ClassificationSettings ClassificationSettings { get; set; }

        /// <summary>
        /// Gets or sets the order by info.
        /// </summary>
        [ContentSection(ContentSectionTitles.LabelsAndContent, 5)]
        [DisplayName("Sort items")]
        [DefaultValue("Title ASC")]
        [DataType(customDataType: KnownFieldTypes.Choices)]
        [Choice(ServiceUrl = "{0}/Default.Sorters()?frontend=True", ServiceCallParameters = "[{ \"taxaUrl\" : \"{0}\"}]")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2243:Attribute string literals should parse correctly", Justification = "By design")]
        public string OrderBy { get; set; }

        /// <summary>
        /// Gets or sets the sort expression.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [DisplayName("Sort expression")]
        [Description("Custom sort expression, used if default sorting is not suitable.")]
        public string SortExpression { get; set; }

        /// <summary>
        /// Gets or sets the field size.
        /// </summary>
        [ContentSection(Constants.ContentSectionTitles.DisplaySettings, 2)]
        [DisplayName("Field size")]
        [DataType(KnownFieldTypes.ChipChoice)]
        public FieldSize FieldSize { get; set; }

        /// <summary>
        /// Gets or sets the number of columns for choice options.
        /// </summary>
        [ContentSection(Constants.ContentSectionTitles.DisplaySettings, 3)]
        [DataType(customDataType: KnownFieldTypes.Choices)]
        [ConditionalVisibility("{\"conditions\":[{\"fieldName\":\"SfViewName\",\"operator\":\"Equals\",\"value\":\"Checkboxes\"}]}")]
        [DisplayName("Layout")]
        [DefaultValue(1)]
        [Choice("[{\"Title\":\"One column\",\"Name\":\"1\",\"Value\":1},{\"Title\":\"Two columns\",\"Name\":\"2\",\"Value\":2},{\"Title\":\"Three columns\",\"Name\":\"3\",\"Value\":3},{\"Title\":\"Side by side\",\"Name\":\"0\",\"Value\":0}]")]
        public int ColumnsNumber { get; set; }

        /// <summary>
        /// Gets or sets the name of the value field.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [ContentSection("AdvancedMain")]
        [DisplayName("Value field name")]
        [Description("Name of the field that will be used to hold the value of the selection.")]
        public string ValueFieldName { get; set; }
    }
}
