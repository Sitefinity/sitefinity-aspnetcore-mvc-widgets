namespace Progress.Sitefinity.AspNetCore.FormWidgets.Entities.TextField
{
    /// <summary>
    /// Enumeration defines possible types for text field input type.
    /// </summary>
    public enum TextType
    {
        /// <summary>
        /// Default. Defines a single-line text field.
        /// </summary>
        Text,

        /// <summary>
        /// Defines a field for an e-mail address
        /// </summary>
        Email,

        /// <summary>
        /// Defines a field for entering a telephone number
        /// </summary>
        Phone,

        /// <summary>
        /// Defines a field for entering a URL
        /// </summary>
        URL,
    }
}
