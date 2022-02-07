using System.ComponentModel;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.ContentList
{
    /// <summary>
    /// Represents the available pager modes.
    /// </summary>
    public enum PagerMode
    {
        /// <summary>
        /// Query mode with URL segments.
        /// </summary>
        [Description("URL segments")]
        URLSegments,

        /// <summary>
        /// Pager mode with query parameters.
        /// </summary>
        [Description("Query parameter")]
        QueryParameter,
    }
}
