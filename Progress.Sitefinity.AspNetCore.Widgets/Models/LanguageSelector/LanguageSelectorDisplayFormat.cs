using System.ComponentModel;
using Progress.Sitefinity.Renderer.Designers.Attributes;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.LanguageSelector
{
    /// <summary>
    /// Enumeration for specifying the language display format.
    /// </summary>
    public enum LanguageSelectorDisplayFormat
    {
        /// <summary>
        /// Display languages in native language format.
        /// </summary>
        [EnumDisplayName("In native language (e.g., français, português)")]
        [Description("In native language (e.g., français, português)")]
        Native,

        /// <summary>
        /// Display languages in native language with capitalized first letter.
        /// </summary>
        [EnumDisplayName("In native language, capitalized (e.g., Français, Português)")]
        [Description("In native language, capitalized (e.g., Français, Português)")]
        NativeCapitalized,

        /// <summary>
        /// Display languages in English.
        /// </summary>
        [EnumDisplayName("In English (e.g., French, Portuguese)")]
        [Description("In English (e.g., French, Portuguese)")]
        English
    }
}
