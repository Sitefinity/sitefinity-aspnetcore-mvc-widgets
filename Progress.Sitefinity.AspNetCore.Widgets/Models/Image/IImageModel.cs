using System.Threading.Tasks;
using Progress.Sitefinity.AspNetCore.ViewComponents;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Image
{
    /// <summary>
    /// Defines model for the Image widget.
    /// </summary>
    public interface IImageModel
    {
        /// <summary>
        /// Initializes the view model.
        /// </summary>
        /// <param name="entity">The image entity.</param>
        /// <returns>The view model of the widget.</returns>
        Task<ImageViewModel> InitializeViewModel(ImageEntity entity);
    }
}
