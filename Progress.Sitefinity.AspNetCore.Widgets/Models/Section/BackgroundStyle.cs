using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Progress.Sitefinity.AspNetCore.Models;
using Progress.Sitefinity.Renderer.Designers;
using Progress.Sitefinity.Renderer.Designers.Attributes;
using Progress.Sitefinity.RestSdk.Dto;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Section
{
    /// <summary>
    /// Provides style information for the background.
    /// </summary>
    public class BackgroundStyle
    {
        /// <summary>
        /// Gets or sets the background type.
        /// </summary>
        [DisplayName("Type")]
        [DataType(customDataType: KnownFieldTypes.ChipChoice)]
        public Background BackgroundType { get; set; }

        /// <summary>
        /// Gets or sets the color style.
        /// </summary>
        [DisplayName("Value")]
        [DataType(customDataType: KnownFieldTypes.Color)]
        [DefaultValue(SimpleBackgroundStyle.DefaultColor)]
        [ConditionalVisibility("{\"conditions\":[{\"fieldName\":\"BackgroundType\",\"operator\":\"Equals\",\"value\":\"Color\"}]}")]
        public string Color { get; set; }

        /// <summary>
        /// Gets or sets the image.
        /// </summary>
        [DisplayName("Value")]
        [ConditionalVisibility("{\"conditions\":[{\"fieldName\":\"BackgroundType\",\"operator\":\"Equals\",\"value\":\"Image\"}]}")]
        [MediaItem("images", false)]
        [DataType(customDataType: "media")]
        public SdkItem ImageItem { get; set; }

        /// <summary>
        /// Gets or sets the video.
        /// </summary>
        [DisplayName("Value")]
        [MediaItem("videos", false)]
        [DataType(customDataType: "media")]
        [ConditionalVisibility("{\"conditions\":[{\"fieldName\":\"BackgroundType\",\"operator\":\"Equals\",\"value\":\"Video\"}]}")]
        public SdkItem VideoItem { get; set; }

        /// <summary>
        /// Gets or sets the position of the image.
        /// </summary>
        [DisplayName(" ")]
        [ConditionalVisibility("{\"conditions\":[{\"fieldName\":\"BackgroundType\",\"operator\":\"Equals\",\"value\":\"Image\"}]}")]
        public Position ImagePosition { get; set; }
    }

    /// <summary>
    /// Enum with available backgrounds.
    /// </summary>
    public enum Background
    {
        /// <summary>
        /// No backgorund.
        /// </summary>
        None,

        /// <summary>
        /// Color.
        /// </summary>
        Color,

        /// <summary>
        /// Image.
        /// </summary>
        Image,

        /// <summary>
        /// Video.
        /// </summary>
        Video,
    }

    /// <summary>
    /// Enum with available paddings and margins.
    /// </summary>
    public enum Position
    {
        /// <summary>
        /// Fill.
        /// </summary>
        Fill,

        /// <summary>
        /// Center.
        /// </summary>
        Center,

        /// <summary>
        /// Cover.
        /// </summary>
        Cover,
    }
}
