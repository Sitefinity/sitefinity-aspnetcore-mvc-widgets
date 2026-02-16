using System.Collections.Generic;
using Progress.Sitefinity.AspNetCore.Models;
using Progress.Sitefinity.RestSdk.Clients.Pages.Dto;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.IntentDrivenContent
{
    /// <summary>
    /// The view model for the IntentDrivenContent widget.
    /// </summary>
    public class IntentDrivenContentViewModel
    {
        /// <summary>
        /// Gets or sets the sections to be displayed.
        /// </summary>
        public IList<SectionViewModel> SectionsConfiguration { get; set; } = new List<SectionViewModel>();

        /// <summary>
        /// Gets or sets the names of the sections.
        /// </summary>
        public IList<string> SectionNames { get; set; } = new List<string>();

        /// <summary>
        /// Gets or sets the default query to be used when no user query is provided.
        /// </summary>
        public string DefaultQuery { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the Sitefinity front end service URL.
        /// </summary>
        public string ServiceUrl { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the CSS class.
        /// </summary>
        public string CssClass { get; set; }
    }
}
