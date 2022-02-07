using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Progress.Sitefinity.AspNetCore.FormWidgets.Entities.Common;
using Progress.Sitefinity.Renderer.Contracts.Forms;
using Progress.Sitefinity.Renderer.Designers;
using Progress.Sitefinity.Renderer.Designers.Attributes;
using static Progress.Sitefinity.AspNetCore.Constants;

namespace Progress.Sitefinity.AspNetCore.FormWidgets.Entities.MultipleChoice
{
    /// <summary>
    /// Entity for the multiple choice widget.
    /// </summary>
    public class MultipleChoiceEntity : ChoiceEntityBase, IHasAdditionalChoiceFieldContract
    {
        /// <inheritdoc/>
        [ContentSection(ContentSectionTitles.LabelsAndContent, 4)]
        [DisplayName("Add \"Other\" as a last choice (expanding a text box)")]
        [DataType(customDataType: KnownFieldTypes.CheckBox)]
        [Group("Options")]
        public virtual bool HasAdditionalChoice { get; set; }

        /// <summary>
        /// Gets or sets the number of columns for choice options.
        /// </summary>
        [ContentSection(Constants.ContentSectionTitles.DisplaySettings, 2)]
        [DataType(customDataType: KnownFieldTypes.Choices)]
        [DisplayName("Layout")]
        [DefaultValue(1)]
        [Choice("[{\"Title\":\"One column\",\"Name\":\"1\",\"Value\":1},{\"Title\":\"Two columns\",\"Name\":\"2\",\"Value\":2},{\"Title\":\"Three columns\",\"Name\":\"3\",\"Value\":3},{\"Title\":\"Side by side\",\"Name\":\"0\",\"Value\":0}]")]
        public int ColumnsNumber { get; set; }
    }
}
