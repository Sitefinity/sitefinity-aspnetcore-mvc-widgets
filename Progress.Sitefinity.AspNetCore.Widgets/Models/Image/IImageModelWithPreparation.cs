using System.Threading.Tasks;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Progress.Sitefinity.RestSdk;
using Progress.Sitefinity.RestSdk.Dto;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Image
{
    /// <summary>
    /// Defines model for the Image widget.
    /// </summary>
    internal interface IImageModelWithPreparation
    {
        /// <summary>
        /// Initializes the view model.
        /// </summary>
        /// <param name="entity">The image entity.</param>
        /// <param name="restClient">The rest client.</param>
        /// <returns>The view model of the widget.</returns>
        Task<ImageDto> GetImage(ImageEntity entity, IRestClient restClient);

        /// <summary>
        /// Initializes the view model.
        /// </summary>
        /// <param name="entity">The image entity.</param>
        /// <param name="image">The image.</param>
        /// <returns>The view model of the widget.</returns>
        Task<ImageViewModel> InitializeViewModel(ImageEntity entity, ImageDto image);
    }
}
