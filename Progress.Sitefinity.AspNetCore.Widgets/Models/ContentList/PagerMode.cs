using System.ComponentModel;
using Progress.Sitefinity.Renderer.Designers.Attributes;

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
        [EnumDisplayName("URL segments")]
        [Description("URL segments")]
        URLSegments,

        /// <summary>
        /// Pager mode with query parameters.
        /// </summary>
        [EnumDisplayName("Query parameter")]
        [Description("Query parameter")]
        QueryParameter,
    }
}
