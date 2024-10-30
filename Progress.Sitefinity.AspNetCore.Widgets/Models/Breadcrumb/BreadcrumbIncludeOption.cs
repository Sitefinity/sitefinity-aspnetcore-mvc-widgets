using System.ComponentModel;
using Progress.Sitefinity.Renderer.Designers.Attributes;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Breadcrumb
{
    /// <summary>
    /// Enum that controls the breadcrumb generation logic.
    /// </summary>
    public enum BreadcrumbIncludeOption
    {
        /// <summary>
        /// Refers to full path to the current page.
        /// </summary>
        [EnumDisplayName("Full path to the current page")]
        [Description("Full path to the current page")]
        CurrentPageFullPath,

        /// <summary>
        /// Refers to path starting from a specifi page.
        /// </summary>
        [EnumDisplayName("Path starting from a specific page...")]
        [Description("Path starting from a specific page...")]
        SpecificPagePath,
    }
}
