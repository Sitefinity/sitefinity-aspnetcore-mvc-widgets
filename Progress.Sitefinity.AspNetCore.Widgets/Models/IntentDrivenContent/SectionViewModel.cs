using System.ComponentModel;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.IntentDrivenContent
{
    /// <summary>
    /// Specifies the display variant for content lists.
    /// </summary>
    public enum ContentListVariant
    {
        /// <summary>
        /// Displays content items in a list format.
        /// </summary>
        [Description("list")]
        List,

        /// <summary>
        /// Displays content items as cards.
        /// </summary>
        [Description("cards")]
        Cards
    }

    /// <summary>
    /// Represents a section configuration for the dynamically generated widget.
    /// </summary>
    public class SectionViewModel
    {
        /// <summary>
        /// Gets or sets the section type.
        /// </summary>
        public SectionType SectionType { get; set; }

        /// <summary>
        /// Gets or sets the CSS class.
        /// </summary>
        public string CssClass { get; set; }
    }
}
