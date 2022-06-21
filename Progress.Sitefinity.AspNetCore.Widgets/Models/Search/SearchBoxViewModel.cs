using System.Collections.Generic;
using Progress.Sitefinity.AspNetCore.Models;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Search
{
    /// <summary>
    /// The view model for the Search box widget.
    /// </summary>
    public class SearchBoxViewModel
    {
        /// <summary>
        /// Gets or sets the page that would display the search results.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1056:URI-like properties should not be strings", Justification = "Readibility")]
        public string SearchResultsPageUrl { get; set; }

        /// <summary>
        /// Gets or sets the symbol count after which suggestions are requested.
        /// </summary>
        public int? SuggestionsTriggerCharCount { get; set; }

        /// <summary>
        /// Gets or sets the scoring profile when Azure search is used.
        /// </summary>
        public ScoringProfile ScoringProfile { get; set; }

        /// <summary>
        /// Gets or sets the seach button text.
        /// </summary>
        public string SearchButtonLabel { get; set; }

        /// <summary>
        /// Gets or sets the seach input placeholder text.
        /// </summary>
        public string SearchBoxPlaceholder { get; set; }

        /// <summary>
        /// Gets or sets the view name.
        /// </summary>
        public string ViewName { get; set; }

        /// <summary>
        /// Gets or sets the custom css class name.
        /// </summary>
        public string CssClass { get; set; }

        /// <summary>
        /// Gets or sets the seach index to be used.
        /// </summary>
        public string SearchIndex { get; set; }

        /// <summary>
        /// Gets or sets the search culture.
        /// </summary>
        public string Culture { get; set; }

        /// <summary>
        /// Gets or sets the site id.
        /// </summary>
        public string SiteId { get; set; }

        /// <summary>
        /// Gets or sets the suggestion fields.
        /// </summary>
        public string SuggestionFields { get; set; }

        /// <summary>
        /// Gets or sets the path of the Sitefinity web service.
        /// </summary>
        public string WebServicePath { get; set; }

        /// <summary>
        /// Gets or sets the attributes for the search box.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Must be able to set in property editor.")]
        public IDictionary<string, IList<AttributeModel>> Attributes { get; set; }
    }
}
