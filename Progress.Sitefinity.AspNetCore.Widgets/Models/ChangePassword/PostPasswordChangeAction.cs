using System.ComponentModel;

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
        [Description("View a message")]
        ViewAMessage = 0,

        /// <summary>
        /// Redirects to specific Sitefinity page.
        /// </summary>
        [Description("Redirect to page...")]
        RedirectToPage = 1,
    }
}
