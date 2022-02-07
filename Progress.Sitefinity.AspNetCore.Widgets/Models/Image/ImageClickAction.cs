using System.ComponentModel;

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
        [Description("Nothing happens")]
        DoNothing,

        /// <summary>
        /// Opens a link.
        /// </summary>
        [Description("Opens a link")]
        OpenLink,

        /// <summary>
        /// Opens image in its original size.
        /// </summary>
        [Description("Opens image in original size")]
        OpenOriginalSize,
    }
}
