using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Progress.Sitefinity.AspNetCore.Configuration;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Common;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Content;
using Progress.Sitefinity.AspNetCore.Widgets.ViewComponents.Common;
using Progress.Sitefinity.RestSdk;
using Progress.Sitefinity.RestSdk.Dto;
using Progress.Sitefinity.RestSdk.OData;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Image
{
    /// <summary>
    /// The model for the Image widget.
    /// </summary>
    public class ImageModel : IImageModel
    {
        private IODataRestClient restClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageModel"/> class.
        /// </summary>
        /// <param name="service">The rest service.</param>
        /// <param name="styles">The style classes provider.</param>
        public ImageModel(IODataRestClient service, IStyleClassesProvider styles)
        {
            this.styles = styles;
            this.restClient = service;
        }

        /// <summary>
        /// Gest the image from the server.
        /// </summary>
        /// <param name="entity">The image entity.</param>
        /// <param name="restClient">The rest client.</param>
        /// <returns>The image.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "public")]
        public Task<ImageDto> GetImage(ImageEntity entity, IRestClient restClient)
        {
            if (restClient == null)
                throw new ArgumentNullException(nameof(restClient));

            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            if (entity.Item != null && entity.Item.Id != null)
            {
                var itemId = entity.Item.Id;
                var args = new BoundFunctionArgs()
                {
                    Id = itemId.ToString(),
                    Name = "Default.GetItemWithFallback()",
                    AdditionalQueryParams = new Dictionary<string, string>() { { "sf_fallback_prop_names", "*" }, { "$select", "*" } },
                    Provider = entity.Item.Provider,
                };

                return (restClient as IODataRestClient).ExecuteBoundFunction<ImageDto>(args);
            }

            return Task.FromResult<ImageDto>(null);
        }

        /// <summary>
        /// Initializes the view model.
        /// </summary>
        /// <param name="entity">The navigation entity.</param>
        /// <returns>The view model of the widget.</returns>
        public async Task<ImageViewModel> InitializeViewModel(ImageEntity entity)
        {
            var image = await this.GetImage(entity, this.restClient);
            return await this.InitializeViewModel(entity, image);
        }

        /// <summary>
        /// Initializes the view model.
        /// </summary>
        /// <param name="entity">The image entity.</param>
        /// <param name="image">The image.</param>
        /// <returns>The view model of the widget.</returns>
        public Task<ImageViewModel> InitializeViewModel(ImageEntity entity, ImageDto image)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var viewModel = new ImageViewModel();
            viewModel.Item = image;

            viewModel.CssClass = entity.CssClass;
            viewModel.Title = entity.Title;
            viewModel.AlternativeText = entity.AlternativeText;
            viewModel.ClickAction = entity.ClickAction;
            viewModel.ImageSize = entity.ImageSize;
            viewModel.FitToContainer = entity.FitToContainer;
            viewModel.ViewName = entity.ViewName;
            viewModel.Attributes = entity.Attributes;
            var margins = this.styles.GetMarginsClasses(entity);
            viewModel.CssClass = (entity.CssClass + " " + margins).Trim();
            if (viewModel.Item != null)
            {
                viewModel.Title = viewModel.Title ?? viewModel.Item.Title;
                viewModel.AlternativeText = viewModel.AlternativeText ?? viewModel.Item.AlternativeText;

                var isSvg = viewModel.Item.GetValue<string>("MimeType") == "image/svg+xml";
                var hasZeroDimensions = viewModel.Item.Width == 0 && viewModel.Item.Height == 0;
                viewModel.Width = isSvg && hasZeroDimensions ? null : viewModel.Item.Width;
                viewModel.Height = isSvg && hasZeroDimensions ? null : viewModel.Item.Height;

                viewModel.SelectedImageUrl = viewModel.Item.Url;
                viewModel.ActionLink = GetActionLink(entity, viewModel.Item);
                if (viewModel.Item.Thumbnails != null)
                {
                    viewModel.Thumbnails = viewModel.Item.Thumbnails.OrderBy(t => t.Width).ToList();
                    if (entity.ImageSize == ImageDisplayMode.Thumbnail && entity.Thumnail != null)
                    {
                        var selectedThumbnail = viewModel.Thumbnails.FirstOrDefault(t => t.Title == entity.Thumnail.Name);
                        viewModel.SelectedImageUrl = entity.Thumnail.Url;

                        if (selectedThumbnail != null)
                        {
                            viewModel.SelectedImageUrl = selectedThumbnail.Url;
                            viewModel.Width = selectedThumbnail.Width;
                            viewModel.Height = selectedThumbnail.Height;
                        }
                    }
                }

                // Thumbnails for images imported from DAM providers are not stored in Sitefinity and we do not know their width and height.
                // We have to set width and heigth to null otherwise the image is zoomed and selected thumbnail not applied correctly.
                if (entity.ImageSize == ImageDisplayMode.Thumbnail && image != null && image.IsDamMedia)
                {
                    viewModel.Width = null;
                    viewModel.Height = null;
                }
            }

            if (entity.CustomSize != null && entity.ImageSize == ImageDisplayMode.CustomSize)
            {
                viewModel.Width = entity.CustomSize.Width;
                viewModel.Height = entity.CustomSize.Height;
            }

            return Task.FromResult(viewModel);
        }

        private static string GetActionLink(ImageEntity entity, ImageDto image)
        {
            if (entity.ClickAction == ImageClickAction.OpenLink)
            {
                if (entity.ActionLink != null)
                {
                    return entity.ActionLink.Href;
                }
            }
            else if (entity.ClickAction == ImageClickAction.OpenOriginalSize)
            {
                return image.Url;
            }

            return string.Empty;
        }

        private IStyleClassesProvider styles;
    }
}
