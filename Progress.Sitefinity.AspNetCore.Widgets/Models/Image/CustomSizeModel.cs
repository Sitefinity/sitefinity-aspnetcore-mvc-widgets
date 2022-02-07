using System.ComponentModel;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Image
{
    /// <summary>
    /// The model for the image custom sizes.
    /// </summary>
    public class CustomSizeModel
    {
        /// <summary>
        /// Gets or sets the width. Relevant for CropCropArguments method.
        /// </summary>
        /// <value>
        /// The width.
        /// </value>
        public int? Width { get; set; }

        /// <summary>
        /// Gets or sets the height. Relevant for CropCropArguments method.
        /// </summary>
        /// <value>
        /// The height.
        /// </value>
        public int? Height { get; set; }

        /// <summary>
        /// Gets or sets the original width of the image(before it was cropped). Relevant for CropCropArguments method.
        /// </summary>
        /// <value>
        /// The width.
        /// </value>
        public int? OriginalWidth { get; set; }

        /// <summary>
        /// Gets or sets the original height(before it was cropped). Relevant for CropCropArguments method.
        /// </summary>
        /// <value>
        /// The height.
        /// </value>
        public int? OriginalHeight { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to constrain the size to proportions.
        /// </summary>
        [DefaultValue(true)]
        public bool ConstrainToProportions { get; set; }
    }
}
