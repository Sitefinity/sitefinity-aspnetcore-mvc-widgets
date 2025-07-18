using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Progress.Sitefinity.AspNetCore.FormWidgets.Entities.Common;
using Progress.Sitefinity.Renderer.Contracts.Forms;
using Progress.Sitefinity.Renderer.Designers;
using Progress.Sitefinity.Renderer.Designers.Attributes;
using static Progress.Sitefinity.AspNetCore.Constants;

namespace Progress.Sitefinity.AspNetCore.FormWidgets.Entities.Dropdown
{
    /// <summary>
    /// Entity for the dropdown widget.
    /// </summary>
    public class DropdownEntity : ChoiceEntityBase
    {
        /// <inheritdoc/>
        [DefaultValue("Untitled")]
        [DataType(customDataType: "")]
        [DisplayName("Label")]
        public override string Label { get; set; }

        /// <inheritdoc/>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Entity")]
        [TableView(Selectable = true, Reorderable = true, AddManyFileName = "choices-predefined-lists.json")]
        [DefaultValue("[{\"Name\":\"Select\"},{\"Name\":\"First choice\",\"Value\":\"1\"},{\"Name\":\"Second choice\",\"Value\":\"2\"}]")]
        public override IList<ChoiceOption> Choices { get; set; }

        /// <summary>
        /// Gets or sets the sorting applied for the options.
        /// </summary>
        [ContentSection(ContentSectionTitles.LabelsAndContent, 4)]
        [DefaultValue(DropdownSorting.Manual)]
        [DisplayName("Sort choices")]
        [DataType(customDataType: KnownFieldTypes.Choices)]
        public DropdownSorting Sorting { get; set; }

        /// <summary>
        /// Gets or sets the field size.
        /// </summary>
        [ContentSection(Constants.ContentSectionTitles.DisplaySettings, 2)]
        [DisplayName("Field size")]
        [DataType(KnownFieldTypes.ChipChoice)]
        public FieldSize FieldSize { get; set; }
    }
}
