using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Progress.Sitefinity.AspNetCore.Models;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Recommendations
{
    /// <summary>
    /// The recommendations view model.
    /// </summary>
    public class RecommendationsViewModel
    {
        /// <summary>
        /// Gets or sets the conversion id.
        /// </summary>
        public int ConversionId { get; set; }

        /// <summary>
        /// Gets or sets the header.
        /// </summary>
        public string Header { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of recommendations that are to be shown.
        /// </summary>
        public int MaxNumberOfItems { get; set; }

        /// <summary>
        /// Gets or sets the unique Id.
        /// </summary>
        public Guid UniqueId { get; set; }

        /// <summary>
        /// Gets or sets the css class.
        /// </summary>
        public string CssClass { get; set; }

        /// <summary>
        /// Gets or sets the site id.
        /// </summary>
        public string SiteId { get; set; }

        /// <summary>
        /// Gets or sets the base url.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1056:URI-like properties should not be strings", Justification = "Url")]
        public string BaseUrl { get; set; }

        /// <summary>
        /// Gets or sets the attributes for the recommendations widget.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Must be able to set in property editor.")]
        public IDictionary<string, IList<AttributeModel>> Attributes { get; set; }
    }
}
