using System.Collections.Generic;
using Progress.Sitefinity.AspNetCore.Models;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.LanguageSelector
{
    /// <summary>
    /// View model for the language selector component.
    /// Contains a list of available languages and customization options for rendering.
    /// </summary>
    public class LanguageSelectorViewModel
    {
        /// <summary>
        /// Gets or sets the missing translation action.
        /// </summary>
        public MissingTranslationAction LanguageSelectorLinkAction { get; set; }

        /// <summary>
        /// Gets the list of available languages.
        /// </summary>
        public IList<LanguageEntry> Languages { get; } = new List<LanguageEntry>();

        /// <summary>
        /// Gets or sets the custom CSS class name to apply to the component.
        /// </summary>
        public string CssClass { get; set; }

        /// <summary>
        /// Gets or sets the name of the view to render.
        /// </summary>
        public string ViewName { get; set; }

        /// <summary>
        /// Gets or sets the attributes for the widget.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "By design")]
        public IDictionary<string, IList<AttributeModel>> Attributes { get; set; }
    }
}
