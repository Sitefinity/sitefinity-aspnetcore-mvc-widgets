using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Answer
{
    /// <summary>
    /// Defines model for the Answer widget.
    /// </summary>
    public interface IAnswerModel
    {
        /// <summary>
        /// Initializes the view model.
        /// </summary>
        /// <param name="entity">The Answer entity.</param>
        /// <param name="httpContext">The HttpContext.</param>
        /// <returns>The view model of the widget.</returns>
        Task<AnswerViewModel> InitializeViewModel(AnswerEntity entity, HttpContext httpContext);
    }
}
