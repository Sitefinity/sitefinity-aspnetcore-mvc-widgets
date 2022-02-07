using Progress.Sitefinity.AspNetCore.FormWidgets.Models.ContentBlock;

namespace Progress.Sitefinity.AspNetCore.FormWidgets.Models.ContentBlock
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
        ContentBlockViewModel InitializeViewModel(ContentBlockEntity entity);
    }
}
