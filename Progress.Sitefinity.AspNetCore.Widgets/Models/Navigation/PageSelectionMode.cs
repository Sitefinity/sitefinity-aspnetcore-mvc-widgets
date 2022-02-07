using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Navigation
{
    /// <summary>
    /// The rendering options for the Navigation widget.
    /// </summary>
    /// <remarks>
    /// Each option describes different selection of sitemap nodes that will be included while rendering the Navigation widget.
    /// </remarks>
    public enum PageSelectionMode
    {
        /// <summary>
        /// Refers to top-level pages and all their child pages
        /// </summary>
        [Description("Top-level pages (and their child-pages if template allows)")]
        TopLevelPages,

        /// <summary>
        /// Refers to all child pages under particular page.
        /// </summary>
        [Description("All pages under particular page...")]
        SelectedPageChildren,

        /// <summary>
        /// Refers to child pages under currently opened page.
        /// </summary>
        [Description("All pages under currently opened page")]
        CurrentPageChildren,

        /// <summary>
        /// Refers to page siblings of the currently opened page.
        /// </summary>
        [Description("All sibling pages of currently opened page")]
        CurrentPageSiblings,

        /// <summary>
        /// Refers to custom selection of pages.
        /// </summary>
        [Description("Custom selection of pages...")]
        SelectedPages,
    }
}
