using System.Collections.Generic;
using Progress.Sitefinity.AspNetCore.Models;
using Progress.Sitefinity.AspNetCore.ViewComponents;

namespace Progress.Sitefinity.AspNetCore.FormWidgets.ViewModels.FormPage
{
    /// <summary>
    /// The view model for the form page widget.
    /// </summary>
    public class FormPageViewModel
    {
        /// <summary>
        /// Gets or sets the page label.
        /// </summary>
        public string PageLabel { get; set; }

        /// <summary>
        /// Gets or sets the button label.
        /// </summary>
        public string ButtonLabel { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the user is allowed to step backward.
        /// </summary>
        public bool AllowStepBackward { get; set; }

        /// <summary>
        /// Gets or sets the back link label.
        /// </summary>
        public string BackLinkLabel { get; set; }

        /// <summary>
        /// Gets or sets the CSS class.
        /// </summary>
        public string CssClass { get; set; }

        /// <summary>
        /// Gets or sets the view context.
        /// </summary>
        public ICompositeViewComponentContext Context { get; set; }
    }
}
