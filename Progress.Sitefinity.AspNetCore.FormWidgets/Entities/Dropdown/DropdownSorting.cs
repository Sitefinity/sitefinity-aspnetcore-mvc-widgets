using System.ComponentModel;

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
        [Description("As set manually")]
        Manual,

        /// <summary>
        /// Alphabetical.
        /// </summary>
        [Description("Alphabetically")]
        Alphabetical,
    }
}
