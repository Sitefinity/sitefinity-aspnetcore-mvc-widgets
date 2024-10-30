using System.ComponentModel;
using Progress.Sitefinity.Renderer.Designers.Attributes;

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
        [EnumDisplayName("Short text")]
        [Description("Short text")]
        ShortText,

        /// <summary>
        /// Long text type.
        /// </summary>
        [EnumDisplayName("Long text")]
        [Description("Long text")]
        LongText,

        /// <summary>
        /// Choices type.
        /// </summary>
        [EnumDisplayName("Choices")]
        [Description("Choices")]
        Choices,

        /// <summary>
        /// Yes/No type.
        /// </summary>
        [EnumDisplayName("Yes / No")]
        [Description("Yes / No")]
        YesNo,

        /// <summary>
        /// Date and Time type.
        /// </summary>
        [EnumDisplayName("Date and time")]
        [Description("Date and time")]
        DateAndTime,

        /// <summary>
        /// Number (whole) type.
        /// </summary>
        [EnumDisplayName("Number (whole)")]
        [Description("Number (whole)")]
        NumberWhole,

        /// <summary>
        /// Number (decimal) type.
        /// </summary>
        [EnumDisplayName("Number (decimal)")]
        [Description("Number (decimal)")]
        NumberDecimal,

        /// <summary>
        /// Classification type.
        /// </summary>
        [EnumDisplayName("Classification")]
        [Description("Classification")]
        Classification,
    }
}
