using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Progress.Sitefinity.AspNetCore.Widgets.Models.ContentBlock;

namespace Progress.Sitefinity.AspNetCore.Widgets.ViewComponents
{
    /// <summary>
    /// The view component for the Content block widget.
    /// </summary>
    [SitefinityWidget(Title = "Content block", Order = 1, Section = WidgetSection.Basic)]
    [ViewComponent(Name = "SitefinityContentBlock")]
    public class ContentBlockViewComponent : ViewComponent
    {
        private IContentBlockModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentBlockViewComponent"/> class.
        /// </summary>
        /// <param name="contentBlockModel">The content block model.</param>
        public ContentBlockViewComponent(IContentBlockModel contentBlockModel)
        {
            this.model = contentBlockModel;
        }

        /// <summary>
        /// Invokes the Content block widget creation.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public virtual async Task<IViewComponentResult> InvokeAsync(IViewComponentContext<ContentBlockEntity> context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var viewModel = await this.model.InitializeViewModel(context.Entity);

            return this.View(viewModel);
        }
    }
}
