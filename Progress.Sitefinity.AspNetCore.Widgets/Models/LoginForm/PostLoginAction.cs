using System.ComponentModel;
using Progress.Sitefinity.Renderer.Designers.Attributes;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.LoginForm
{
    /// <summary>
    /// Choices for post login action for login form.
    /// </summary>
    public enum PostLoginAction
    {
        /// <summary>
        /// Does not redirect and returns to same page.
        /// </summary>
        [EnumDisplayName("Stay on the same page")]
        [Description("Stay on the same page")]
        StayOnSamePage = 0,

        /// <summary>
        /// Redirects to specific Sitefinity page.
        /// </summary>
        [EnumDisplayName("Redirect to page...")]
        [Description("Redirect to page...")]
        RedirectToPage = 1,
    }
}
