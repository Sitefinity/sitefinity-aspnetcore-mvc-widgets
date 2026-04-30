using System.Collections.Generic;

namespace Progress.Sitefinity.AspNetCore.FormWidgets.ViewModels.FormNavigation
{
    /// <summary>
    /// The view model for the form navigation widget.
    /// </summary>
    public class FormNavigationViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormNavigationViewModel"/> class.
        /// </summary>
        public FormNavigationViewModel()
        {
            this.NavigationSteps = new List<string>();
        }

        /// <summary>
        /// Gets or sets the CSS class.
        /// </summary>
        public string CssClass { get; set; }

        /// <summary>
        /// Gets or sets the navigation steps.
        /// </summary>
        public IEnumerable<string> NavigationSteps { get; set; }
    }
}
