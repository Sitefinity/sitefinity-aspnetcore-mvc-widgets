namespace Progress.Sitefinity.AspNetCore.Widgets.Models.ChangePassword
{
    /// <summary>
    /// Labels view model.
    /// </summary>
    public class ChangePasswordLabelsViewModel
    {
        /// <summary>
        /// Gets or sets the header.
        /// </summary>
        public string Header { get; set; }

        /// <summary>
        /// Gets or sets the current password label.
        /// </summary>
        public string OldPassword { get; set; }

        /// <summary>
        /// Gets or sets the new password label.
        /// </summary>
        public string NewPassword { get; set; }

        /// <summary>
        /// Gets or sets the confirm password label.
        /// </summary>
        public string RepeatPassword { get; set; }

        /// <summary>
        /// Gets or sets the submit button label.
        /// </summary>
        /// <value>
        /// The submit button label.
        /// </value>
        public string SubmitButtonLabel { get; set; }

        /// <summary>
        /// Gets or sets the login first message.
        /// </summary>
        /// <value>
        /// The Login first message.
        /// </value>
        public string LoginFirstMessage { get; set; }

        /// <summary>
        /// Gets or sets the validation required message.
        /// </summary>
        /// <value>
        /// The validation required message.
        /// </value>
        public string ValidationRequiredMessage { get; set; }

        /// <summary>
        /// Gets or sets the validation mismatch message.
        /// </summary>
        /// <value>
        /// The validation mismatch message.
        /// </value>
        public string ValidationMismatchMessage { get; set; }

        /// <summary>
        /// Gets or sets the external provider message.
        /// </summary>
        /// <value>
        /// The external provider message.
        /// </value>
        public string ExternalProviderMessageFormat { get; set; }
    }
}
