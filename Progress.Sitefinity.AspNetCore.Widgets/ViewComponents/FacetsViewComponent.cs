using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.Extensions.Logging;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Progress.Sitefinity.AspNetCore.Web;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Facets;
using Progress.Sitefinity.RestSdk;
using Progress.Sitefinity.RestSdk.Exceptions;
using Progress.Sitefinity.RestSdk.OData;

namespace Progress.Sitefinity.AspNetCore.Widgets.ViewComponents
{
    /// <summary>
    /// The view component for the facets widget.
    /// </summary>
    [SitefinityWidget(Title = "Search facets", Order = 4, Section = WidgetSection.NavigationAndSearch, EmptyIconText = "Select search facets", EmptyIconAction = EmptyLinkAction.Edit, EmptyIcon = "search", Category = WidgetCategory.Content, IconName = "search", NotPersonalizable = true)]
    [ViewComponent(Name = "SitefinityFacets")]
    public class FacetsViewComponent : ViewComponent
    {
        private readonly ILogger<FacetsViewComponent> logger;
        private IFacetsModel model;
        private IRenderContext renderContext;
        private IODataRestClient restService;

        /// <summary>
        /// Initializes a new instance of the <see cref="FacetsViewComponent"/> class.
        /// </summary>
        /// <param name="model">The <see cref="IFacetsModel"/> model.</param>
        /// <param name="renderContext">The render context.</param>
        /// <param name="restService">The rest service.</param>
        /// <param name="logger">The logger.</param>
        public FacetsViewComponent(
            IFacetsModel model,
            IRenderContext renderContext,
            IODataRestClient restService,
            ILogger<FacetsViewComponent> logger)
        {
            this.model = model;
            this.renderContext = renderContext;
            this.restService = restService;
            this.logger = logger;
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
                SearchMetadataDto searchMetadata;

                try
                {
                    searchMetadata = await this.GetSearchMetadata();
                }
                catch (ErrorCodeException exception) when (exception.Code == "General")
                {
                    this.logger.LogError(exception, "Failed to retrieve search metadata for the facets widget.");
                    context.SetWarning("This widget cannot be displayed because search metadata could not be retrieved. Check the application logs for details.");
                    context.SetHideEmptyVisual(true);
                    return new ContentViewComponentResult(string.Empty);
                }

                // with unsupported search service
                if (!searchMetadata.SearchServiceSupportsFacets)
                {
                    context.SetWarning("This widget cannot be used with your current search service. Search facets work only with Lucene, Azure Search, and Elasticsearch services.");
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
