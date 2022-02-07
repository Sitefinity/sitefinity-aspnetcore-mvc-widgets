using System.ComponentModel;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Registration
{
    /// <summary>
    /// Choices for post registration action.
    /// </summary>
    public enum PostRegistrationAction
    {
        /// <summary>
        /// Displays a message.
        /// </summary>
        [Description("View a message")]
        ViewMessage = 0,

        /// <summary>
        /// Redirects to specific Sitefinity page.
        /// </summary>
        [Description("Redirect to page...")]
        RedirectToPage = 1,
    }
}
