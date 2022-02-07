using System.Threading.Tasks;
using Progress.Sitefinity.AspNetCore.FormWidgets.Entities.FileField;
using Progress.Sitefinity.AspNetCore.FormWidgets.ViewComponents;
using Progress.Sitefinity.AspNetCore.FormWidgets.ViewModels.FileField;

namespace Progress.Sitefinity.AspNetCore.FormWidgets.Models.FileField
{
    /// <summary>
    /// The model object that holds the logic for the <see cref="FileFieldViewComponent" /> widget.
    /// </summary>
    public interface IFileFieldModel
    {
        /// <summary>
        /// Initializes the file field view model.
        /// </summary>
        /// <param name="entity">The file field entity.</param>
        /// <returns>The view model of the widget.</returns>
        Task<FileFieldViewModel> InitializeViewModel(FileFieldEntity entity);
    }
}
