using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Facets
{
    /// <summary>
    /// Defines model for the facets widget.
    /// </summary>
    public interface IFacetsModel
    {
        /// <summary>
        /// Initializes the view model.
        /// </summary>
        /// <param name="entity">The facets entity.</param>
        /// <param name="httpContext">The current http context.</param>
        /// <returns>The view model of the widget.</returns>
        Task<FacetsViewModel> InitializeViewModel(FacetsEntity entity, HttpContext httpContext);
    }
}
