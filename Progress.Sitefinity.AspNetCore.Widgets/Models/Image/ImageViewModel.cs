using System.Collections.Generic;
using Progress.Sitefinity.AspNetCore.Models;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Content;
using Progress.Sitefinity.RestSdk.Dto;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Image
{
    /// <summary>
    /// The view model for the image widget.
    /// </summary>
    public class ImageViewModel : ContentViewModelBase<ImageDto>
    {
        /// <summary>
        /// Gets or sets the click action for the image.
        /// </summary>
        public ImageClickAction ClickAction { get; set; }

        /// <summary>
        /// Gets or sets the selected image URL depending on the selected mode.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1056:Uri properties should not be strings", Justification = "Needed in the model.")]
        public string SelectedImageUrl { get; set; }

        /// <summary>
        /// Gets or sets the title of the image for the current component.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the alternative text.
        /// </summary>
        public string AlternativeText { get; set; }

        /// <summary>
        /// Gets or sets the action link.
        /// </summary>
        public string ActionLink { get; set; }

        /// <summary>
        /// Gets or sets the image size.
        /// </summary>
        public ImageDisplayMode ImageSize { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether an image should fit in the container.
        /// </summary>
        public bool FitToContainer { get; set; }

        /// <summary>
        /// Gets a value indicating whether an image should fit in the container.
        /// </summary>
        public IList<ThumbnailDto> Thumbnails { get; internal set; }

        /// <summary>
        /// Gets or sets the view name.
        /// </summary>
        public string ViewName { get; set; }

        /// <summary>
        /// Gets or sets the width of the image.
        /// </summary>
        public int? Width { get; set; }

        /// <summary>
        /// Gets or sets the height of the image.
        /// </summary>
        public int? Height { get; set; }

        /// <summary>
        /// Gets or sets the attributes for the image.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Must be able to set in property editor.")]
        public IDictionary<string, IList<AttributeModel>> Attributes { get; set; }
    }
}
