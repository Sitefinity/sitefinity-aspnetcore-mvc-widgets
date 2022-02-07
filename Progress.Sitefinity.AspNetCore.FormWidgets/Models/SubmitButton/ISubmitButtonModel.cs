using System.Threading.Tasks;
using Progress.Sitefinity.AspNetCore.FormWidgets.Entities.SubmitButton;
using Progress.Sitefinity.AspNetCore.FormWidgets.ViewComponents;
using Progress.Sitefinity.AspNetCore.FormWidgets.ViewModels.SubmitButton;

namespace Progress.Sitefinity.AspNetCore.FormWidgets.Models.SubmitButton
{
    /// <summary>
    /// The model object that holds the logic for the <see cref="SubmitButtonViewComponent" /> widget.
    /// </summary>
    public interface ISubmitButtonModel
    {
        /// <summary>
        /// Initializes the submit button view model.
        /// </summary>
        /// <param name="entity">The submit button entity.</param>
        /// <returns>The view model of the widget.</returns>
        Task<SubmitButtonViewModel> InitializeViewModel(SubmitButtonEntity entity);
    }
}
