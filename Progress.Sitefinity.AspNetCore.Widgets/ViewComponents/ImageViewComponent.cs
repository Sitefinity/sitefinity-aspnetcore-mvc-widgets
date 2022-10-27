﻿using System;
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
    [SitefinityWidget(EmptyIcon = "picture-o", EmptyIconText = "Select image", Title = "Image", Order = 2, Section = WidgetSection.Basic)]
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

            context.State.TryGetValue(ImagePreparation.PreparedData, out object value);

            if (value is ErrorCodeException ex)
            {
                throw new ErrorCodeException(ex.Message);
            }

            ImageViewModel viewModel;
            if (value is ImageDto item && this.model is IImageModelWithPreparation)
            {
                viewModel = await (this.model as IImageModelWithPreparation).InitializeViewModel(context.Entity, item);
            }
            else
            {
                viewModel = await this.model.InitializeViewModel(context.Entity);
            }

            return this.View(viewModel.ViewName, viewModel);
        }
    }
}
