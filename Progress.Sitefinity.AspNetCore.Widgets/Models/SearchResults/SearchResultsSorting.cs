using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        [Description("Most relevant on top")]
        MostRelevantOnTop,

        /// <summary>
        /// Newest first.
        /// </summary>
        [Description("Newest first")]
        NewestFirst,

        /// <summary>
        /// Oldest first.
        /// </summary>
        [Description("Oldest first")]
        OldestFirst,
    }
}
