using System;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Progress.Sitefinity.AspNetCore.FormWidgets.Entities.NumberField;
using Progress.Sitefinity.AspNetCore.FormWidgets.ViewComponents;
using Progress.Sitefinity.AspNetCore.FormWidgets.ViewComponents.Common;
using Progress.Sitefinity.AspNetCore.FormWidgets.ViewModels.NumberField;

namespace Progress.Sitefinity.AspNetCore.FormWidgets.Models.NumberField
{
    /// <summary>
    /// The model object that holds the logic for the <see cref="NumberFieldViewComponent" /> widget.
    /// </summary>
    public class NumberFieldModel : INumberFieldModel
    {
        private const string RequiredDefaultValidationMessage = "{0} field is required";
        private const string InvalidDefaultValidationMessage = "{0} field is invalid";
        private const string InvalidRangeDefaultValidationMessage = "Number is out of the allowed range";
        private const string DecimalsAreNotAllowedValidationMessage = "Decimals are not allowed";

        private FormWidgetsStyleGenerator formWidgetsStyleGenerator;

        /// <summary>
        /// Initializes a new instance of the <see cref="NumberFieldModel"/> class.
        /// </summary>
        /// <param name="formWidgetsStyleGenerator">The style generator for forms widgets.</param>
        public NumberFieldModel(FormWidgetsStyleGenerator formWidgetsStyleGenerator)
        {
            this.formWidgetsStyleGenerator = formWidgetsStyleGenerator;
        }

        /// <inheritdoc/>
        public Task<NumberFieldViewModel> InitializeViewModel(NumberFieldEntity entity)
        {
            var viewModel = new NumberFieldViewModel();
            viewModel.CssClass = entity.CssClass;

            string sizeCssClass = this.formWidgetsStyleGenerator.GetFieldSizeCss(entity.FieldSize);
            if (entity.FieldSize != "XS")
            {
                viewModel.CssClass += $" {sizeCssClass}";
            }
            else
            {
                viewModel.InputCssClass = sizeCssClass;
            }

            viewModel.Label = entity.Label;
            viewModel.PlaceholderText = entity.PlaceholderText;
            viewModel.InstructionalText = entity.InstructionalText;
            viewModel.FieldName = entity.SfFieldName;
            viewModel.PredefinedValue = entity.PredefinedValue;
            viewModel.ValidationAttributes = this.BuildValidationAttributes(entity);
            viewModel.ViolationRestrictionsJson = JObject.FromObject(new
            {
                maxValue = entity.ValueRange?.Max,
                minValue = entity.ValueRange?.Min,
            }).ToString();
            viewModel.ViolationRestrictionsMessages = JObject.FromObject(new
            {
                invalidRange = BuildValidationMessage(entity.Label, entity.ValueRangeViolationMessage, InvalidRangeDefaultValidationMessage),
                required = BuildValidationMessage(entity.Label, entity.RequiredErrorMessage, RequiredDefaultValidationMessage),
                invalid = BuildValidationMessage(entity.Label, InvalidDefaultValidationMessage, InvalidDefaultValidationMessage),
                step = BuildValidationMessage(entity.Label, DecimalsAreNotAllowedValidationMessage, DecimalsAreNotAllowedValidationMessage),
            }).ToString();

            if (entity.PrefixOrSuffix.ChoiceValue != 0 && !string.IsNullOrWhiteSpace(entity.PrefixOrSuffix.TextValue))
            {
                viewModel.AffixType = (AffixType)entity.PrefixOrSuffix.ChoiceValue;
                viewModel.AffixText = entity.PrefixOrSuffix.TextValue;
            }

            return Task.FromResult(viewModel);
        }

        private static string BuildValidationMessage(string textFieldLabel, string actualMessage, string defaultMessage)
        {
            actualMessage = string.IsNullOrEmpty(actualMessage) ? defaultMessage : actualMessage;
            string result = string.Format(CultureInfo.InvariantCulture, actualMessage, textFieldLabel);

            return result;
        }

        private string BuildValidationAttributes(NumberFieldEntity entity)
        {
            var attributes = new StringBuilder();

            if (entity.Required)
                attributes.Append(@"required=""required"" ");

            string step = entity.AllowDecimals ? "any" : "1";
            attributes.Append(@$"step=""{step}"" ");

            if (entity.ValueRange.Min.HasValue)
            {
                attributes.Append(@$"min=""{entity.ValueRange.Min.Value}"" ");
            }

            if (entity.ValueRange.Max.HasValue)
            {
                attributes.Append(@$"max=""{entity.ValueRange.Max.Value}"" ");
            }

            return attributes.ToString();
        }
    }
}
