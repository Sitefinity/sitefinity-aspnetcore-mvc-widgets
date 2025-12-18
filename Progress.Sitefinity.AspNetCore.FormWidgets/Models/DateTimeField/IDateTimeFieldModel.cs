using System.Threading.Tasks;
using Progress.Sitefinity.AspNetCore.FormWidgets.Entities.DateTimeField;
using Progress.Sitefinity.AspNetCore.FormWidgets.ViewComponents;
using Progress.Sitefinity.AspNetCore.FormWidgets.ViewModels.DateTimeField;

namespace Progress.Sitefinity.AspNetCore.FormWidgets.Models.DateField
{
    /// <summary>
    /// The model object that holds the logic for the <see cref="DateTimeFieldViewComponent" /> widget.
    /// </summary>
    public interface IDateTimeFieldModel
    {
        /// <summary>
        /// Initializes the date and time field view model.
        /// </summary>
        /// <param name="entity">The date and time field entity.</param>
        /// <returns>The view model of the widget.</returns>
        Task<DateTimeFieldViewModel> InitializeViewModel(DateTimeFieldEntity entity);
    }
}
