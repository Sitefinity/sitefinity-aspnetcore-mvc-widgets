using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json.Linq;
using Progress.Sitefinity.AspNetCore.Widgets.Models.ContentList;
using Progress.Sitefinity.AspNetCore.Widgets.Models.ContentPager;
using Progress.Sitefinity.AspNetCore.Widgets.ViewComponents.Common;
using Progress.Sitefinity.RestSdk;
using Progress.Sitefinity.RestSdk.OData;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.SearchResults
{
    /// <summary>
    /// The model for the search results widget.
    /// </summary>
    public class SearchResultsModel : ISearchResultsModel
    {
        private readonly IODataRestClient restClient;
        private readonly IStyleClassesProvider styles;
        private IStringLocalizer<SearchResultsModel> localizer;

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchResultsModel"/> class.
        /// </summary>
        /// <param name="restClient">The rest client.</param>
        /// <param name="localizer">The localization provider.</param>
        /// <param name="styles">The styles provider.</param>
        public SearchResultsModel(
            IODataRestClient restClient,
            IStringLocalizer<SearchResultsModel> localizer,
            IStyleClassesProvider styles)
        {
            this.restClient = restClient;
            this.localizer = localizer;
            this.styles = styles;
        }

        /// <inheritdoc/>
        public async Task<SearchResultsViewModel> InitializeViewModel(SearchResultsEntity entity, SearchParamsModel searchParamsModel)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            if (searchParamsModel == null)
                throw new ArgumentNullException(nameof(searchParamsModel));

            var currentSite = await this.restClient.Sites().GetCurrentSite();
            var margins = this.styles.GetMarginsClasses(entity);
            var cssClass = (entity.CssClass + " " + margins).Trim();

            var searchResultsViewModel = new SearchResultsViewModel()
            {
                SearchResults = new List<SearchResultDocumentDto>(),
                ResultsHeader = string.Format(CultureInfo.InvariantCulture, entity.NoResultsHeader, searchParamsModel.SearchQuery),
                LanguagesLabel = entity.LanguagesLabel,
                ResultsNumberLabel = entity.ResultsNumberLabel,
                Attributes = entity.Attributes,
                CssClass = cssClass,
                Languages = currentSite.Cultures,
                AllowUsersToSortResults = entity.AllowUsersToSortResults == 1,
                Sorting = entity.Sorting,
                SoryByLabel = this.localizer.GetString("Sort by"),
            };

            if (string.IsNullOrEmpty(searchParamsModel.SearchQuery))
            {
                return searchResultsViewModel;
            }
            else
            {
                JObject response = await this.PerformSearch(entity, searchParamsModel);
                int totalCount = response[nameof(SearchResultsViewModel.TotalCount)].ToObject<int>();
                IList<SearchResultDocumentDto> searchResults = response[nameof(SearchResultsViewModel.SearchResults)].ToObject<List<SearchResultDocumentDto>>();
                searchResultsViewModel.TotalCount = totalCount;
                searchResultsViewModel.SearchResults = searchResults;

                if (entity.ListSettings.DisplayMode == ListDisplayMode.Paging)
                {
                    int pagesCount = (int)Math.Ceiling((double)searchResultsViewModel.TotalCount / (double)entity.ListSettings.ItemsPerPage);
                    searchResultsViewModel.PagesCount = pagesCount;
                    searchResultsViewModel.CurrentPage = searchParamsModel.Page;
                    searchResultsViewModel.Pager = new ContentPagerViewModel(searchParamsModel.Page, searchResultsViewModel.TotalCount, entity.ListSettings.ItemsPerPage, ContentPagerViewModel.PageNumberDefaultTemplate, ContentPagerViewModel.PageNumberDefaultQueryTemplate, PagerMode.QueryParameter);
                }

                if (searchResultsViewModel.SearchResults.Count > 0)
                {
                    searchResultsViewModel.ResultsHeader = string.Format(CultureInfo.InvariantCulture, entity.SearchResultsHeader, searchParamsModel.SearchQuery);
                }
            }

            return searchResultsViewModel;
        }

        private async Task<JObject> PerformSearch(SearchResultsEntity entity, SearchParamsModel searchParamsModel)
        {
            string orderByClause = string.IsNullOrEmpty(searchParamsModel.OrderBy) ? entity.Sorting.ToString() : searchParamsModel.OrderBy;

            if (orderByClause == SearchResultsSorting.NewestFirst.ToString())
            {
                orderByClause = "PublicationDate desc";
            }
            else if (orderByClause == SearchResultsSorting.OldestFirst.ToString())
            {
                orderByClause = "PublicationDate";
            }
            else
            {
                orderByClause = string.Empty;
            }

            int skip = 0;
            int take = 20;
            if (entity.ListSettings.DisplayMode == ListDisplayMode.Paging)
            {
                take = entity.ListSettings.ItemsPerPage;
                skip = (searchParamsModel.Page - 1) * take;
            }
            else if (entity.ListSettings.DisplayMode == ListDisplayMode.Limit)
            {
                take = entity.ListSettings.LimitItemsCount;
            }
            else if (entity.ListSettings.DisplayMode == ListDisplayMode.All)
            {
                take = int.MaxValue;
            }

            var response = await this.restClient.ExecuteUnboundFunction<JObject>(new BoundFunctionArgs()
            {
                Name = "Default.PerformSearch",
                AdditionalQueryParams = new Dictionary<string, string>()
                {
                    ["indexCatalogue"] = searchParamsModel.IndexCatalogue,
                    ["searchQuery"] = searchParamsModel.SearchQuery,
                    ["wordsMode"] = searchParamsModel.WordsMode,
                    ["orderBy"] = orderByClause,
                    ["sf_culture"] = searchParamsModel.Culture,
                    ["$skip"] = skip.ToString(CultureInfo.InvariantCulture),
                    ["$top"] = take.ToString(CultureInfo.InvariantCulture),
                    ["searchFields"] = entity.SearchFields,
                    ["highlightedFields"] = entity.HighlightedFields,
                    ["scoringInfo"] = searchParamsModel.ScroingInfo,
                },
            });

            return response;
        }
    }
}
