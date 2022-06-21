using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Search
{
    /// <summary>
    /// Defines model for the Search box widget.
    /// </summary>
    public interface ISearchBoxModel
    {
        /// <summary>
        /// Initializes the view model.
        /// </summary>
        /// <param name="entity">The search box entity.</param>
        /// <returns>The view model of the widget.</returns>
        Task<SearchBoxViewModel> InitializeViewModel(SearchBoxEntity entity);
    }
}
