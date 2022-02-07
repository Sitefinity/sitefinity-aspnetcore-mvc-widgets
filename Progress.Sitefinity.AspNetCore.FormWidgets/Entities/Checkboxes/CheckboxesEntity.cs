using System.Collections.Generic;
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
        public override IList<ChoiceOption> Choices { get; set; } = new List<ChoiceOption>() { new ChoiceOption { Name = "First choice", Value = "1" }, new ChoiceOption { Name = "Second choice", Value = "2" }, new ChoiceOption { Name = "Third choice", Value = "3" } };
    }
}
