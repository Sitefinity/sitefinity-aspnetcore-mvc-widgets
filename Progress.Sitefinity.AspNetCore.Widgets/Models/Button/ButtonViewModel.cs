using System.Collections.Generic;
using Progress.Sitefinity.AspNetCore.Models;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Button
{
    /// <summary>
    /// The view model for the image widget.
    /// </summary>
    public class ButtonViewModel
    {
        /// <summary>
        /// Gets or sets the action label for the primary action.
        /// </summary>
        public string PrimaryActionLabel { get; set; }

        /// <summary>
        /// Gets or sets the action link for the primary action.
        /// </summary>
        public string PrimaryActionHref { get; set; }

        /// <summary>
        /// Gets or sets the action label for the secondary action.
        /// </summary>
        public string SecondaryActionLabel { get; set; }

        /// <summary>
        /// Gets or sets the action link for the secondary action.
        /// </summary>
        public string SecondaryActionHref { get; set; }

        /// <summary>
        /// Gets or sets the CSS class of the button.
        /// </summary>
        public string CssClass { get; set; }

        /// <summary>
        /// Gets or sets the CSS class of the primary button.
        /// </summary>
        public string PrimaryButtonCssClass { get; set; }

        /// <summary>
        /// Gets or sets the CSS class of the secondary button.
        /// </summary>
        public string SecondaryButtonCssClass { get; set; }

        /// <summary>
        /// Gets or sets the attributes for the button.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Must be able to set in property editor.")]
        public IDictionary<string, IList<AttributeModel>> Attributes { get; set; }
    }
}
