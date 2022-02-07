using System.Threading.Tasks;
using Progress.Sitefinity.AspNetCore.FormWidgets.Entities.TextField;
using Progress.Sitefinity.AspNetCore.FormWidgets.ViewComponents;
using Progress.Sitefinity.AspNetCore.FormWidgets.ViewModels.TextField;

namespace Progress.Sitefinity.AspNetCore.FormWidgets.Models.TextField
{
    /// <summary>
    /// The model object that holds the logic for the <see cref="TextFieldViewComponent" /> widget.
    /// </summary>
    public interface ITextFieldModel
    {
        /// <summary>
        /// Initializes the text field view model.
        /// </summary>
        /// <param name="entity">The text field entity.</param>
        /// <returns>The view model of the widget.</returns>
        Task<TextFieldViewModel> InitializeViewModel(TextFieldEntity entity);
    }
}
