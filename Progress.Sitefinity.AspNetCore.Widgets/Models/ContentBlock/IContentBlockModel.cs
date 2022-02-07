using System.Threading.Tasks;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.ContentBlock
{
    /// <summary>
    /// Defines model for the Content block widget.
    /// </summary>
    public interface IContentBlockModel
    {
        /// <summary>
        /// Initializes the view model.
        /// </summary>
        /// <param name="entity">The content block entity.</param>
        /// <returns>The view model of the widget.</returns>
        Task<ContentBlockViewModel> InitializeViewModel(ContentBlockEntity entity);
    }
}
