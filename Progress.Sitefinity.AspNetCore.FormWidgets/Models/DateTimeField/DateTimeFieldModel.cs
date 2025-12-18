using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Progress.Sitefinity.AspNetCore.FormWidgets.Entities.DateTimeField;
using Progress.Sitefinity.AspNetCore.FormWidgets.ViewModels.DateTimeField;
using Progress.Sitefinity.Renderer.Entities;

namespace Progress.Sitefinity.AspNetCore.FormWidgets.Models.DateField
{
    /// <summary>
    /// The model for the date form field widget.
    /// </summary>
    public class DateTimeFieldModel : IDateTimeFieldModel
    {
        private const string RequiredDefaultValidationMessage = "{0} field is required";
        private const string InvalidDefaultValidationMessage = "{0} field is invalid";

        /// <inheritdoc />
        public Task<DateTimeFieldViewModel> InitializeViewModel(DateTimeFieldEntity entity)
        {
            DateTimeFieldViewModel viewModel = new DateTimeFieldViewModel();
            viewModel.Label = entity.Label;
            viewModel.CssClass = entity.CssClass;
            viewModel.InstructionalText = entity.InstructionalText;
            viewModel.FieldName = entity.SfFieldName;
            viewModel.InputType = GetDateInputType(entity.FieldType);
            viewModel.ValidationAttributes = BuildValidationAttributes(entity);
            viewModel.ViolationRestrictionsJson = JObject.FromObject(new { }).ToString();
            viewModel.ViolationRestrictionsMessages = JObject.FromObject(new
            {
                required = BuildValidationMessage(entity.Label, entity.RequiredErrorMessage, RequiredDefaultValidationMessage),
                invalid = BuildValidationMessage(entity.Label, InvalidDefaultValidationMessage, InvalidDefaultValidationMessage),
            }).ToString();

            return Task.FromResult(viewModel);
        }

        private static string BuildValidationMessage(string textFieldLabel, string actualMessage, string defaultMessage)
        {
            actualMessage = string.IsNullOrEmpty(actualMessage) ? defaultMessage : actualMessage;
            string result = string.Format(CultureInfo.InvariantCulture, actualMessage, textFieldLabel);

            return result;
        }

        private static string BuildValidationAttributes(DateTimeFieldEntity entity)
        {
            var attributes = new StringBuilder();

            if (entity.Required)
                attributes.Append(@"required=""required"" ");

            return attributes.ToString();
        }

        private static string GetDateInputType(DateFieldType fieldType)
        {
            switch (fieldType)
            {
                case DateFieldType.DateTime: return "datetime-local";
                case DateFieldType.DateOnly: return "date";
                case DateFieldType.TimeOnly: return "time";
                default: return "date";
            }
        }
    }
}
