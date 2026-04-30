using System.Threading.Tasks;
using Progress.Sitefinity.AspNetCore.FormWidgets.Entities.FormPage;
using Progress.Sitefinity.AspNetCore.FormWidgets.ViewModels.FormPage;

namespace Progress.Sitefinity.AspNetCore.FormWidgets.Models.FormPage
{
    /// <summary>
    /// The model for the form page widget.
    /// </summary>
    public interface IFormPageModel
    {
        /// <summary>
        /// Initializes the view model.
        /// </summary>
        /// <param name="entity">The form page entity.</param>
        /// <returns>The view model.</returns>
        Task<FormPageViewModel> InitializeViewModel(FormPageEntity entity);
    }
}
