using System.Collections.Generic;
using Progress.Sitefinity.AspNetCore.ViewComponents;

namespace Progress.Sitefinity.AspNetCore.FormWidgets.ViewModels.FormSection
{
    /// <summary>
    /// The view model for the section widget.
    /// </summary>
    public class FormSectionViewModel
    {
        /// <summary>
        /// Gets or sets the column's count.
        /// </summary>
        public int ColumnsCount { get; set; }

        /// <summary>
        /// Gets or sets the names of the columns.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Need to set the view model.")]
        public IList<string> ColumnNames { get; set; }

        /// <summary>
        /// Gets or sets the proportions information for the columns.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Need to set the view model.")]
        public IList<string> ColumnProportions { get; set; }

        /// <summary>
        /// Gets or sets the view context.
        /// </summary>
        public ICompositeViewComponentContext Context { get; set; }
    }
}
