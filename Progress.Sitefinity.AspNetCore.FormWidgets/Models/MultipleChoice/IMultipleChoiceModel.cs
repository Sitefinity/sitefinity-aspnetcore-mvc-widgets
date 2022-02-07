using System.Threading.Tasks;
using Progress.Sitefinity.AspNetCore.FormWidgets.Entities.MultipleChoice;
using Progress.Sitefinity.AspNetCore.FormWidgets.ViewComponents;
using Progress.Sitefinity.AspNetCore.FormWidgets.ViewModels.MultipleChoice;

namespace Progress.Sitefinity.AspNetCore.FormWidgets.Models.MultipleChoice
{
    /// <summary>
    /// The model object that holds the logic for the <see cref="MultipleChoiceViewComponent" /> widget.
    /// </summary>
    public interface IMultipleChoiceModel
    {
        /// <summary>
        /// Initializes the multiple choice view model.
        /// </summary>
        /// <param name="entity">The multiple choice entity.</param>
        /// <returns>The view model of the widget.</returns>
        Task<MultipleChoiceViewModel> InitializeViewModel(MultipleChoiceEntity entity);
    }
}
