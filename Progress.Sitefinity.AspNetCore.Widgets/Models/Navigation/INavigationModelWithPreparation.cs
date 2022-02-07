using System.Threading.Tasks;
using Progress.Sitefinity.RestSdk.Dto;
using Progress.Sitefinity.RestSdk.OData;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Navigation
{
    /// <summary>
    /// Defines model for the Content block widget.
    /// </summary>
    internal interface INavigationModelWithPreparation
    {
        /// <summary>
        /// Initializes the view model with a preloaded state.
        /// </summary>
        /// <param name="entity">The navigation entity.</param>
        /// <param name="items">The page items.</param>
        /// <returns>The view model of the widget.</returns>
        NavigationViewModel InitializeViewModel(NavigationEntity entity, PageViewModel[] items);

        /// <summary>
        /// Loads the items for the provided entity using hte provided <see cref="IODataRestClient" /> instance.
        /// </summary>
        /// <param name="entity">The entity class.</param>
        /// <param name="restClient">The rest client.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<ODataWrapper<PageViewModel[]>> GetItems(NavigationEntity entity, IODataRestClient restClient);
    }
}
