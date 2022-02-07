using System.Threading.Tasks;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Registration
{
    /// <summary>
    /// Defines model for the registration widget.
    /// </summary>
    public interface IRegistrationModel
    {
        /// <summary>
        /// Initializes the view model.
        /// </summary>
        /// <param name="entity">The registration entity.</param>
        /// <returns>The view model of the widget.</returns>
        public Task<RegistrationViewModel> InitializeViewModel(RegistrationEntity entity);
    }
}
