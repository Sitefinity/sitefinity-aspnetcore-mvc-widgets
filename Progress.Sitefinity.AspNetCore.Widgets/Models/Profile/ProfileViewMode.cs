using System.ComponentModel;
using Progress.Sitefinity.Renderer.Designers.Attributes;

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
        [EnumDisplayName("Edit mode only")]
        [Description("Edit mode only")]
        Edit = 0,

        /// <summary>
        /// Read mode only.
        /// </summary>
        [EnumDisplayName("Read mode only")]
        [Description("Read mode only")]
        Read = 1,

        /// <summary>
        /// Both - read and edit mode.
        /// </summary>
        [EnumDisplayName("Both - read and edit mode")]
        [Description("Both - read and edit mode")]
        ReadEdit = 2,
    }
}
