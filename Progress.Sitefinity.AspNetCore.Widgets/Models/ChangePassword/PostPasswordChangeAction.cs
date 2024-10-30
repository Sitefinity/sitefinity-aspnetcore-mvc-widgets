using System.ComponentModel;
using Progress.Sitefinity.Renderer.Designers.Attributes;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.ChangePassword
{
    /// <summary>
    /// Choices for post change password action.
    /// </summary>
    public enum PostPasswordChangeAction
    {
        /// <summary>
        /// Does not redirect and displays a message.
        /// </summary>
        [EnumDisplayName("View a message")]
        [Description("View a message")]
        ViewAMessage = 0,

        /// <summary>
        /// Redirects to specific Sitefinity page.
        /// </summary>
        [EnumDisplayName("Redirect to page...")]
        [Description("Redirect to page...")]
        RedirectToPage = 1,
    }
}
