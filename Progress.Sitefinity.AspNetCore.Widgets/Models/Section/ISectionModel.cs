using System.Threading.Tasks;
using Progress.Sitefinity.AspNetCore.ViewComponents;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Section
{
    /// <summary>
    /// The model for the section widget.
    /// </summary>
    public interface ISectionModel
    {
        /// <summary>
        /// Initializes the view model.
        /// </summary>
        /// <param name="entity">The section entity.</param>
        /// <returns>The view model.</returns>
        Task<SectionViewModel> InitializeViewModel(SectionEntity entity);
    }
}
