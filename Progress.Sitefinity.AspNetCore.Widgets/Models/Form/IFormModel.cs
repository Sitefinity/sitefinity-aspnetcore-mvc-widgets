using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Progress.Sitefinity.AspNetCore.ViewComponents;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Form
{
    /// <summary>
    /// Defines model for the Form widget.
    /// </summary>
    public interface IFormModel
    {
        /// <summary>
        /// Initializes the view model.
        /// </summary>
        /// <param name="entity">The form entity.</param>
        /// <param name="query">The request query params.</param>
        /// <returns>The view model of the widget.</returns>
        Task<FormViewModel> InitializeViewModel(FormEntity entity, IQueryCollection query);
    }
}
