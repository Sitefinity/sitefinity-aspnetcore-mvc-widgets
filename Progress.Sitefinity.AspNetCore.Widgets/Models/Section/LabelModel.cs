using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Progress.Sitefinity.Renderer.Designers.Attributes;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Section
{
    /// <summary>
    /// Defines label model.
    /// </summary>
    public class LabelModel
    {
        /// <summary>
        /// Gets or sets the CSS class.
        /// </summary>
        [DisplayName("LABEL")]
        [Placeholder("type a label...")]
        [MaxLength(30)]
        public string Label { get; set; }
    }
}
