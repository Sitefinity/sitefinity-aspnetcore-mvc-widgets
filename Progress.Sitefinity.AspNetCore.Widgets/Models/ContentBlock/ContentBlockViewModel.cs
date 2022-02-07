using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Progress.Sitefinity.AspNetCore.Models;
using Progress.Sitefinity.AspNetCore.ViewComponents;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.ContentBlock
{
    /// <summary>
    /// The view model for the Content block widget.
    /// </summary>
    public class ContentBlockViewModel
    {
        /// <summary>
        /// Gets or sets the content of the contnet block.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the styles.
        /// </summary>
        public string WrapperCssClass { get; set; }

        /// <summary>
        /// Gets or sets the tag name.
        /// </summary>
        public string TagName { get; set; }

        /// <summary>
        /// Gets or sets the attributes for the content block.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Must be able to set in property editor.")]
        public IList<AttributeModel> Attributes { get; set; }
    }
}
