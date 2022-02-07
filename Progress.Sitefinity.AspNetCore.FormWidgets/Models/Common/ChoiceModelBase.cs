using System;
using System.Globalization;
using Newtonsoft.Json.Linq;
using Progress.Sitefinity.AspNetCore.FormWidgets.Entities.Common;
using Progress.Sitefinity.AspNetCore.FormWidgets.ViewModels.Common;

namespace Progress.Sitefinity.AspNetCore.FormWidgets.Models.Common
{
    /// <summary>
    /// Base model class logic for form choice fields.
    /// </summary>
    /// <typeparam name="TChoiceEntity">The entity class.</typeparam>
    public abstract class ChoiceModelBase<TChoiceEntity>
        where TChoiceEntity : ChoiceEntityBase
    {
        /// <summary>
        /// Initializes the view model.
        /// </summary>
        /// <typeparam name="T">The view model class type.</typeparam>
        /// <param name="entity">The choice entity.</param>
        /// <returns>The initialized view model.</returns>
        /// <exception cref="ArgumentNullException">Throws an exception if the entity param is null.</exception>
        public T InitializeViewModel<T>(TChoiceEntity entity)
            where T : ChoiceViewModelBase, new()
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var viewModel = new T();
            viewModel.Label = entity.Label;
            viewModel.InstructionalText = entity.InstructionalText;
            viewModel.Choices = entity.Choices;
            viewModel.Required = entity.Required;
            viewModel.FieldName = entity.SfFieldName;
            viewModel.CssClass = entity.CssClass;
            viewModel.ViolationRestrictionsMessages = JObject.FromObject(new
            {
                required = BuildValidationMessage(entity.Label, entity.RequiredErrorMessage, "{0} field is required"),
            }).ToString();

            return viewModel;
        }

        private static string BuildValidationMessage(string textFieldLabel, string actualMessage, string defaultMessage)
        {
            actualMessage = string.IsNullOrEmpty(actualMessage) ? defaultMessage : actualMessage;
            string result = string.Format(CultureInfo.InvariantCulture, actualMessage, textFieldLabel);

            return result;
        }
    }
}
