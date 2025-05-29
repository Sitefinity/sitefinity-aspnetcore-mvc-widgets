using System;
using System.Collections.Generic;
using Progress.Sitefinity.AspNetCore.Models;
using Progress.Sitefinity.RestSdk.Dto;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.ContentList
{
    /// <summary>
    /// The base content list view model.
    /// </summary>
    public class ContentListViewModelBase
    {
        /// <summary>
        /// Gets or sets the attributes for navigation widget.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Must be able to set in property editor.")]
        public IDictionary<string, IList<AttributeModel>> Attributes { get; set; }

        /// <summary>
        /// Gets or sets the type of the content items selected.
        /// </summary>
        public string Type { get; set; }
    }
}
