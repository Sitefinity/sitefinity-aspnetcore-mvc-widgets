using System.Collections.Generic;
using Progress.Sitefinity.Renderer.Contracts.Forms;

namespace Progress.Sitefinity.AspNetCore.FormWidgets.ViewModels.Common
{
    /// <summary>
    /// The base view model for the choice field widgets.
    /// </summary>
    public class ChoiceViewModelBase
    {
        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// Gets or sets the instructional text.
        /// </summary>
        public string InstructionalText { get; set; }

        /// <summary>
        /// Gets or sets the choices.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Entity")]
        public IList<ChoiceOption> Choices { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this field is required.
        /// </summary>
        public bool Required { get; set; }

        /// <summary>
        /// Gets or sets the violation restrictions messages.
        /// </summary>
        public string ViolationRestrictionsMessages { get; set; }

        /// <summary>
        /// Gets or sets the field name.
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// Gets or sets the CSS class.
        /// </summary>
        public string CssClass { get; set; }
    }
}
