namespace Progress.Sitefinity.AspNetCore.Widgets.Models.SearchResults
{
    /// <summary>
    /// The class that holds information about the search parameters.
    /// </summary>
    public class SearchParamsModel
    {
        /// <summary>
        /// Gets or sets the name of the search index.
        /// </summary>
        public string IndexCatalogue { get; set; }

        /// <summary>
        /// Gets or sets the search parameter.
        /// </summary>
        public string SearchQuery { get; set; }

        /// <summary>
        /// Gets or sets the words mode.
        /// </summary>
        public string WordsMode { get; set; }

        /// <summary>
        /// Gets or sets the order by clause.
        /// </summary>
        public string OrderBy { get; set; }

        /// <summary>
        /// Gets or sets the language.
        /// </summary>
        public string Culture { get; set; }

        /// <summary>
        /// Gets or sets the page number.
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Gets or sets the base64 encoded scroing information for Azure.
        /// </summary>
        public string ScroingInfo { get; set; }

        /// <summary>
        /// Gets or sets the value if search results should be provided for all indexed sites or only for the current site.
        /// </summary>
        public int ShowResultsForAllIndexedSites { get; set; }
    }
}
