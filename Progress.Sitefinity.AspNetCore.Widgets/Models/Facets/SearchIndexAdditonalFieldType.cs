using System.ComponentModel;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Facets
{
    /// <summary>
    /// Search index additional filed type enumeration.
    /// </summary>
    public enum SearchIndexAdditonalFieldType
    {
        /// <summary>
        /// Short text type.
        /// </summary>
        [Description("Short text")]
        ShortText,

        /// <summary>
        /// Long text type.
        /// </summary>
        [Description("Long text")]
        LongText,

        /// <summary>
        /// Choices type.
        /// </summary>
        [Description("Choices")]
        Choices,

        /// <summary>
        /// Yes/No type.
        /// </summary>
        [Description("Yes / No")]
        YesNo,

        /// <summary>
        /// Date and Time type.
        /// </summary>
        [Description("Date and time")]
        DateAndTime,

        /// <summary>
        /// Number (whole) type.
        /// </summary>
        [Description("Number (whole)")]
        NumberWhole,

        /// <summary>
        /// Number (decimal) type.
        /// </summary>
        [Description("Number (decimal)")]
        NumberDecimal,

        /// <summary>
        /// Classification type.
        /// </summary>
        [Description("Classification")]
        Classification,
    }
}
