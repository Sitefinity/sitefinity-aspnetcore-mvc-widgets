using System.ComponentModel;

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
        [Description("Full path to the current page")]
        CurrentPageFullPath,

        /// <summary>
        /// Refers to path starting from a specifi page.
        /// </summary>
        [Description("Path starting from a specific page...")]
        SpecificPagePath,
    }
}
