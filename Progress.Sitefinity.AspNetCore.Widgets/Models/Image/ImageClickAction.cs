using System.ComponentModel;
using Progress.Sitefinity.Renderer.Designers.Attributes;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Image
{
    /// <summary>
    /// Available options for image click action.
    /// </summary>
    public enum ImageClickAction
    {
        /// <summary>
        /// The none.
        /// </summary>
        [EnumDisplayName("Nothing happens")]
        [Description("Nothing happens")]
        DoNothing,

        /// <summary>
        /// Opens a link.
        /// </summary>
        [EnumDisplayName("Opens a link")]
        [Description("Opens a link")]
        OpenLink,

        /// <summary>
        /// Opens image in its original size.
        /// </summary>
        [EnumDisplayName("Opens image in original size")]
        [Description("Opens image in original size")]
        OpenOriginalSize,
    }
}
