using System.Collections.Generic;
using Progress.Sitefinity.AspNetCore.Models;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Common;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.LoginForm
{
    /// <summary>
    /// The view model for the Login form widget.
    /// </summary>
    public class LoginFormViewModel : ExternalLoginBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoginFormViewModel"/> class.
        /// </summary>
        public LoginFormViewModel()
        {
            this.Labels = new LoginFormLabelsViewModel();
        }

        /// <summary>
        /// Gets or sets the login handler path.
        /// </summary>
        public string LoginHandlerPath { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the rememberMe checkbox is checked.
        /// </summary>
        public bool RememberMe { get; set; }

        /// <summary>
        /// Gets or sets the membership provider name.
        /// </summary>
        public string MembershipProviderName { get; set; }

        /// <summary>
        /// Gets or sets the forgotten password link.
        /// </summary>
        public string ForgottenPasswordLink { get; set; }

        /// <summary>
        /// Gets or sets the registration link.
        /// </summary>
        public string RegistrationLink { get; set; }

        /// <summary>
        /// Gets or sets the labels.
        /// </summary>
        /// <value>
        /// The labels.
        /// </value>
        public LoginFormLabelsViewModel Labels { get; set; }

        /// <summary>
        /// Gets or sets the html form class attribute values.
        /// </summary>
        public string CssClass { get; set; }

        /// <summary>
        /// Gets or sets the attributes for the widget.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Must be able to set in property editor.")]
        public IDictionary<string, IList<AttributeModel>> Attributes { get; set; }
    }
}
