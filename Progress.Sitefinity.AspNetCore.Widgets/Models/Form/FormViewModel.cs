using System.Collections.Generic;
using Progress.Sitefinity.AspNetCore.Models;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Form
{
    /// <summary>
    /// The view model for the Form widget.
    /// </summary>
    public class FormViewModel
    {
        /// <summary>
        /// Gets or sets the CSS class of the button.
        /// </summary>
        public string CssClass { get; set; }

        /// <summary>
        /// Gets or sets the form model.
        /// </summary>
        public PageModel FormModel { get; set; }

        /// <summary>
        /// Gets or sets the form submit url.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1056:URI-like properties should not be strings", Justification = "Not needed")]
        public string SubmitUrl { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the widget has custom submit action.
        /// </summary>
        public bool CustomSubmitAction { get; set; }

        /// <summary>
        /// Gets or sets the form submit redirect url.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1056:URI-like properties should not be strings", Justification = "Not needed")]
        public string RedirectUrl { get; set; }

        /// <summary>
        /// Gets or sets the form submit success message.
        /// </summary>
        public string SuccessMessage { get; set; }

        /// <summary>
        /// Gets or sets the warning.
        /// </summary>
        public string Warning { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the data submission is skipped (used to disable submission in preview).
        /// </summary>
        public bool SkipDataSubmission { get; set; }

        /// <summary>
        /// Gets or sets the form rules.
        /// </summary>
        public string Rules { get; set; }

        /// <summary>
        /// Gets or sets the hidden fields.
        /// </summary>
        public string HiddenFields { get; set; }

        /// <summary>
        /// Gets or sets the attributes for the form.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Must be able to set in property editor.")]
        public IDictionary<string, IList<AttributeModel>> Attributes { get; set; }
    }
}
