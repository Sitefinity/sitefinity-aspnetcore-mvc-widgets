using System.Collections.Generic;
using Progress.Sitefinity.AspNetCore.Models;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Common;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Registration
{
    /// <summary>
    /// The view model for the Registration widget.
    /// </summary>
    public class RegistrationViewModel : ExternalLoginBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationViewModel"/> class.
        /// </summary>
        public RegistrationViewModel()
        {
            this.Labels = new RegistrationLabelsViewModel();
        }

        /// <summary>
        /// Gets or sets the registration handler path.
        /// </summary>
        public string RegistrationHandlerPath { get; set; }

        /// <summary>
        /// Gets or sets the resend confirmation email handler path.
        /// </summary>
        public string ResendConfirmationEmailHandlerPath { get; set; }

        /// <summary>
        /// Gets or sets the labels.
        /// </summary>
        /// <value>
        /// The labels.
        /// </value>
        public RegistrationLabelsViewModel Labels { get; set; }

        /// <summary>
        /// Gets or sets the html form class attribute values.
        /// </summary>
        public string CssClass { get; set; }

        /// <summary>
        /// Gets or sets the login page url.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1056:URI-like properties should not be strings", Justification = "Reviewed.")]
        public string LoginPageUrl { get; set; }

        /// <summary>
        /// Gets or sets the post registration action.
        /// </summary>
        public PostRegistrationAction PostRegistrationAction { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to require qustion and answer for the registration.
        /// </summary>
        public bool RequiresQuestionAndAnswer { get; set; }

        /// <summary>
        /// Gets or sets the activation method.
        /// </summary>
        public string ActivationMethod { get; set; }

        /// <summary>
        /// Gets or sets the attributes for the widget.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Must be able to set in property editor.")]
        public IDictionary<string, IList<AttributeModel>> Attributes { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the widget is in activation mode.
        /// </summary>
        public bool IsAccountActivationRequest { get; set; }

        /// <summary>
        /// Gets or sets the activation page url.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1056:URI-like properties should not be strings", Justification = "Reviewed.")]
        public string ActivationPageUrl { get; set; }

        /// <inheritdoc/>
        protected override bool ShowSuccessMessageQueryParameter => true;
    }
}
