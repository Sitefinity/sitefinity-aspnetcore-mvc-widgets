namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Results
{
    /// <summary>
    /// Represents a single search result item.
    /// </summary>
    public class ResultItemViewModel
    {
        /// <summary>
        /// Gets or sets the title of the result.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the link to the result.
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// Gets or sets the order of the result.
        /// </summary>
        internal int Order { get; set; }
    }
}
