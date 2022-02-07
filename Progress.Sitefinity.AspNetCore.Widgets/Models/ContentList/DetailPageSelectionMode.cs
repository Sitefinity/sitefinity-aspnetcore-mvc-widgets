using System;
using System.Collections.Generic;
using System.ComponentModel;

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
        [Description("Auto-generated page - same layout as the list page")]
        SamePage,

        /// <summary>
        /// Option for opening the detail item in another existing page.
        /// </summary>
        [Description("Select existing page")]
        ExistingPage,
    }
}
