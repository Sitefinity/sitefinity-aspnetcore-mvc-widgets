namespace Progress.Sitefinity.AspNetCore.FormWidgets.ViewModels.DateTimeField
{
    /// <summary>
    /// The view model for the date form field widget.
    /// </summary>
    public class DateTimeFieldViewModel
    {
        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// Gets or sets the instructional text.
        /// </summary>
        public string InstructionalText { get; set; }

        /// <summary>
        /// Gets or sets the field name.
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// Gets or sets the input type.
        /// </summary>
        public string InputType { get; set; }

        /// <summary>
        /// Gets or sets the CSS class.
        /// </summary>
        public string CssClass { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this field is readonly.
        /// </summary>
        public bool Readonly { get; set; }

        /// <summary>
        /// Gets or sets the violation restrictions json.
        /// </summary>
        public string ViolationRestrictionsJson { get; set; }

        /// <summary>
        /// Gets or sets the violation restrictions messages.
        /// </summary>
        public string ViolationRestrictionsMessages { get; set; }

        /// <summary>
        /// Gets or sets the validation attributes.
        /// </summary>
        public string ValidationAttributes { get; set; }

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
