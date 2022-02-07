namespace Progress.Sitefinity.AspNetCore.FormWidgets.ViewModels.FileField
{
    /// <summary>
    /// The view model for the text field widget.
    /// </summary>
    public class FileFieldViewModel
    {
        /// <summary>
        /// Gets or sets the CSS class.
        /// </summary>
        /// <value>
        /// The CSS class.
        /// </value>
        public string CssClass { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether a file selection is required.
        /// </summary>
        /// <value>
        /// <c>true</c> if a file selection is required; otherwise, <c>false</c>.
        /// </value>
        public bool Required { get; set; }

        /// <summary>
        /// Gets or sets the min file size in megabytes (MB).
        /// </summary>
        public int? MinFileSizeInMb { get; set; }

        /// <summary>
        /// Gets or sets the max file size in megabytes (MB).
        /// </summary>
        public int? MaxFileSizeInMb { get; set; }

        /// <summary>
        /// Gets or sets the allowed file types.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1819:Properties should not return arrays", Justification = "DTO")]
        public string[] AllowedFileTypes { get; set; }

        /// <summary>
        /// Gets or sets the file size violation message.
        /// </summary>
        /// <value>
        /// The file size violation message.
        /// </value>
        public string FileSizeViolationMessage { get; set; }

        /// <summary>
        /// Gets or sets the required violation message.
        /// </summary>
        /// <value>
        /// The required violation message.
        /// </value>
        public string RequiredViolationMessage { get; set; }

        /// <summary>
        /// Gets or sets the file type violation message.
        /// </summary>
        /// <value>
        /// The file type violation message.
        /// </value>
        public string FileTypeViolationMessage { get; set; }

        /// <summary>
        /// Gets or sets the field name.
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to allow multiple file upload.
        /// </summary>
        public bool AllowMultipleFiles { get; set; }

        /// <summary>
        /// Gets or sets the violation restrictions json.
        /// </summary>
        public string ViolationRestrictionsJson { get; set; }

        /// <summary>
        /// Gets or sets the validation attributes.
        /// </summary>
        public string ValidationAttributes { get; set; }

        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// Gets or sets the instructional text.
        /// </summary>
        public string InstructionalText { get; set; }

        /// <summary>
        /// Gets a value indicating whether the field has an instructional text.
        /// </summary>
        public bool HasDescription
        {
            get
            {
                return !string.IsNullOrEmpty(this.InstructionalText);
            }
        }
    }
}
