using System.Threading.Tasks;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.IntentDrivenContent
{
    /// <summary>
    /// Defines the contract for the dynamically generated model.
    /// </summary>
    public interface IIntentDrivenContentModel
    {
        /// <summary>
        /// Initializes the view model for the dynamically generated widget.
        /// </summary>
        /// <param name="entity">The entity to initialize the view model with.</param>
        /// <returns>The initialized view model.</returns>
        Task<IntentDrivenContentViewModel> InitializeViewModel(IntentDrivenContentEntity entity);
    }
}
