using Progress.Sitefinity.AspNetCore.FormWidgets.ViewModels.Common;

namespace Progress.Sitefinity.AspNetCore.FormWidgets.ViewModels.DynamicList
{
    /// <summary>
    /// The view model for the multiple choice widget.
    /// </summary>
    public class DynamicListViewModel : ChoiceViewModelBase
    {
        /// <summary>
        /// Gets or sets the number of columns for choice options.
        /// </summary>
        public int ColumnsNumber { get; set; }
    }
}
