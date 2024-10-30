using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Image;
using Progress.Sitefinity.AspNetCore.Widgets.Preparations;
using Progress.Sitefinity.RestSdk.Dto;
using Progress.Sitefinity.RestSdk.Exceptions;

namespace Progress.Sitefinity.AspNetCore.Widgets.ViewComponents
{
    /// <summary>
    /// The view component for the Image widget.
    /// </summary>
    [SitefinityWidget(EmptyIcon = "picture-o", EmptyIconText = "Select image", Title = "Image", Order = 2, Section = WidgetSection.Basic, IconName = "image")]
    [ViewComponent(Name = "SitefinityImage")]
    public class ImageViewComponent : ViewComponent
    {
        private IImageModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageViewComponent"/> class.
        /// </summary>
        /// <param name="imageModel">The image model.</param>
        public ImageViewComponent(IImageModel imageModel)
        {
            this.model = imageModel;
        }

        /// <summary>
        /// Invokes the Image widget creation.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public virtual async Task<IViewComponentResult> InvokeAsync(IViewComponentContext<ImageEntity> context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var viewModel = await this.model.InitializeViewModel(context.Entity);

            return this.View(viewModel.ViewName, viewModel);
        }
    }
}
