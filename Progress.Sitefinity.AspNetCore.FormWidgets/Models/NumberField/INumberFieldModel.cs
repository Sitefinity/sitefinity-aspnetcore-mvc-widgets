using System.Threading.Tasks;
using Progress.Sitefinity.AspNetCore.FormWidgets.Entities.NumberField;
using Progress.Sitefinity.AspNetCore.FormWidgets.ViewComponents;
using Progress.Sitefinity.AspNetCore.FormWidgets.ViewModels.NumberField;

namespace Progress.Sitefinity.AspNetCore.FormWidgets.Models.NumberField
{
    /// <summary>
    /// The model object that holds the logic for the <see cref="NumberFieldViewComponent" /> widget.
    /// </summary>
    public interface INumberFieldModel
    {
        /// <summary>
        /// Initializes the number field view model.
        /// </summary>
        /// <param name="entity">The number field entity.</param>
        /// <returns>The view model of the widget.</returns>
        Task<NumberFieldViewModel> InitializeViewModel(NumberFieldEntity entity);
    }
}
