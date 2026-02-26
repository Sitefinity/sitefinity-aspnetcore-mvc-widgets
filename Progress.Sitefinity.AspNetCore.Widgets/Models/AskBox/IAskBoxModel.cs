using System.Threading.Tasks;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.AskBox
{
    /// <summary>
    /// Defines model for the AskBox widget.
    /// </summary>
    public interface IAskBoxModel
    {
        /// <summary>
        /// Initializes the view model.
        /// </summary>
        /// <param name="entity">The AskBox entity.</param>
        /// <returns>The view model of the widget.</returns>
        Task<AskBoxViewModel> InitializeViewModel(AskBoxEntity entity);
    }
}
