using System.Collections.Generic;
using Progress.Sitefinity.AspNetCore.Models;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.ChangePassword
{
    /// <summary>
    /// The view model for the Change password widget.
    /// </summary>
    public class ChangePasswordViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChangePasswordViewModel"/> class.
        /// </summary>
        public ChangePasswordViewModel()
        {
            this.Labels = new ChangePasswordLabelsViewModel();
        }

        /// <summary>
        /// Gets or sets the change password handler path.
        /// </summary>
        public string ChangePasswordHandlerPath { get; set; }

        /// <summary>
        /// Gets or sets the html form class attribute values.
        /// </summary>
        public string CssClass { get; set; }

        /// <summary>
        /// Gets or sets the labels.
        /// </summary>
        public ChangePasswordLabelsViewModel Labels { get; set; }

        /// <summary>
        /// Gets or sets the redirect url.
        /// </summary>
        /// <value>
        /// The redirect url.
        /// </value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1056:URI-like properties should not be strings", Justification = "Reviewed.")]
        public string RedirectUrl { get; set; }

        /// <summary>
        /// Gets or sets the post password change message.
        /// </summary>
        /// <value>
        /// The post password change message.
        /// </value>
        public string PostPasswordChangeMessage { get; set; }

        /// <summary>
        /// Gets or sets post password change action.
        /// </summary>
        /// <value>
        /// The post change action.
        /// </value>
        public PostPasswordChangeAction PostPasswordChangeAction { get; set; }

        /// <summary>
        /// Gets or sets the external provider name.
        /// </summary>
        public string ExternalProviderName { get; set; }

        /// <summary>
        /// Gets or sets the attributes for the widget.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Must be able to set in property editor.")]
        public IDictionary<string, IList<AttributeModel>> Attributes { get; set; }
    }
}
