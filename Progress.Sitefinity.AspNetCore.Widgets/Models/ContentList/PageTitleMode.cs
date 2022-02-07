using System.ComponentModel;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.ContentList
{
    /// <summary>
    /// Respresents the options for interactions between the page title and the detail item when generating SEO meta information.
    /// </summary>
    public enum PageTitleMode
    {
        /// <summary>
        /// Replaces the title with the correspoding item's title.
        /// </summary>
        [Description("Replace")]
        Replace,

        /// <summary>
        /// Appends the item's title to the page title.
        /// </summary>
        [Description("Append")]
        Append,

        /// <summary>
        /// Adds the child item's title to the parent item's title.
        /// </summary>
        [Description("Hierarchy")]
        Hierarchy,

        /// <summary>
        /// Detail item's title is not taken into consideration.
        /// </summary>
        [Description("Do not set")]
        DoNotSet,
    }
}
