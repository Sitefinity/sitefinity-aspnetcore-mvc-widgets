using System;
using System.Collections.Generic;
using System.ComponentModel;
using Progress.Sitefinity.Renderer.Designers.Attributes;

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
        [EnumDisplayName("Top-level pages (and their child-pages if template allows)")]
        [Description("Top-level pages (and their child-pages if template allows)")]
        TopLevelPages,

        /// <summary>
        /// Refers to all child pages under particular page.
        /// </summary>
        [EnumDisplayName("All pages under particular page...")]
        [Description("All pages under particular page...")]
        SelectedPageChildren,

        /// <summary>
        /// Refers to child pages under currently opened page.
        /// </summary>
        [EnumDisplayName("All pages under currently opened page")]
        [Description("All pages under currently opened page")]
        CurrentPageChildren,

        /// <summary>
        /// Refers to page siblings of the currently opened page.
        /// </summary>
        [EnumDisplayName("All sibling pages of currently opened page")]
        [Description("All sibling pages of currently opened page")]
        CurrentPageSiblings,

        /// <summary>
        /// Refers to custom selection of pages.
        /// </summary>
        [EnumDisplayName("Custom selection of pages...")]
        [Description("Custom selection of pages...")]
        SelectedPages,
    }
}
