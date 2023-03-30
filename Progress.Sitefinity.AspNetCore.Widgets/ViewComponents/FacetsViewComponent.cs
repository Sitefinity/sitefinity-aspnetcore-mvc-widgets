using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Progress.Sitefinity.AspNetCore.Web;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Facets;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Recommendations;
using Progress.Sitefinity.RestSdk;
using Progress.Sitefinity.RestSdk.OData;

namespace Progress.Sitefinity.AspNetCore.Widgets.ViewComponents
{
    /// <summary>
    /// The view component for the facets widget.
    /// </summary>
    [SitefinityWidget(Title = "Search facets", Order = 2, Section = WidgetSection.SearchAndClassification, EmptyIconText = "Select search facets", EmptyIconAction = EmptyLinkAction.Edit, EmptyIcon = "search", Category = WidgetCategory.NavigationAndSearch)]
    [ViewComponent(Name = "SitefinityFacets")]
    public class FacetsViewComponent : ViewComponent
    {
        private IFacetsModel model;
        private IRenderContext renderContext;
        private IODataRestClient restService;

        /// <summary>
        /// Initializes a new instance of the <see cref="FacetsViewComponent"/> class.
        /// </summary>
        /// <param name="model">The <see cref="IFacetsModel"/> model.</param>
        /// <param name="renderContext">The render context.</param>
        /// <param name="restService">The rest service.</param>
        public FacetsViewComponent(IFacetsModel model, IRenderContext renderContext, IODataRestClient restService)
        {
            this.model = model;
            this.renderContext = renderContext;
            this.restService = restService;
        }

        /// <summary>
        /// Invokes the search facets creation.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public virtual async Task<IViewComponentResult> InvokeAsync(IViewComponentContext<FacetsEntity> context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var viewModel = await this.model.InitializeViewModel(context.Entity, this.HttpContext);

            if (this.renderContext.IsEdit)
            {
                var searchMetadata = await this.GetSearchMetadata();

                // with unsupported search service
                if (searchMetadata.IsSearchModuleActivated && !searchMetadata.SearchServiceSupportsFacets)
                {
                    context.SetWarning("This widget cannot be used with your current search service. Search facets works only with Azure Search and Elasticsearch services.");
                    context.SetHideEmptyVisual(true);
                    return new ContentViewComponentResult(string.Empty);
                }
            }

            return this.View(context.Entity.SfViewName, viewModel);
        }

        private async Task<SearchMetadataDto> GetSearchMetadata()
        {
            var searchMetadata = await this.restService.ExecuteUnboundFunction<SearchMetadataDto>(new BoundActionArgs
            {
                Name = $"Default.GetSearchMetadata()",
            });

            return searchMetadata;
        }
    }
}
