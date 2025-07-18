using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Progress.Sitefinity.AspNetCore.ViewComponents.AttributeConfigurator.Attributes;
using Progress.Sitefinity.Renderer.Contracts.Forms;
using Progress.Sitefinity.Renderer.Designers;
using Progress.Sitefinity.Renderer.Designers.Attributes;
using static Progress.Sitefinity.AspNetCore.Constants;

namespace Progress.Sitefinity.AspNetCore.FormWidgets.Entities.Common
{
    /// <summary>
    /// Base entity that holds the proprties for choice fields.
    /// </summary>
    [SectionsOrder(ContentSectionTitles.LabelsAndContent, ContentSectionTitles.DisplaySettings)]
    public class ChoiceEntityBase : IChoiceFieldContract
    {
        /// <inheritdoc/>
        [DefaultValue("Select a choice")]
        [ContentSection(ContentSectionTitles.LabelsAndContent, 1)]
        [DisplayName("Label or question")]
        [DataType(customDataType: KnownFieldTypes.TextArea)]
        public virtual string Label { get; set; }

        /// <summary>
        /// Gets or sets the instructional text of the widget.
        /// </summary>
        [ContentSection(ContentSectionTitles.LabelsAndContent, 2)]
        [DisplayName("Instructional text")]
        [Description("Suitable for giving examples how the entered value will be used.")]
        public string InstructionalText { get; set; }

        /// <inheritdoc/>
        [ContentSection(ContentSectionTitles.LabelsAndContent, 3)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Entity")]
        [TableView(Selectable = true, Reorderable = true)]
        [DefaultValue("[{\"Name\":\"First choice\",\"Value\":\"1\"},{\"Name\":\"Second choice\",\"Value\":\"2\"},{\"Name\":\"Third choice\",\"Value\":\"3\"}]")]
        public virtual IList<ChoiceOption> Choices { get; set; }

        /// <inheritdoc/>
        [ContentSection(ContentSectionTitles.LabelsAndContent, 5)]
        [DisplayName("Required field")]
        [DataType(customDataType: KnownFieldTypes.CheckBox)]
        [Group("Options")]
        public bool Required { get; set; }

        /// <inheritdoc/>
        [ContentSection(ContentSectionTitles.LabelsAndContent, 6)]
        [DisplayName("Hide field initially (use form rules to display it)")]
        [DataType(customDataType: KnownFieldTypes.CheckBox)]
        [Group("Options")]
        public bool Hidden { get; set; }

        /// <inheritdoc/>
        [ContentSection(ContentSectionTitles.LabelsAndContent, 7)]
        [DisplayName("Error message if choice is not selected")]
        [DefaultValue("{0} field is required")]
        [ConditionalVisibility("{\"conditions\":[{\"fieldName\":\"Required\",\"operator\":\"Equals\",\"value\":true}]}")]
        public string RequiredErrorMessage { get; set; }

        /// <summary>
        /// Gets or sets the view name.
        /// </summary>
        [ViewSelector]
        [ContentSection(Constants.ContentSectionTitles.DisplaySettings, 1)]
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
    }
}
