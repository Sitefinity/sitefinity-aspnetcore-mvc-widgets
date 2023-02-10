using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Progress.Sitefinity.AspNetCore.Configuration;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Progress.Sitefinity.AspNetCore.Web;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Recommendations;
using Progress.Sitefinity.AspNetCore.Widgets.ViewComponents.Common;
using Progress.Sitefinity.RestSdk;
using Progress.Sitefinity.RestSdk.OData;

namespace Progress.Sitefinity.AspNetCore.Widgets.ViewComponents
{
    /// <summary>
    /// The view component for the Recommendations widget.
    /// </summary>
    [SitefinityWidget(Title = RecommendationsViewComponent.Title, EmptyIconText = "Configure recommendations", EmptyIconAction = EmptyLinkAction.Edit, EmptyIcon = "pencil", Section = WidgetSection.Marketing)]
    [ViewComponent(Name = "SitefinityRecommendations")]
    public class RecommendationsViewComponent : ViewComponent
    {
        /// <summary>
        /// The default title of the <see cref="RecommendationsViewComponent" /> - Content recommendations.
        /// </summary>
        public const string Title = "Content recommendations";

        private IODataRestClient restService;
        private IRenderContext renderContext;
        private IHttpContextAccessor httpContextAccessor;
        private IStyleClassesProvider styles;
        private IRequestContext requestContext;
        private ISitefinityConfig sitefinityConfig;

        /// <summary>
        /// Initializes a new instance of the <see cref="RecommendationsViewComponent"/> class.
        /// </summary>
        /// <param name="restService">The rest service.</param>
        /// <param name="renderContext">The render context.</param>
        /// <param name="httpContextAccessor">The http context accessor.</param>
        /// <param name="styles">The style classes provider.</param>
        /// <param name="requestContext">The request context.</param>
        /// <param name="sitefinityConfig">The sitefinity config.</param>
        public RecommendationsViewComponent(
                                            IODataRestClient restService,
                                            IRenderContext renderContext,
                                            IHttpContextAccessor httpContextAccessor,
                                            IStyleClassesProvider styles,
                                            IRequestContext requestContext,
                                            ISitefinityConfig sitefinityConfig)
        {
            this.restService = restService;
            this.renderContext = renderContext;
            this.httpContextAccessor = httpContextAccessor;
            this.styles = styles;
            this.requestContext = requestContext;
            this.sitefinityConfig = sitefinityConfig;
        }

        /// <summary>
        /// Invokes the Content list widget creation.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public virtual async Task<IViewComponentResult> InvokeAsync(IViewComponentContext<RecommendationsEntity> context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            RecommendationsViewModel viewModel = this.GetViewModel(context.Entity);
            var recommendationsFeatureState = await this.GetRecommendationsFeatureState(viewModel.ConversionId);

            if (this.renderContext.IsEdit || this.renderContext.IsPreview)
            {
                return this.GetViewForDesignMode(context, recommendationsFeatureState, viewModel);
            }

            if (!recommendationsFeatureState.HasValidConnectionForCurrentSite ||
                !recommendationsFeatureState.IsContentRecommendationsFeatureEnabled ||
                !recommendationsFeatureState.ConversionExists ||
                recommendationsFeatureState.LostConnectionToInsight ||
                context.Entity.Conversion <= 0)
            {
                return new ContentViewComponentResult(string.Empty);
            }

            return this.View(context.Entity.SfViewName ?? RecommendationsConstants.DefaultViewName, viewModel);
        }

        private RecommendationsViewModel GetViewModel(RecommendationsEntity entity)
        {
            var recommendationsViewModel = new RecommendationsViewModel();
            recommendationsViewModel.ConversionId = entity.Conversion;
            recommendationsViewModel.UniqueId = Guid.NewGuid();
            recommendationsViewModel.Header = entity.Header ?? RecommendationsConstants.DefaultHeader;
            recommendationsViewModel.MaxNumberOfItems = entity.MaxNumberOfItems;
            recommendationsViewModel.WebServicePath = this.sitefinityConfig.WebServicePath;
            recommendationsViewModel.Attributes = entity.Attributes;
            var margins = this.styles.GetMarginsClasses(entity);
            recommendationsViewModel.CssClass = (entity.CssClass + " " + margins).Trim();
            recommendationsViewModel.SiteId = this.requestContext.Site.Id;

            return recommendationsViewModel;
        }

        private async Task<RecommendationsStateFeatureDto> GetRecommendationsFeatureState(int conversionId)
        {
            var recommendationsFeatureState = await this.restService.ExecuteUnboundFunction<RecommendationsStateFeatureDto>(new BoundActionArgs
            {
                Name = $"Default.GetRecommendationsFeatureState(conversionId={conversionId})",
            });

            return recommendationsFeatureState;
        }

        private IViewComponentResult GetViewForDesignMode(IViewComponentContext<RecommendationsEntity> context, RecommendationsStateFeatureDto recommendationsFeatureState, RecommendationsViewModel recommendationsViewModel)
        {
            // No connected Insight or no sites set for tracking
            if (!recommendationsFeatureState.HasValidConnectionForCurrentSite)
            {
                context.SetWarning("This widget works only if there is an active connection to Sitefinity Insight. Configure a connection in Administration > Sitefinity Insight.");
                context.SetHideEmptyVisual(true);
                return new ContentViewComponentResult(string.Empty);
            }

            // Lost connection to Insight
            if (recommendationsFeatureState.LostConnectionToInsight)
            {
                throw new Exception("Тhe connection to Sitefinity Insight has been lost. Try to restore the connection.");
            }

            // Connected Insight but no Premium subscription
            if (!recommendationsFeatureState.IsContentRecommendationsFeatureEnabled)
            {
                context.SetWarning("Content recommendations are available only to Sitefinity Insight Premium subscriptions.");
                context.SetHideEmptyVisual(true);
                return new ContentViewComponentResult(string.Empty);
            }

            // Empty (no convesion selected)
            if (recommendationsViewModel.ConversionId <= 0)
            {
                return new ContentViewComponentResult(string.Empty);
            }

            // Deleted conversion
            if (!recommendationsFeatureState.ConversionExists)
            {
                throw new Exception("The selected conversion is not found. Select another conversion.");
            }

            context.SetWarning("This is a sample recommendation. Actual recommendations are visible only on the public site for visitors who have interacted enough with the site.");
            return this.View(RecommendationsConstants.DefaultDesignViewName, recommendationsViewModel);
        }
    }
}
