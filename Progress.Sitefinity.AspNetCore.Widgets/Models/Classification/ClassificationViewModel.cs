using System.Collections.Generic;
using Progress.Sitefinity.AspNetCore.Models;
using Progress.Sitefinity.RestSdk.Dto;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Classification
{
    /// <summary>
    /// The view model for the Classification widget.
    /// </summary>
    public class ClassificationViewModel
    {
        /// <summary>
        /// Gets or sets the taxons.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Needed to be set in the model.")]
        public IEnumerable<TaxonDto> Taxons { get; set; } = new List<TaxonDto>();

        /// <summary>
        /// Gets or sets a value indicating whether the item count should be shown.
        /// </summary>
        public bool ShowItemCount { get; set; }

        /// <summary>
        /// Gets or sets the view name.
        /// </summary>
        public string ViewName { get; set; }

        /// <summary>
        /// Gets or sets the custom css class name.
        /// </summary>
        public string CssClass { get; set; }

        /// <summary>
        /// Gets or sets the attributes for the search box.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Must be able to set in property editor.")]
        public IDictionary<string, IList<AttributeModel>> Attributes { get; set; }

        internal const string ClassificationDefaultUlrSegment = "-in-{{classificationName}}-{{taxaUrl}}";
        internal const string ClassificationSlot = "{{classificationName}}";
    }
}
