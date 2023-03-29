using System.Threading.Tasks;
using Progress.Sitefinity.AspNetCore.FormWidgets.Entities.Dropdown;
using Progress.Sitefinity.AspNetCore.FormWidgets.ViewComponents;
using Progress.Sitefinity.AspNetCore.FormWidgets.ViewComponents.DynamicList;
using Progress.Sitefinity.AspNetCore.FormWidgets.ViewModels.DynamicList;

namespace Progress.Sitefinity.AspNetCore.FormWidgets.Models.DynamicList
{
    /// <summary>
    /// The model object that holds the logic for the <see cref="DynamicListViewComponent" /> widget.
    /// </summary>
    public interface IDynamicListModel
    {
        /// <summary>
        /// Initializes the multiple choice view model.
        /// </summary>
        /// <param name="entity">The multiple choice entity.</param>
        /// <returns>The view model of the widget.</returns>
        Task<DynamicListViewModel> InitializeViewModel(DynamicListEntity entity);
    }
}
