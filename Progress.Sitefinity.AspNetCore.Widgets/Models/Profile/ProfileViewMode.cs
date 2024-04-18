using System.ComponentModel;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Profile
{
    /// <summary>
    /// The profile view mode.
    /// </summary>
    public enum ProfileViewMode
    {
        /// <summary>
        /// Edit mode only.
        /// </summary>
        [Description("Edit mode only")]
        Edit = 0,

        /// <summary>
        /// Read mode only.
        /// </summary>
        [Description("Read mode only")]
        Read = 1,

        /// <summary>
        /// Both - read and edit mode.
        /// </summary>
        [Description("Both - read and edit mode")]
        ReadEdit = 2,
    }
}
