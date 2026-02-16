using System.ComponentModel;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.IntentDrivenContent
{
    /// <summary>
    /// Defines the types of sections available for the dynamically generated widget.
    /// </summary>
    public enum SectionType
    {
        /// <summary>
        /// Title and Summary section type.
        /// </summary>
        [Description("titleandsummary")]
        TitleAndSummary,

        /// <summary>
        /// Rich Text section type.
        /// </summary>
        [Description("richtext")]
        RichText,

        /// <summary>
        /// FAQ section type.
        /// </summary>
        [Description("faq")]
        FAQ,

        /// <summary>
        /// Hero section type.
        /// </summary>
        [Description("herosection")]
        Hero,

        /// <summary>
        /// Content List section type.
        /// </summary>
        [Description("contentlist_list")]
        ContentList,

        /// <summary>
        /// Content List section type.
        /// </summary>
        [Description("contentlist_cards")]
        ContentListCards,

        /// <summary>
        /// Error section type.
        /// </summary>
        [Description("error")]
        Error
    }
}
