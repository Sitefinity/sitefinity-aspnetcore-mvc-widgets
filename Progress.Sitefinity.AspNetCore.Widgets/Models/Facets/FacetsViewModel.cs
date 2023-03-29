using System.Collections.Generic;
using Progress.Sitefinity.AspNetCore.Models;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Facets
{
    /// <summary>
    /// Model for displaying search facets.
    /// </summary>
    public class FacetsViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FacetsViewModel"/> class.
        /// </summary>
        public FacetsViewModel()
        {
            this.SearchFacets = new List<SearchFacets>();
        }

        /// <summary>
        /// Gets or sets the list of search facets.
        /// </summary>
        public IList<SearchFacets> SearchFacets { get; set; }

        /// <summary>
        /// Gets or sets the clear all label.
        /// </summary>
        public string ClearAllLabel { get; set; }

        /// <summary>
        /// Gets or sets the filter results label.
        /// </summary>
        public string FilterResultsLabel { get; set; }

        /// <summary>
        /// Gets or sets the applied to label.
        /// </summary>
        public string AppliedFiltersLabel { get; set; }

        /// <summary>
        /// Gets or sets the show more label.
        /// </summary>
        public string ShowMoreLabel { get; set; }

        /// <summary>
        /// Gets or sets the show less label.
        /// </summary>
        public string ShowLessLabel { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the button is active or not.
        /// </summary>
        public bool IsShowMoreLessButtonActive { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the facets are displyed with their count.
        /// </summary>
        public bool DisplayItemCount { get; set; }

        /// <summary>
        /// Gets or sets the custom css class name.
        /// </summary>
        public string CssClass { get; set; }

        /// <summary>
        /// Gets or sets the attributes for the search facets.
        /// </summary>
        public IDictionary<string, IList<AttributeModel>> Attributes { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether there are any facet elements.
        /// </summary>
        public bool HasAnyFacetElements { get; set; }

        /// <summary>
        /// Gets or sets the index catalogue.
        /// </summary>
        public string IndexCatalogue { get; set; }
    }
}
