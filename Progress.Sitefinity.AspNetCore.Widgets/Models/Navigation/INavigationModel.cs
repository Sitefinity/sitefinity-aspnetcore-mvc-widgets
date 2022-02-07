using System.Threading.Tasks;
using Progress.Sitefinity.RestSdk.OData;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Navigation
{
    /// <summary>
    /// Defines model for the Content block widget.
    /// </summary>
    public interface INavigationModel
    {
        /// <summary>
        /// Initializes the view model.
        /// </summary>
        /// <param name="entity">The navigation entity.</param>
        /// <returns>The view model of the widget.</returns>
        Task<NavigationViewModel> InitializeViewModel(NavigationEntity entity);
    }
}
