using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Progress.Sitefinity.AspNetCore.Widgets.Models.SitefinityAssistant;

namespace Progress.Sitefinity.AspNetCore.Widgets.ViewComponents
{
    /// <summary>
    /// The view component for the widget.
    /// </summary>
    [SitefinityWidget(Title = "AI assistant", EmptyIconText = "Select an AI assistant", EmptyIcon = "pencil", Order = 1, Section = WidgetSection.Marketing, IconName = "chat")]
    [ViewComponent(Name = "SitefinityAssistant")]
    public class SitefinityAssistantViewComponent : ViewComponent
    {
        private readonly ISitefinityAssistantModel sitefinityAssistantModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="SitefinityAssistantViewComponent"/> class.
        /// </summary>
        /// <param name="sitefinityAssistantModel">The sitefinityAssistatModel parameter.</param>
        public SitefinityAssistantViewComponent(ISitefinityAssistantModel sitefinityAssistantModel)
        {
            this.sitefinityAssistantModel = sitefinityAssistantModel;
        }

        /// <summary>
        /// Invokes the view.
        /// </summary>
        /// <param name="context">The context parameter.</param>
        /// <returns>The view.</returns>
        public async Task<IViewComponentResult> InvokeAsync(IViewComponentContext<SitefinityAssistantEntity> context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var viewModel = await this.sitefinityAssistantModel.GetViewModel(context);

            return this.View(viewModel);
        }
    }
}
