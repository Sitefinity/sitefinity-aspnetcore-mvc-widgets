using System.Threading.Tasks;
using Progress.Sitefinity.AspNetCore.FormWidgets.Entities.Dropdown;
using Progress.Sitefinity.AspNetCore.FormWidgets.ViewComponents;
using Progress.Sitefinity.AspNetCore.FormWidgets.ViewModels.Common;

namespace Progress.Sitefinity.AspNetCore.FormWidgets.Models.Dropdown
{
    /// <summary>
    /// The model object that holds the logic for the <see cref="DropdownViewComponent" /> widget.
    /// </summary>
    public interface IDropdownModel
    {
        /// <summary>
        /// Initializes the dropdown view model.
        /// </summary>
        /// <param name="entity">The dropdown entity.</param>
        /// <returns>The view model of the widget.</returns>
        Task<ChoiceViewModelBase> InitializeViewModel(DropdownEntity entity);
    }
}
