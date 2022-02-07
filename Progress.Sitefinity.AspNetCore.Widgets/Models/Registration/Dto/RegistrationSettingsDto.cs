namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Registration.Dto
{
    /// <summary>
    /// Represents registration settings dto.
    /// </summary>
    internal class RegistrationSettingsDto
    {
        /// <summary>
        /// Gets or sets a value indicating whether the membership provider settings require question and answer for reset/retrieval password functionality.
        /// </summary>
        /// <value>
        /// <c>true</c> if the membership provider requires question and answer; otherwise, <c>false</c>.
        /// </value>
        public bool RequiresQuestionAndAnswer { get; set; }

        /// <summary>
        /// Gets or sets a value indicating if the user account is activated immediately after registration or after confirmation.
        /// </summary>
        public string ActivationMethod { get; set; }
    }
}
