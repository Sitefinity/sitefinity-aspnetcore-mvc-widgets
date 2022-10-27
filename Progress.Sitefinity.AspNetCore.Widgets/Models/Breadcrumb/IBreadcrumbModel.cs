using System.Threading.Tasks;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Breadcrumb
{
    /// <summary>
    /// Defines model for the Breadcrumb widget.
    /// </summary>
    public interface IBreadcrumbModel
    {
        /// <summary>
        /// Initializes the view model.
        /// </summary>
        /// <param name="entity">The breadcrumb entity.</param>
        /// <returns>The view model of the widget.</returns>
        Task<BreadcrumbViewModel> InitializeViewModel(BreadcrumbEntity entity);
    }
}
