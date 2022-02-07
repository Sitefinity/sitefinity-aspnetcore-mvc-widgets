using System.Threading.Tasks;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.ChangePassword
{
    /// <summary>
    /// Defines model for the change password widget.
    /// </summary>
    public interface IChangePasswordModel
    {
        /// <summary>
        /// Initializes the view model.
        /// </summary>
        /// <param name="entity">The login form entity.</param>
        /// <returns>The view model of the widget.</returns>
        Task<ChangePasswordViewModel> InitializeViewModel(ChangePasswordEntity entity);
    }
}
