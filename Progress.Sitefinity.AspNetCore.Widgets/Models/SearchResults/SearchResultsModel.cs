using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json.Linq;
using Progress.Sitefinity.AspNetCore.RestSdk;
using Progress.Sitefinity.AspNetCore.Web;
using Progress.Sitefinity.AspNetCore.Widgets.Models.ContentList;
using Progress.Sitefinity.AspNetCore.Widgets.Models.ContentPager;
using Progress.Sitefinity.AspNetCore.Widgets.ViewComponents.Common;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.SearchResults
{
    /// <summary>
    /// The model for the search results widget.
    /// </summary>
    public class SearchResultsModel : ISearchResultsModel
    {
        private readonly ISearchClient searchClient;
        private readonly IRequestContext requestContext;
        private readonly IStyleClassesProvider styles;
        private IStringLocalizer<SearchResultsModel> localizer;

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchResultsModel"/> class.
        /// </summary>
        /// <param name="requestContext">The request context.</param>
        /// <param name="restClient">The rest client.</param>
        /// <param name="localizer">The localization provider.</param>
        /// <param name="styles">The styles provider.</param>
        public SearchResultsModel(
            IRequestContext requestContext,
            ISearchClient restClient,
            IStringLocalizer<SearchResultsModel> localizer,
            IStyleClassesProvider styles)
        {
            this.requestContext = requestContext;
            this.searchClient = restClient;
            this.localizer = localizer;
            this.styles = styles;
        }

        /// <inheritdoc/>
        public virtual async Task<SearchResultsViewModel> InitializeViewModel(SearchResultsEntity entity, SearchParamsModel searchParamsModel, HttpContext httpContext)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            if (searchParamsModel == null)
                throw new ArgumentNullException(nameof(searchParamsModel));
            if (httpContext == null)
                throw new ArgumentNullException(nameof(httpContext));

            httpContext.AddVaryByQueryParams(new[] { "searchQuery", "page", "sf_culture", "orderBy", "filter" });

            var currentSite = this.requestContext.Site;
            var languages = currentSite.Cultures.Select(x => new Sitefinity.RestSdk.Clients.Sites.Dto.SiteDto.CultureDto()
            {
                Name = x.Name,
                DisplayName = CultureInfo.GetCultureInfo(x.Name).DisplayName,
            });

            var margins = this.styles.GetMarginsClasses(entity);
            var cssClass = (entity.CssClass + " " + margins).Trim();

            var searchResultsViewModel = new SearchResultsViewModel()
            {
                SearchResults = new List<SearchResultDocumentDto>(),
                ResultsHeader = string.Format(CultureInfo.InvariantCulture, entity.NoResultsHeader, searchParamsModel.SearchQuery),
                LanguagesLabel = entity.LanguagesLabel,
                ResultsNumberLabel = entity.ResultsNumberLabel,
                SortByLabel = entity.SortByLabel,
                Attributes = entity.Attributes,
                CssClass = cssClass,
                Languages = languages,
                AllowUsersToSortResults = entity.AllowUsersToSortResults == 1,
                Sorting = entity.Sorting,
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

            var response = await this.searchClient.Search<JObject>(new SearchArgs()
            {
                IndexName = searchParamsModel.IndexCatalogue,
                SearchQuery = searchParamsModel.SearchQuery,
                WordsMode = searchParamsModel.WordsMode,
                OrderByClause = orderByClause,
                Culture = searchParamsModel.Culture,
                Skip = skip,
                Take = take,
                SearchFields = entity.SearchFields,
                HighlightedFields = entity.HighlightedFields,
                ScroingInfo = searchParamsModel.ScroingInfo,
                ShowResultsForAllIndexedSites = searchParamsModel.ShowResultsForAllIndexedSites,
                Filter = searchParamsModel.Filter,
                AdditionalResultFields = entity.AdditionalResultFields,
            });

            return response;
        }
}
}
