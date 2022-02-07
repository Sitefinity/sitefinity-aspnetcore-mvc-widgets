using System.Threading.Tasks;
using Progress.Sitefinity.AspNetCore.FormWidgets.Entities.FormSection;
using Progress.Sitefinity.AspNetCore.FormWidgets.ViewModels.FormSection;

namespace Progress.Sitefinity.AspNetCore.FormWidgets.Models.FormSection
{
    /// <summary>
    /// The model for the form section widget.
    /// </summary>
    public interface IFormSectionModel
    {
        /// <summary>
        /// Initializes the view model.
        /// </summary>
        /// <param name="entity">The section entity.</param>
        /// <returns>The view model.</returns>
        Task<FormSectionViewModel> InitializeViewModel(FormSectionEntity entity);
    }
}
