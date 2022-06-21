using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Progress.Sitefinity.AspNetCore.Widgets.Models.SearchResults;

namespace Progress.Sitefinity.AspNetCore.Widgets.ViewComponents
{
    /// <summary>
    /// The view compnent for Section widget.
    /// </summary>
    [SitefinityWidget(Title = "Search results", Order = 0, Section = WidgetSection.SearchAndClassification, EmptyIconText = "Search results", EmptyIconAction = EmptyLinkAction.Edit, EmptyIcon = "search", Category = WidgetCategory.NavigationAndSearch)]
    [ViewComponent(Name = "SitefinitySearchResults")]
    public class SearchResultsViewComponent : ViewComponent
    {
        private ISearchResultsModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchResultsViewComponent"/> class.
        /// </summary>
        /// <param name="model">The <see cref="ISearchResultsModel"/> model.</param>
        public SearchResultsViewComponent(ISearchResultsModel model)
        {
            this.model = model;
        }

        /// <summary>
        /// Invokes the search results widget creation.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public virtual async Task<IViewComponentResult> InvokeAsync(IViewComponentContext<SearchResultsEntity> context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var searchParamsModel = this.GetSearchParams();
            var viewModel = await this.model.InitializeViewModel(context.Entity, searchParamsModel);
            return this.View(context.Entity.SfViewName, viewModel);
        }

        private SearchParamsModel GetSearchParams()
        {
            string indexCatalogue = this.HttpContext.Request.Query["indexCatalogue"];
            string searchQuery = this.HttpContext.Request.Query["searchQuery"];
            string wordsMode = this.HttpContext.Request.Query["wordsMode"];
            string orderBy = this.HttpContext.Request.Query["orderBy"];
            string culture = this.HttpContext.Request.Query["sf_culture"];
            string pageParam = this.HttpContext.Request.Query["page"];
            string scoringProfile = this.HttpContext.Request.Query["scoringInfo"];
            int page = 1;
            if (!string.IsNullOrEmpty(pageParam) && int.TryParse(pageParam, out int pageNumber) && pageNumber > 0)
            {
                page = pageNumber;
            }

            var searchParams = new SearchParamsModel()
            {
                IndexCatalogue = indexCatalogue,
                SearchQuery = searchQuery,
                WordsMode = wordsMode,
                OrderBy = orderBy,
                Culture = culture,
                Page = page,
                ScroingInfo = scoringProfile,
            };

            return searchParams;
        }
    }
}
