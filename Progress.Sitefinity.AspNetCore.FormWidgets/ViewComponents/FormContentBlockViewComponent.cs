using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.AspNetCore.FormWidgets.Models.ContentBlock;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Progress.Sitefinity.Renderer.Forms;

namespace Progress.Sitefinity.AspNetCore.FormWidgets.ViewComponents
{
    /// <summary>
    /// The view component for the Content block widget.
    /// </summary>
    [SitefinityFormWidget(FormFieldType.ContentBlock, Title = "Content block", Order = 3, Section = WidgetSection.Other)]
    [ViewComponent(Name = "SitefinityFormContentBlock")]
    public class FormContentBlockViewComponent : ViewComponent
    {
        private IContentBlockModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="FormContentBlockViewComponent"/> class.
        /// </summary>
        /// <param name="contentBlockModel">The content block model.</param>
        public FormContentBlockViewComponent(IContentBlockModel contentBlockModel)
        {
            this.model = contentBlockModel;
        }

        /// <summary>
        /// Invokes the Content block widget creation.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public IViewComponentResult Invoke(IViewComponentContext<ContentBlockEntity> context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var viewModel = this.model.InitializeViewModel(context.Entity);

            return this.View(viewModel);
        }
    }
}
