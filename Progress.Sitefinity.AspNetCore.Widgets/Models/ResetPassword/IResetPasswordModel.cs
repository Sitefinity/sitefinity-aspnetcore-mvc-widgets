using System.Threading.Tasks;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.ResetPassword
{
    /// <summary>
    /// Defines model for the forgotten password widget.
    /// </summary>
    public interface IResetPasswordModel
    {
        /// <summary>
        /// Initializes the view model.
        /// </summary>
        /// <param name="entity">The forgotten password entity.</param>
        /// <returns>The view model of the widget.</returns>
        Task<ResetPasswordViewModel> InitializeViewModel(ResetPasswordEntity entity);
    }
}
