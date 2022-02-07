using System;
using System.Globalization;
using System.Text;
using System.Web;
using Newtonsoft.Json.Linq;
using Progress.Sitefinity.AspNetCore.FormWidgets.Entities.Common;
using Progress.Sitefinity.AspNetCore.FormWidgets.ViewComponents.Common;
using Progress.Sitefinity.AspNetCore.FormWidgets.ViewModels.Common;

namespace Progress.Sitefinity.AspNetCore.FormWidgets.Models.Common
{
    /// <summary>
    /// Base class for form text fields.
    /// </summary>
    /// <typeparam name="TTextEntity">The text entity type.</typeparam>
    public abstract class TextModelBase<TTextEntity>
        where TTextEntity : TextEntityBase
    {
        private const string TextLengthDefaultValidationMessage = "{0} field input is too long";
        private const string RequiredDefaultValidationMessage = "{0} field is required";
        private const string InvalidDefaultValidationMessage = "{0} field is invalid";
        private const string RegularExpressionDefaultValidationMessage = "Please match the requested format";
        private FormWidgetsStyleGenerator formWidgetsStyleGenerator;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextModelBase{TTextFieldEntityBase}"/> class.
        /// </summary>
        /// <param name="formWidgetsStyleGenerator">The style generator for forms widgets.</param>
        public TextModelBase(FormWidgetsStyleGenerator formWidgetsStyleGenerator)
        {
            this.formWidgetsStyleGenerator = formWidgetsStyleGenerator;
        }

        /// <summary>
        /// Initializes the view model.
        /// </summary>
        /// <typeparam name="T">The view model class type.</typeparam>
        /// <param name="entity">The text entity.</param>
        /// <returns>The initialized view model.</returns>
        /// <exception cref="ArgumentNullException">Throws an exception if the entity param is null.</exception>
        protected T InitializeViewModel<T>(TTextEntity entity)
            where T : TextViewModelBase, new()
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var viewModel = new T();
            viewModel.CssClass = entity.CssClass + " " + this.formWidgetsStyleGenerator.GetFieldSizeCss(entity.FieldSize);
            viewModel.Label = entity.Label;
            viewModel.PlaceholderText = entity.PlaceholderText;
            viewModel.InstructionalText = entity.InstructionalText;
            viewModel.FieldName = entity.SfFieldName;
            viewModel.PredefinedValue = entity.PredefinedValue;
            viewModel.ValidationAttributes = this.BuildValidationAttributes(entity);
            viewModel.ViolationRestrictionsJson = JObject.FromObject(new
            {
                maxLength = entity.Range?.Max,
                minLength = entity.Range?.Min,
            }).ToString();
            viewModel.ViolationRestrictionsMessages = JObject.FromObject(new
            {
                invalidLength = BuildValidationMessage(entity.Label, entity.TextLengthViolationMessage, TextLengthDefaultValidationMessage),
                required = BuildValidationMessage(entity.Label, entity.RequiredErrorMessage, RequiredDefaultValidationMessage),
                invalid = BuildValidationMessage(entity.Label, InvalidDefaultValidationMessage, InvalidDefaultValidationMessage),
                regularExpression = BuildValidationMessage(entity.Label, this.GetRegularExpressionViolationMessage(entity), RegularExpressionDefaultValidationMessage),
            }).ToString();

            return viewModel;
        }

        /// <summary>
        /// Gets the regular expression.
        /// </summary>
        /// <param name="textEntity">The text entity class.</param>
        /// <returns>The regular expression.</returns>
        protected abstract string GetRegularExpression(TTextEntity textEntity);

        /// <summary>
        /// Gets the regular expression violation message.
        /// </summary>
        /// <param name="textEntity">The text entity class.</param>
        /// <returns>The regular expression violation message.</returns>
        protected abstract string GetRegularExpressionViolationMessage(TTextEntity textEntity);

        private static string BuildValidationMessage(string textFieldLabel, string actualMessage, string defaultMessage)
        {
            actualMessage = string.IsNullOrEmpty(actualMessage) ? defaultMessage : actualMessage;
            string result = string.Format(CultureInfo.InvariantCulture, actualMessage, textFieldLabel);

            return result;
        }

        private string BuildValidationAttributes(TTextEntity entity)
        {
            var attributes = new StringBuilder();

            if (entity.Required)
                attributes.Append(@"required=""required"" ");

            string regularExpression = this.GetRegularExpression(entity);
            if (!string.IsNullOrEmpty(regularExpression))
            {
#pragma warning disable CA1305 // Specify IFormatProvider
                attributes.Append($"pattern=\"{HttpUtility.HtmlAttributeEncode(regularExpression)}\" ");
#pragma warning restore CA1305 // Specify IFormatProvider
            }

            return attributes.ToString();
        }
    }
}
