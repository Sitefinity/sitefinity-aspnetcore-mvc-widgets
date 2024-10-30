using System.ComponentModel;
using Progress.Sitefinity.Renderer.Designers.Attributes;

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
        [EnumDisplayName("Original size")]
        [Description("Original size")]
        OriginalSize,

        /// <summary>
        /// Use thumbnail.
        /// </summary>
        [EnumDisplayName("Use thumbnail")]
        [Description("Use thumbnail")]
        Thumbnail,

        /// <summary>
        /// Custom size.
        /// </summary>
        [EnumDisplayName("Custom size")]
        [Description("Custom size")]
        CustomSize,
    }
}
