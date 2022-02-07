using System.ComponentModel;
using Progress.Sitefinity.Renderer.Designers.Attributes;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Section
{
    /// <summary>
    /// Defines custom CSS model.
    /// </summary>
    public class CustomCssModel
    {
        /// <summary>
        /// Gets or sets the CSS class.
        /// </summary>
        [DisplayName("CLASS")]
        [Placeholder("type CSS class...")]
        public string Class { get; set; }
    }
}
