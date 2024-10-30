using System.ComponentModel;
using Progress.Sitefinity.Renderer.Designers.Attributes;

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
        [EnumDisplayName("None")]
        [Description("None")]
        None,

        /// <summary>
        /// The small.
        /// </summary>
        [EnumDisplayName("S")]
        [Description("Small")]
        S,

        /// <summary>
        /// The medium.
        /// </summary>
        [EnumDisplayName("M")]
        [Description("Medium")]
        M,

        /// <summary>
        /// The large.
        /// </summary>
        [EnumDisplayName("L")]
        [Description("Large")]
        L,
    }
}
