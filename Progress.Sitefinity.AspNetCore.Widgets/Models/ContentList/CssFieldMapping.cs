namespace Progress.Sitefinity.AspNetCore.Widgets.Models.ContentList
{
    /// <summary>
    /// Defines a mapping between a css class and a field.
    /// </summary>
    public class CssFieldMapping
    {
        /// <summary>
        /// Gets or sets the field name.
        /// <remarks>This name is ment to be displayed in the widget editor and in the widget templates.</remarks>
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// Gets or sets the css class.
        /// </summary>
        public string CssClass { get; set; }
    }
}
