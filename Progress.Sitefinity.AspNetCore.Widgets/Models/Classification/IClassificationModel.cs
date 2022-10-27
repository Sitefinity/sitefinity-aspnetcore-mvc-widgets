using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Classification
{
    /// <summary>
    /// Defines model for the Classification widget.
    /// </summary>
    public interface IClassificationModel
    {
        /// <summary>
        /// Initializes the view model.
        /// </summary>
        /// <param name="entity">The classification widget entity.</param>
        /// <returns>The view model of the widget.</returns>
        Task<ClassificationViewModel> InitializeViewModel(ClassificationEntity entity);
    }
}
