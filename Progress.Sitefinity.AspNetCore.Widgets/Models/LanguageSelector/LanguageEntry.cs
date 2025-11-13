namespace Progress.Sitefinity.AspNetCore.Widgets.Models.LanguageSelector
{
    /// <summary>
    /// Represents an entry for a language, including its name, value, selection status,
    /// translation status, and associated URLs.
    /// </summary>
    public class LanguageEntry
    {
        /// <summary>
        /// Gets or sets the name of the language.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the language value or code (e.g., "en", "fr").
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this language is currently selected.
        /// </summary>
        public bool Selected { get; set; }

        /// <summary>
        /// Gets or sets the URL of the current page in this language.
        /// </summary>
        public string PageUrl { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the content has been translated into this language.
        /// </summary>
        public bool IsTranslated { get; set; }

        /// <summary>
        /// Gets or sets the URL of the localized home page for this language.
        /// </summary>
        public string LocalizedHomePageUrl { get; set; }
    }
}
