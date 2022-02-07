using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Progress.Sitefinity.AspNetCore.ViewComponents;

namespace Progress.Sitefinity.AspNetCore.FormWidgets.Models.ContentBlock
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
    }
}
