using System.Threading.Tasks;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.LoginForm
{
    /// <summary>
    /// Defines model for the login form widget.
    /// </summary>
    public interface ILoginFormModel
    {
        /// <summary>
        /// Initializes the view model.
        /// </summary>
        /// <param name="entity">The login form entity.</param>
        /// <returns>The view model of the widget.</returns>
        Task<LoginFormViewModel> InitializeViewModel(LoginFormEntity entity);
    }
}
