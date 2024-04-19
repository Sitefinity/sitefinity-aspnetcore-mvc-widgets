using System.Collections.Generic;
using Progress.Sitefinity.AspNetCore.Models;
using Progress.Sitefinity.AspNetCore.Widgets.Models.ContentPager;
using static Progress.Sitefinity.RestSdk.Clients.Sites.Dto.SiteDto;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.SearchResults
{
    /// <summary>
    /// The view model for the Search results widget.
    /// </summary>
    public class SearchResultsViewModel
    {
        /// <summary>
        /// Gets or sets the total count of the serach results.
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// Gets or sets the search results.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Needed")]
        public IList<SearchResultDocumentDto> SearchResults { get; set; }

        /// <summary>
        /// Gets or sets the pages count.
        /// </summary>
        public int PagesCount { get; set; }

        /// <summary>
        /// Gets or sets the current page.
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// Gets or sets the pager view model.
        /// </summary>
        public ContentPagerViewModel Pager { get; set; }

        /// <summary>
        /// Gets or sets the search results header.
        /// </summary>
        public string ResultsHeader { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the users are allowed to sort results.
        /// </summary>
        public bool AllowUsersToSortResults { get; set; }

        /// <summary>
        /// Gets or sets the results number label.
        /// </summary>
        public string ResultsNumberLabel { get; set; }

        /// <summary>
        /// Gets or sets the current site languages.
        /// </summary>
        public IEnumerable<CultureDto> Languages { get; set; }

        /// <summary>
        /// Gets or sets the value for the default results sorting.
        /// </summary>
        public SearchResultsSorting Sorting { get; set; }

        /// <summary>
        /// Gets or sets the languages label.
        /// </summary>
        public string LanguagesLabel { get; set; }

        /// <summary>
        /// Gets or sets the CSS class of the wrapper.
        /// </summary>
        public string CssClass { get; set; }

        /// <summary>
        /// Gets or sets the attributes for the form.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Must be able to set in property editor.")]
        public IDictionary<string, IList<AttributeModel>> Attributes { get; set; }

        /// <summary>
        /// Gets or sets the Sort by label.
        /// </summary>
        public string SortByLabel { get; set; }
    }
}
