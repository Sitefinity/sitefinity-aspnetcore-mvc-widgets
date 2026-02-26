using System.Collections.Generic;
using Progress.Sitefinity.AspNetCore.Models;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Results
{
    /// <summary>
    /// The view model for the Results widget.
    /// </summary>
    public class ResultsViewModel
    {
        /// <summary>
        /// Gets or sets the CSS class.
        /// </summary>
        public string CssClass { get; set; }

        /// <summary>
        /// Gets or sets the results header text.
        /// </summary>
        public string ResultsHeader { get; set; }

        /// <summary>
        /// Gets or sets the results number label.
        /// </summary>
        public string ResultsNumberLabel { get; set; }

        /// <summary>
        /// Gets or sets the page size.
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Gets or sets the search results.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Must be able to set in model.")]
        public List<ResultItemViewModel> SearchResults { get; set; }

        /// <summary>
        /// Gets or sets the attributes for the Results widget.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Must be able to set in property editor.")]
        public IDictionary<string, IList<AttributeModel>> Attributes { get; set; }
    }
}
