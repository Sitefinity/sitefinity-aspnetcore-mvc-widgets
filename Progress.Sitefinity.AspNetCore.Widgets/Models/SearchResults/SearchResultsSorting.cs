using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Progress.Sitefinity.Renderer.Designers.Attributes;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.SearchResults
{
    /// <summary>
    /// Respresents the options for sorting search results widget options.
    /// </summary>
    public enum SearchResultsSorting
    {
        /// <summary>
        /// Most relevant on top.
        /// </summary>
        [EnumDisplayName("Most relevant on top")]
        [Description("Most relevant on top")]
        MostRelevantOnTop,

        /// <summary>
        /// Newest first.
        /// </summary>
        [EnumDisplayName("Newest first")]
        [Description("Newest first")]
        NewestFirst,

        /// <summary>
        /// Oldest first.
        /// </summary>
        [EnumDisplayName("Oldest first")]
        [Description("Oldest first")]
        OldestFirst,
    }
}
