using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Progress.Sitefinity.AspNetCore.Widgets.ViewComponents.Common;
using Progress.Sitefinity.RestSdk;
using Progress.Sitefinity.RestSdk.Dto;
using Progress.Sitefinity.RestSdk.OData;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Facets
{
    /// <summary>
    /// The model for the facets widget.
    /// </summary>
    public class FacetsModel : IFacetsModel
    {
        private readonly IODataRestClient restClient;
        private readonly IStyleClassesProvider styles;

        /// <summary>
        /// Initializes a new instance of the <see cref="FacetsModel"/> class.
        /// </summary>
        /// <param name="restClient">The rest client.</param>
        /// <param name="styles">The styles provider.</param>
        public FacetsModel(IODataRestClient restClient, IStyleClassesProvider styles)
        {
            this.restClient = restClient;
            this.styles = styles;
        }

        /// <inheritdoc/>
        public async Task<FacetsViewModel> InitializeViewModel(FacetsEntity entity, HttpContext httpContext)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            if (httpContext == null)
                throw new ArgumentNullException(nameof(httpContext));

            var viewModel = new FacetsViewModel();
            viewModel.IndexCatalogue = entity.IndexCatalogue;
            viewModel.Attributes = entity.Attributes;
            viewModel.AppliedFiltersLabel = entity.AppliedFiltersLabel;
            viewModel.ClearAllLabel = entity.ClearAllLabel;
            viewModel.FilterResultsLabel = entity.FilterResultsLabel;
            viewModel.ShowMoreLabel = entity.ShowMoreLabel;
            viewModel.ShowLessLabel = entity.ShowLessLabel;
            viewModel.IsShowMoreLessButtonActive = entity.IsShowMoreLessButtonActive;
            viewModel.DisplayItemCount = entity.DisplayItemCount;

            string searchQuery = httpContext.Request.Query["searchQuery"];

            if (!string.IsNullOrEmpty(searchQuery))
            {
                var facetableFieldsFromIndex = await this.GetFacetableFieldsFromIndex(entity.IndexCatalogue);
                IList<string> facetableFieldsKeys = facetableFieldsFromIndex.Select(x => x.FacetableFieldNames.FirstOrDefault()).ToList();

                var selectedFacetsToBeUsed = entity.SelectedFacets
                    .GroupBy(x => x.FacetableFieldNames[0])
                    .Select(f => f.LastOrDefault())
                    .Where(x => facetableFieldsKeys.Contains(x.FacetableFieldNames[0]));

                List<Facet> facets = WidgetSettingsFacetFieldMapper.MapWidgetSettingsToFieldsModel(selectedFacetsToBeUsed);

                string filter = httpContext.Request.Query["filter"];
                string culture = httpContext.Request.Query["sf_culture"];
                string resultsForAllSites = httpContext.Request.Query["resultsForAllSites"];
                var searchServiceFacetResponse = await this.GetFacets(searchQuery, culture, entity.IndexCatalogue, filter, resultsForAllSites, entity.SearchFields, facets);

                var facetsDict = searchServiceFacetResponse.ToDictionary(x => x.FacetKey, x => x.FacetResponses);

                viewModel.SearchFacets = SearchFacetsModelBuilder.BuildFacetsViewModel(entity.SelectedFacets, facetsDict, facetableFieldsKeys, entity.SortType);
            }

            viewModel.HasAnyFacetElements = HasAnyFacetElements(viewModel.SearchFacets);
            var margins = this.styles.GetMarginsClasses(entity);
            viewModel.CssClass = (entity.WidgetCssClass + " " + margins).Trim();

            return viewModel;
        }

        private static bool HasAnyFacetElements(IList<SearchFacets> searchFacets)
        {
            bool hasAnyFacetElements = false;
            if (searchFacets.Any())
            {
                hasAnyFacetElements = searchFacets.Any(sf => sf.FacetElements.Any());
            }

            return hasAnyFacetElements;
        }

        private async Task<IEnumerable<FacetsViewModelDto>> GetFacetableFieldsFromIndex(string indexCatalogue)
        {
            var response = await this.restClient.ExecuteUnboundFunction<ODataWrapper<IEnumerable<FacetsViewModelDto>>>(new BoundFunctionArgs()
            {
                Name = $"Default.GetFacetableFields(indexCatalogName='{indexCatalogue}')",
            });

            return response.Value;
        }

        private async Task<IList<FacetFlatResponseDto>> GetFacets(string searchQuery, string culture, string indexCatalogue, string filter, string resultsForAllSites, string searchFields, List<Facet> facets)
        {
            string facetsStr = JsonConvert.SerializeObject(facets);

            var response = await this.restClient.ExecuteUnboundFunction<ODataWrapper<IList<FacetFlatResponseDto>>>(new BoundFunctionArgs()
            {
                Name = "Default.GetFacets",
                AdditionalQueryParams = new Dictionary<string, string>()
                {
                    ["searchQuery"] = searchQuery,
                    ["sf_culture"] = culture,
                    ["indexCatalogName"] = indexCatalogue,
                    ["filter"] = filter,
                    ["resultsForAllSites"] = resultsForAllSites,
                    ["searchFields"] = searchFields,
                    ["facetFields"] = facetsStr,
                },
            });

            return response.Value;
        }
    }
}
