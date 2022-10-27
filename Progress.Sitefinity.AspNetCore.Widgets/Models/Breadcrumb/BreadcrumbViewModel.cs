using System.Collections.Generic;
using Progress.Sitefinity.AspNetCore.Models;
using Progress.Sitefinity.RestSdk.Clients.Pages.Dto;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Breadcrumb
{
    /// <summary>
    /// The view model for the Breadcrumb widget.
    /// </summary>
    public class BreadcrumbViewModel
    {
        /// <summary>
        /// Gets or sets the message when a page is missing.
        /// </summary>
        public string PageMissingMessage { get; set; }

        /// <summary>
        /// Gets or sets the css class.
        /// </summary>
        public string CssClass { get; set; }

        /// <summary>
        /// Gets the pages in the breadcrumb.
        /// </summary>
        public List<PageNodeDto> Pages { get; } = new List<PageNodeDto>();

        /// <summary>
        /// Gets or sets the attributes for the breadcrumb.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Must be able to set in property editor.")]
        public IDictionary<string, IList<AttributeModel>> Attributes { get; set; }
    }
}
