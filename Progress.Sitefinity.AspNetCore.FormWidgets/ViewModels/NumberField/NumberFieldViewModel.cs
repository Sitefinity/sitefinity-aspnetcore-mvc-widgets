namespace Progress.Sitefinity.AspNetCore.FormWidgets.ViewModels.NumberField
{
    /// <summary>
    /// The view model for the text field widget.
    /// </summary>
    public class NumberFieldViewModel
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
        /// Gets or sets the placeholder text.
        /// </summary>
        public string PlaceholderText { get; set; }

        /// <summary>
        /// Gets or sets the field name.
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// Gets or sets the predefined value of the forms field.
        /// </summary>
        public decimal PredefinedValue { get; set; }

        /// <summary>
        /// Gets or sets the CSS class.
        /// </summary>
        public string CssClass { get; set; }

        /// <summary>
        /// Gets or sets the input CSS class.
        /// </summary>
        public string InputCssClass { get; set; }

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
        /// Gets or sets a value indicating whether to allow decimals.
        /// </summary>
        public bool AllowDecimals { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this field is readonly.
        /// </summary>
        public bool Readonly { get; set; }

        /// <summary>
        /// Gets or sets the type of the affix.
        /// </summary>
        /// <value>
        /// The type of the affix.
        /// </value>
        public AffixType AffixType { get; set; }

        /// <summary>
        /// Gets or sets the affix text.
        /// </summary>
        /// <value>
        /// The affix text.
        /// </value>
        public string AffixText { get; set; }

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
