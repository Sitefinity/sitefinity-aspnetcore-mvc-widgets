using System.Threading.Tasks;
using Progress.Sitefinity.AspNetCore.FormWidgets.Entities.FormNavigation;
using Progress.Sitefinity.AspNetCore.FormWidgets.ViewModels.FormNavigation;

namespace Progress.Sitefinity.AspNetCore.FormWidgets.Models.FormNavigation
{
    /// <summary>
    /// The model for the form navigation widget.
    /// </summary>
    public interface IFormNavigationModel
    {
        /// <summary>
        /// Initializes the view model.
        /// </summary>
        /// <param name="entity">The form navigation entity.</param>
        /// <returns>The view model.</returns>
        Task<FormNavigationViewModel> InitializeViewModel(FormNavigationEntity entity);
    }
}
