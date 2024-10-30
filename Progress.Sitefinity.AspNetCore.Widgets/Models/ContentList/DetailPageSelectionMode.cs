using System;
using System.Collections.Generic;
using System.ComponentModel;
using Progress.Sitefinity.Renderer.Designers.Attributes;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.ContentList
{
    /// <summary>
    /// The detail page selection mode.
    /// </summary>
    /// <remarks>
    /// Each option describes different option for opening detail of content items inside the content widget.
    /// </remarks>
    public enum DetailPageSelectionMode
    {
        /// <summary>
        /// Option for opening the detail item in the same page.
        /// </summary>
        [EnumDisplayName("Auto-generated page - same layout as the list page")]
        [Description("Auto-generated page - same layout as the list page")]
        SamePage,

        /// <summary>
        /// Option for opening the detail item in another existing page.
        /// </summary>
        [EnumDisplayName("Select existing page")]
        [Description("Select existing page")]
        ExistingPage,
    }
}
