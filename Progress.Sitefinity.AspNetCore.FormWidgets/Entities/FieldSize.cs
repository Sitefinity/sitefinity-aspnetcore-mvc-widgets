using System.ComponentModel;

namespace Progress.Sitefinity.AspNetCore.FormWidgets.Entities
{
    /// <summary>
    /// Styles with available offset sizes.
    /// </summary>
    public enum FieldSize
    {
        /// <summary>
        /// The none.
        /// </summary>
        [Description("None")]
        None,

        /// <summary>
        /// The small.
        /// </summary>
        [Description("Small")]
        S,

        /// <summary>
        /// The medium.
        /// </summary>
        [Description("Medium")]
        M,

        /// <summary>
        /// The large.
        /// </summary>
        [Description("Large")]
        L,
    }
}
