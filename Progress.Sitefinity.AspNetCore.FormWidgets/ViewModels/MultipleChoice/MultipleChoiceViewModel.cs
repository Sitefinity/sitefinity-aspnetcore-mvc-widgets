using Progress.Sitefinity.AspNetCore.FormWidgets.ViewModels.Common;

namespace Progress.Sitefinity.AspNetCore.FormWidgets.ViewModels.MultipleChoice
{
    /// <summary>
    /// The view model for the multiple choice widget.
    /// </summary>
    public class MultipleChoiceViewModel : ChoiceViewModelBase
    {
        /// <summary>
        /// Gets or sets a value indicating whether an additional choice should be added.
        /// </summary>
        public bool HasAdditionalChoice { get; set; }

        /// <summary>
        /// Gets or sets the number of columns for choice options.
        /// </summary>
        public int ColumnsNumber { get; set; }
    }
}
