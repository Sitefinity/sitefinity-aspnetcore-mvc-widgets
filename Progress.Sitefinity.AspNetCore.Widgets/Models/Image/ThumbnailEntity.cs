namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Image
{
    /// <summary>
    /// The entity for the Image widget. Contains all of the data persited in the database.
    /// </summary>
    public class ThumbnailEntity
    {
        /// <summary>
        /// Gets or sets the thumbnail profile's name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the thumbnail profile's title.
        /// </summary>
        /// <value>The name.</value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the generated URL for this thumbnial profile.
        /// </summary>
        /// <value>The URL.</value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1056:Uri properties should not be strings", Justification = "Compatability.")]
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the original URL for the image.
        /// </summary>
        /// <value>The URL.</value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1056:Uri properties should not be strings", Justification = "Compatability.")]
        public string OriginalUrl { get; set; }

        /// <summary>
        /// Gets or sets the width of the thumbnail. It's calculated in Sitefinity.
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Gets or sets the heigh of the thumbnail. It's calculated in Sitefinity.
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Gets or sets mime type of the thumbnail.
        /// </summary>
        public string MimeType { get; set; }
    }
}
