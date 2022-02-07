using System;
using Progress.Sitefinity.AspNetCore.FormWidgets.ViewModels.Common;

namespace Progress.Sitefinity.AspNetCore.FormWidgets.ViewModels.TextField
{
    /// <summary>
    /// The view model for the text field widget.
    /// </summary>
    public class TextFieldViewModel : TextViewModelBase
    {
        /// <summary>
        /// Gets or sets the type of the input element.
        /// </summary>
        public string InputType { get; set; }
    }
}
