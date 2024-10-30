using System;
using System.Collections.Generic;
using Progress.Sitefinity.AspNetCore.Configuration;
using Progress.Sitefinity.AspNetCore.Models;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Profile
{
    /// <summary>
    /// The view model for the Profile widget.
    /// </summary>
    public class ProfileViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileViewModel"/> class.
        /// </summary>
        public ProfileViewModel()
        {
            this.Labels = new ProfileLabelsViewModel();
        }

        /// <summary>
        /// Gets or sets the labels.
        /// </summary>
        /// <value>
        /// The labels.
        /// </value>
        public ProfileLabelsViewModel Labels { get; set; }

        /// <summary>
        /// Gets or sets the ViewMode.
        /// </summary>
        public ProfileViewMode ViewMode { get; set; }

        /// <summary>
        /// Gets or sets the EditModeAction.
        /// </summary>
        public EditModeAction EditModeAction { get; set; }

        /// <summary>
        /// Gets or sets the ReadAndEditModeAction.
        /// </summary>
        public ReadEditModeAction ReadEditModeAction { get; set; }

        /// <summary>
        /// Gets or sets the EditModeRedirectUrl.
        /// </summary>
        public string EditModeRedirectUrl { get; set; }

        /// <summary>
        /// Gets or sets the ReadAndEditModeRedirectUrl.
        /// </summary>
        public string ReadEditModeRedirectUrl { get; set; }

        /// <summary>
        /// Gets or sets the UpdateProfile handler path.
        /// </summary>
        public string UpdateProfileHandlerPath { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the FirstName field.
        /// </summary>
        /// <value>
        /// The FirstName field.
        /// </value>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the LastName field.
        /// </summary>
        /// <value>
        /// The LastName field.
        /// </value>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the Email field.
        /// </summary>
        /// <value>
        /// The Email field.
        /// </value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the Provider name.
        /// </summary>
        /// <value>
        /// The Provider name.
        /// </value>
        public string ProviderName { get; set; }

        /// <summary>
        /// Gets or sets the Nickname field.
        /// </summary>
        /// <value>
        /// The Nickname field.
        /// </value>
        public string Nickname { get; set; }

        /// <summary>
        /// Gets or sets the About field.
        /// </summary>
        /// <value>
        /// The About field.
        /// </value>
        public string About { get; set; }

        /// <summary>
        /// Gets or sets the html form class attribute values.
        /// </summary>
        public string CssClass { get; set; }

        /// <summary>
        /// Gets or sets the avatar url attribute values.
        /// </summary>
        public string AvatarUrl { get; set; }

        /// <summary>
        /// Gets or sets the invalid class.
        /// </summary>
        public string InvalidClass { get; set; }

        /// <summary>
        /// Gets or sets the custom fields.
        /// </summary>
        public IDictionary<string, object> CustomFields { get; set; }

        /// <summary>
        /// Gets or sets the read-only fields.
        /// </summary>
        public IEnumerable<string> ReadOnlyFields { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the current request is for email change confirmation.
        /// </summary>
        public bool ConfirmEmailChangeRequest { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the current request is for email change confirmation.
        /// </summary>
        public bool ConfirmEmailChangeError { get; set; }

        /// <summary>
        /// Gets or sets the account activation method.
        /// </summary>
        public string ActivationMethod { get; set; }

        /// <summary>
        /// Gets or sets the attributes for the widget.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Must be able to set in property editor.")]
        public IDictionary<string, IList<AttributeModel>> Attributes { get; set; }

        /// <summary>
        /// Gets or sets the attributes for the columns and for the section.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Must be able to set in the model.")]
        public IDictionary<VisibilityStyle, string> VisibilityClasses { get; set; }

        /// <summary>
        /// Gets or sets the attributes for the columns and for the section.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Must be able to set in the model.")]
        public IEnumerable<string> AllowedAvatarFormat { get; set; }

        /// <summary>
        /// Gets or sets the send again activation link url.
        /// </summary>
        public string SendAgainActivationLinkUrl { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the activation link will be shown.
        /// </summary>
        public bool ShowSendAgainActivationLink { get; set; }

        /// <summary>
        /// Gets or sets a warning.
        /// </summary>
        public string Warning { get; set; }
    }
}
