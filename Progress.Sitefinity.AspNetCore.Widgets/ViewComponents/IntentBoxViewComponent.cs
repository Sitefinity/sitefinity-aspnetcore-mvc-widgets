using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Progress.Sitefinity.AspNetCore.Widgets.Models.IntentBox;

namespace Progress.Sitefinity.AspNetCore.Widgets.ViewComponents
{
    /// <summary>
    /// ViewComponent for rendering intent box.
    /// </summary>
    [SitefinityWidget(Title = "Intent box", Section = WidgetSection.DynamicExperiences)]
    [ViewComponent(Name = "SitefinityIntentBox")]
    public class IntentBoxViewComponent : ViewComponent
    {
        private readonly IIntentBoxModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="IntentBoxViewComponent"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public IntentBoxViewComponent(IIntentBoxModel model)
        {
            this.model = model;
        }

        /// <summary>
        /// Invokes the ViewComponent.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>The view component result.</returns>
        public async Task<IViewComponentResult> InvokeAsync(IViewComponentContext<IntentBoxEntity> context)
        {
            if (context == null)
                return this.Content(string.Empty);

            var viewModel = await this.model.InitializeViewModel(context.Entity);
            return this.View(context.Entity.SfViewName ?? "Default", viewModel);
        }
    }
}
