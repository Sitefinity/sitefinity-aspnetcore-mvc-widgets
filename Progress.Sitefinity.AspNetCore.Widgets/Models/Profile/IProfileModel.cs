using System.Threading.Tasks;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Profile
{
    /// <summary>
    /// Defines model for the Profile widget.
    /// </summary>
    public interface IProfileModel
    {
        /// <summary>
        /// Initializes the view model.
        /// </summary>
        /// <param name="entity">The Profile entity.</param>
        /// <returns>The view model of the widget.</returns>
        Task<ProfileViewModel> InitializeViewModel(ProfileEntity entity);
    }
}
