using System.ComponentModel;
using Progress.Sitefinity.Renderer.Designers.Attributes;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Form
{
    /// <summary>
    /// The form submit action options for the Form widget.
    /// </summary>
    /// <remarks>
    /// Each option describes different confirmation action that will be executed when the form is submitted.
    /// </remarks>
    public enum FormSubmitAction
    {
        /// <summary>
        /// Refers to the action that is set in the forms designer
        /// </summary>
        [EnumDisplayName("As set in the form")]
        [Description("As set in the form")]
        AsSetInForm,

        /// <summary>
        /// Refers to the action of displaying a custom message.
        /// </summary>
        [EnumDisplayName("Custom message")]
        [Description("Custom message")]
        Message,

        /// <summary>
        /// Refers to the action of redirecting to a custom page.
        /// </summary>
        [EnumDisplayName("Custom redirect to a page")]
        [Description("Custom redirect to a page")]
        Redirect,
    }
}
