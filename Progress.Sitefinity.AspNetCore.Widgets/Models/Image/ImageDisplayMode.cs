using System.ComponentModel;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Image
{
    /// <summary>
    /// The enum with the sizing modes of an image.
    /// </summary>
    public enum ImageDisplayMode
    {
        /// <summary>
        /// The responsive.
        /// </summary>
        Responsive,

        /// <summary>
        /// Original size.
        /// </summary>
        [Description("Original size")]
        OriginalSize,

        /// <summary>
        /// Use thumbnail.
        /// </summary>
        [Description("Use thumbnail")]
        Thumbnail,

        /// <summary>
        /// Custom size.
        /// </summary>
        [Description("Custom size")]
        CustomSize,
    }
}
