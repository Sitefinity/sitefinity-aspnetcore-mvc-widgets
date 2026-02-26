using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Progress.Sitefinity.AspNetCore.Widgets.Models.AskBox;

namespace Progress.Sitefinity.AspNetCore.Widgets.ViewComponents
{
    /// <summary>
    /// The view component for the AskBox widget.
    /// </summary>
    [SitefinityWidget(Title = "AI ask box", EmptyIconText = "Set where to seach", Order = 0, Section = WidgetSection.AISearch, IconName = "ai-search-sparkle", EmptyIconAction = EmptyLinkAction.Edit, EmptyIcon = "search")]
    [ViewComponent(Name = "SitefinityAskBox")]
    public class AskBoxViewComponent : ViewComponent
    {
        private readonly IAskBoxModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="AskBoxViewComponent"/> class.
        /// </summary>
        /// <param name="askBoxModel">The AskBox model.</param>
        public AskBoxViewComponent(IAskBoxModel askBoxModel)
        {
            this.model = askBoxModel;
        }

        /// <summary>
        /// Invokes the AskBox widget creation.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public virtual async Task<IViewComponentResult> InvokeAsync(IViewComponentContext<AskBoxEntity> context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var viewModel = await this.model.InitializeViewModel(context.Entity);

            return this.View(context.Entity.SfViewName, viewModel);
        }
    }
}
