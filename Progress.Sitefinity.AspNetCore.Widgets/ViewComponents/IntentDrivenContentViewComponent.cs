using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Progress.Sitefinity.AspNetCore.Widgets.Models.IntentDrivenContent;

namespace Progress.Sitefinity.AspNetCore.Widgets.ViewComponents
{
    /// <summary>
    /// ViewComponent for rendering intent-driven content.
    /// </summary>
    [SitefinityWidget(Title = "Intent-driven content", Section = WidgetSection.DynamicExperiences, EmptyIconText = "Set up content")]
    [ViewComponent(Name = "SitefinityIntentDrivenContent")]
    public class IntentDrivenContentViewComponent : ViewComponent
    {
        private readonly IIntentDrivenContentModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="IntentDrivenContentViewComponent"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public IntentDrivenContentViewComponent(IIntentDrivenContentModel model)
        {
            this.model = model;
        }

        /// <summary>
        /// Invokes the ViewComponent.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>The view component result.</returns>
        public async Task<IViewComponentResult> InvokeAsync(IViewComponentContext<IntentDrivenContentEntity> context)
        {
            if (context == null)
                return this.Content(string.Empty);

            var viewModel = await this.model.InitializeViewModel(context.Entity);
            return this.View(context.Entity.SfViewName ?? "Default", viewModel);
        }
    }
}
