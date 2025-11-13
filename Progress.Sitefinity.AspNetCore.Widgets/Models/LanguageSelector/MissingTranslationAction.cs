using System.ComponentModel;
using Progress.Sitefinity.Renderer.Designers.Attributes;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.LanguageSelector
{
    /// <summary>
    /// Enumeration for specifying action to be taken on missing translation.
    /// </summary>
    public enum MissingTranslationAction
    {
        /// <summary>
        /// If the current page is not translated, the link will be hidden
        /// </summary>
        [EnumDisplayName("Hide the link to the missing translation")]
        [Description("Hide the link to the missing translation")]
        HideLink,

        /// <summary>
        /// If the current page is not translated, the link will lead to the home page
        /// </summary>
        [EnumDisplayName("Redirect to the home page in the language of the missing translation")]
        [Description("Redirect to the home page in the language of the missing translation")]
        RedirectToHomePage
    }
}
