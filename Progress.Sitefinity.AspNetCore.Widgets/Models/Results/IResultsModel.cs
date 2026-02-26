using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Results
{
    /// <summary>
    /// Defines model for the Results widget.
    /// </summary>
    public interface IResultsModel
    {
        /// <summary>
        /// Initializes the view model.
        /// </summary>
        /// <param name="entity">The Results entity.</param>
        /// <param name="httpContext">The HTTP context.</param>
        /// <returns>The view model of the widget.</returns>
        Task<ResultsViewModel> InitializeViewModel(ResultsEntity entity, HttpContext httpContext);
    }
}
