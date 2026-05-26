using System;
using System.Globalization;
using Progress.Sitefinity.RestSdk.Filters;
using Progress.Sitefinity.RestSdk.OData;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Search.Filters
{
    /// <summary>
    /// Provides metadata and utility methods for handling filter operations in search scenarios.
    /// </summary>
    /// <remarks>The SearchFilterMetadataProvider class implements the IFilterMetadataProvider interface to supply
    /// information about filter fields, related types, and property characteristics. It also offers serialization support
    /// for filter values. This class is intended to be used in contexts where dynamic filtering and metadata extraction
    /// are required, such as building search queries or filter UIs.</remarks>
    public class SearchFilterMetadataProvider : IFilterMetadataProvider
    {
        private const string DateTimeFormat = "yyyy-MM-dd'T'HH:mm:ss.fff'Z'";

        private static readonly string[] AcceptedDateFormats =
        [
            "yyyy-MM-dd'T'HH:mm:ss.fffK",
            "yyyy-MM-dd'T'HH:mm:ssK",
            "yyyy-MM-dd'T'HH:mm:ss.fff'Z'",
            "yyyy-MM-dd'T'HH:mm:ss'Z'",
            "yyyy-MM-dd",
        ];

        /// <summary>
        /// Determines the field type associated with the specified filter clause in the given filter context.
        /// </summary>
        /// <param name="clause">The filter clause for which to determine the field type.</param>
        /// <param name="filterContext">The context in which the filter is applied. This may influence how the filter clause is interpreted.</param>
        /// <returns>The field type corresponding to the provided filter clause. This implementation always returns <see
        /// cref="FieldType.Text"/>.</returns>
        public FieldType GetFieldType(FilterClause clause, FilterContext filterContext)
        {
            return FieldType.Text;
        }

        /// <summary>
        /// Gets the type associated with the specified filter context.
        /// </summary>
        /// <param name="filter">The relation filter that determines the context for type retrieval.</param>
        /// <param name="filterContext">The filter context containing the type information to be retrieved.</param>
        /// <returns>The type value from the provided filter context.</returns>
        public string GetRelatedType(RelationFilter filter, FilterContext filterContext)
        {
            return filterContext.Type;
        }

        /// <summary>
        /// Determines whether the property specified in the filter clause represents a collection type.
        /// </summary>
        /// <remarks>Use this method to validate or handle filter clauses that may target collection
        /// properties, ensuring correct processing in filtering operations.</remarks>
        /// <param name="filterContext">The context in which the filter is being evaluated. Provides information necessary for interpreting the
        /// filter clause.</param>
        /// <param name="clause">The filter clause that identifies the property to check for collection type.</param>
        /// <returns>true if the property is a collection type; otherwise, false.</returns>
        public bool IsPropertyACollection(FilterContext filterContext, FilterClause clause)
        {
            return clause.Operator == FilterClause.Operators.ContainsAnd
                || clause.Operator == FilterClause.Operators.ContainsOr
                || clause.Operator == FilterClause.Operators.In
                || clause.Operator == FilterClause.StringOperators.Contains
                || clause.Operator == FilterClause.Operators.DoesNotContain;
        }

        /// <summary>
        /// Attempts to convert the specified filter value to its string representation based on its type.
        /// </summary>
        /// <remarks>Boolean values are serialized as 'true' or 'false'. Numeric values are formatted
        /// using the invariant culture. Other values are converted to strings, with single quotes escaped and the
        /// result enclosed in single quotes.</remarks>
        /// <param name="propName">The name of the property associated with the filter value to serialize.</param>
        /// <param name="value">The value to serialize. Supported types include Boolean, numeric types, and strings.</param>
        /// <param name="filterContext">The context in which the filter is being applied. Provides additional information relevant to serialization.</param>
        /// <param name="result">When this method returns, contains the serialized string representation of the value if serialization
        /// succeeds; otherwise, an undefined value.</param>
        /// <returns>true if the value was successfully serialized; otherwise, false.</returns>
        public bool TrySerializeFilterValue(string propName, object value, FilterContext filterContext, out string result)
        {
            if (value is bool boolVal)
            {
                result = boolVal ? "true" : "false";
                return true;
            }
            else if (value is int || value is long || value is double || value is float || value is decimal)
            {
                result = string.Format(CultureInfo.InvariantCulture, "{0}", value);
                return true;
            }
            else if (value is DateTime dt)
            {
                result = new DateTimeOffset(dt.Kind == DateTimeKind.Unspecified ? DateTime.SpecifyKind(dt, DateTimeKind.Utc) : dt).ToString(DateTimeFormat, CultureInfo.InvariantCulture);
                return true;
            }
            else if (value is DateTimeOffset date)
            {
                result = date.ToString(DateTimeFormat, CultureInfo.InvariantCulture);
                return true;
            }
            else if (value is string strValue && DateTimeOffset.TryParseExact(strValue, AcceptedDateFormats, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out date))
            {
                result = date.ToString(DateTimeFormat, CultureInfo.InvariantCulture);
                return true;
            }

            var escaped = value?.ToString()?.Replace("'", "''", StringComparison.Ordinal);
            result = string.Format(CultureInfo.InvariantCulture, "'{0}'", escaped);

            return true;
        }
    }
}
