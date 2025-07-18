using System.Collections.Generic;
using System.ComponentModel;
using Progress.Sitefinity.AspNetCore.FormWidgets.Entities.MultipleChoice;
using Progress.Sitefinity.Renderer.Contracts.Forms;
using Progress.Sitefinity.Renderer.Designers.Attributes;

namespace Progress.Sitefinity.AspNetCore.FormWidgets.Entities.Checkboxes
{
    /// <summary>
    /// Entity for the checkboxes choice widget.
    /// </summary>
    public class CheckboxesEntity : MultipleChoiceEntity
    {
        /// <inheritdoc/>
        [TableView(Selectable = true, Reorderable = true, MultipleSelect = true)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Entity")]
        [DefaultValue("[{\"Name\":\"First choice\",\"Value\":\"1\"},{\"Name\":\"Second choice\",\"Value\":\"2\"},{\"Name\":\"Third choice\",\"Value\":\"3\"}]")]
        public override IList<ChoiceOption> Choices { get; set; }
    }
}
