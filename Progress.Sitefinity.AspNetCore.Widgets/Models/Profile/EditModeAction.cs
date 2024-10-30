using System.ComponentModel;
using Progress.Sitefinity.Renderer.Designers.Attributes;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Profile
{
    /// <summary>
    /// The actions that can be performed in Edit mode post update/after saving.
    /// </summary>
    public enum EditModeAction
    {
        /// <summary>
        /// Displays a message.
        /// </summary>
        [EnumDisplayName("View a message")]
        [Description("View a message")]
        ViewMessage = 0,

        /// <summary>
        /// Redirects to specific Sitefinity page.
        /// </summary>
        [Description("Redirect to page...")]
        [EnumDisplayName("Redirect to page...")]
        RedirectToPage = 1,
    }
}
