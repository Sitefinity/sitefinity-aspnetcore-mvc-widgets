using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.ContentList
{
    /// <summary>
    /// Defines model for the Content list widget.
    /// </summary>
    public interface IContentListModelBase
    {
        /// <summary>
        /// Initializes the view model.
        /// </summary>
        /// <param name="entity">The content list entity base.</param>
        /// <param name="urlParameters">The url parameters.</param>
        /// <param name="httpContext">The http context.</param>
        /// <returns>The view model of the widget.</returns>
        Task<object> HandleListView(ContentListEntityBase entity, ReadOnlyCollection<string> urlParameters, HttpContext httpContext);
    }
}
