using System.Collections.Generic;
using Newtonsoft.Json;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.SearchResults
{
    /// <summary>
    /// A single object that is part of the search results.
    /// </summary>
    [JsonConverter(typeof(SearchResultsJsonConverter))]
    public class SearchResultDocumentDto
    {
        /// <summary>
        /// Gets or sets the highlighter result.
        /// </summary>
        public string HighLighterResult { get; set; }

        /// <summary>
        /// Gets or sets the language.
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// Gets or sets the provider.
        /// </summary>
        public string Provider { get; set; }

        /// <summary>
        /// Gets or sets the link.
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the content type.
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// Gets or sets the item's original id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the url of the item's thumbnail.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1056:URI-like properties should not be strings", Justification = "Used on the front end.")]
        public string ThumbnailUrl { get; set; }

        /// <summary>
        /// Gets or sets a list with additional fields that have been indexed.
        /// </summary>
        public Dictionary<string, object> IndexedFields { get; set; } = new Dictionary<string, object>();

        /// <summary>
        /// Retrieves a value from the Indexed fields.
        /// </summary>
        /// <param name="indexedFieldName">The name of the indexed field.</param>
        /// <returns>The value of the indexed field. </returns>
        public object GetValue(string indexedFieldName)
        {
            if (this.IndexedFields.TryGetValue(indexedFieldName, out object fieldValue))
            {
                return fieldValue;
            }

            return null;
        }
    }
}
