using System;
using System.Threading.Tasks;
using Progress.Sitefinity.AspNetCore.Configuration;
using Progress.Sitefinity.AspNetCore.RestSdk;
using Progress.Sitefinity.AspNetCore.Web;
using Progress.Sitefinity.AspNetCore.Widgets.ViewComponents.Common;
using Progress.Sitefinity.RestSdk;
using Progress.Sitefinity.RestSdk.Clients.Pages.Dto;
using Progress.Sitefinity.RestSdk.OData;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Search
{
    /// <summary>
    /// The model for the search box widget.
    /// </summary>
    public class SearchBoxModel : ISearchBoxModel
    {
        private readonly IStyleClassesProvider styles;
        private IODataRestClient restService;
        private ISitefinityConfig sfConfig;
        private IRequestContext requestContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchBoxModel"/> class.
        /// </summary>
        /// <param name="restService">The client for Sitefinity web services.</param>
        /// <param name="requestContext">The current request context.</param>
        /// <param name="styles">The styles provider.</param>
        /// <param name="sfConfig">The Sitefinity config.</param>
        public SearchBoxModel(
            IODataRestClient restService,
            IRequestContext requestContext,
            IStyleClassesProvider styles,
            ISitefinityConfig sfConfig)
        {
            this.restService = restService;
            this.sfConfig = sfConfig;
            this.requestContext = requestContext;
            this.styles = styles;
        }

        /// <inheritdoc/>
        public virtual async Task<SearchBoxViewModel> InitializeViewModel(SearchBoxEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var currentSite = this.requestContext.Site;
            var viewModel = new SearchBoxViewModel();

            viewModel.Attributes = entity.Attributes;
            viewModel.ViewName = entity.SfViewName;
            viewModel.CssClass = entity.CssClass;
            viewModel.SuggestionsTriggerCharCount = entity.SuggestionsTriggerCharCount;
            viewModel.SearchButtonLabel = entity.SearchButtonLabel;
            viewModel.SearchBoxPlaceholder = entity.SearchBoxPlaceholder;
            viewModel.SearchIndex = entity.SearchIndex;
            viewModel.SuggestionFields = entity.SuggestionFields;
            viewModel.WebServicePath = $"/{this.sfConfig.WebServicePath}";
            viewModel.SearchResultsPageUrl = await this.GetPageNodeUrl(entity.SearchResultsPage);
            viewModel.ShowResultsForAllIndexedSites = entity.ShowResultsForAllIndexedSites;
            viewModel.ScoringProfile = new ScoringProfile
            {
                ScoringSetting = entity.ScoringProfile ?? string.Empty,
                ScoringParameters = entity.ScoringParameters?.Count > 0 ? string.Join(';', entity.ScoringParameters) : string.Empty,
            };

            viewModel.SiteId = currentSite.Id;
            viewModel.Culture = this.requestContext.Culture.Name;

            var margins = this.styles.GetMarginsClasses(entity);
            viewModel.CssClass = (entity.CssClass + " " + margins).Trim();
            viewModel.ActiveClass = this.styles.StylingConfig.ActiveClass;
            viewModel.VisibilityClasses = this.styles.StylingConfig.VisibilityClasses;
            viewModel.SearchAutocompleteItemClass = this.styles.StylingConfig.SearchAutocompleteItemClass;

            return viewModel;
        }

        private async Task<string> GetPageNodeUrl(Sitefinity.Renderer.Entities.Content.MixedContentContext context)
        {
            if (context?.Content?[0]?.Variations?.Length != 0)
            {
                var pageNodes = await this.restService.GetItems<PageNodeDto>(context, new GetAllArgs() { Fields = new[] { nameof(PageNodeDto.ViewUrl) } });

                var items = pageNodes.Items;

                if (items.Count == 1)
                {
                    return items[0].ViewUrl;
                }
            }

            return string.Empty;
        }
    }
}
