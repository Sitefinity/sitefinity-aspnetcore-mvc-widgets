using System.ComponentModel;
using Progress.Sitefinity.Renderer.Designers.Attributes;

namespace Progress.Sitefinity.AspNetCore.FormWidgets.Entities.Dropdown
{
    /// <summary>
    /// Respresents the options for sorting dropdown widget options.
    /// </summary>
    public enum DropdownSorting
    {
        /// <summary>
        /// As set manually.
        /// </summary>
        [EnumDisplayName("As set manually")]
        [Description("As set manually")]
        Manual,

        /// <summary>
        /// Alphabetical.
        /// </summary>
        [EnumDisplayName("Alphabetically")]
        [Description("Alphabetically")]
        Alphabetical,
    }
}
