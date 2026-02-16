using System.Threading.Tasks;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.IntentBox
{
    /// <summary>
    /// Provides an interface for the Intent Box model, which is responsible for initializing the view model for the Intent Box widget.
    /// </summary>
    public interface IIntentBoxModel
    {
        /// <summary>
        /// Initializes the <see cref="IntentBoxViewModel"/> based on the provided <see cref="IntentBoxEntity"/>.
        /// </summary>
        /// <param name="entity">The entity containing the configuration for the Intent Box widget.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the initialized <see cref="IntentBoxViewModel"/>.</returns>
        Task<IntentBoxViewModel> InitializeViewModel(IntentBoxEntity entity);
    }
}
