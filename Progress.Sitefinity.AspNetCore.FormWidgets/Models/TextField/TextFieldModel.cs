using System;
using System.Globalization;
using System.Threading.Tasks;
using Progress.Sitefinity.AspNetCore.FormWidgets.Entities.TextField;
using Progress.Sitefinity.AspNetCore.FormWidgets.Models.Common;
using Progress.Sitefinity.AspNetCore.FormWidgets.ViewComponents;
using Progress.Sitefinity.AspNetCore.FormWidgets.ViewComponents.Common;
using Progress.Sitefinity.AspNetCore.FormWidgets.ViewModels.TextField;
using Progress.Sitefinity.AspNetCore.Web;
using Progress.Sitefinity.Renderer.Contracts.Forms;

namespace Progress.Sitefinity.AspNetCore.FormWidgets.Models.TextField
{
    /// <summary>
    /// The model object that holds the logic for the <see cref="TextFieldViewComponent" /> widget.
    /// </summary>
    public class TextFieldModel : TextModelBase<TextFieldEntity>, ITextFieldModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TextFieldModel"/> class.
        /// </summary>
        /// <param name="formWidgetsStyleGenerator">The style generator for forms widgets.</param>
        public TextFieldModel(FormWidgetsStyleGenerator formWidgetsStyleGenerator)
            : base(formWidgetsStyleGenerator)
        {
        }

        /// <inheritdoc/>
        public Task<TextFieldViewModel> InitializeViewModel(TextFieldEntity entity)
        {
            var model = this.InitializeViewModel<TextFieldViewModel>(entity);
#pragma warning disable CA1308
            var inputType = Enum.GetName(typeof(TextType), entity.InputType).ToLowerInvariant();
            model.InputType = HandleInputType(inputType);
#pragma warning restore CA1308

            return Task.FromResult<TextFieldViewModel>(model);
        }

        /// <inheritdoc/>
        protected override string GetRegularExpression(TextFieldEntity textEntity)
        {
            if (textEntity == null)
                throw new ArgumentNullException(nameof(textEntity));

            return textEntity.RegularExpression;
        }

        /// <inheritdoc/>
        protected override string GetRegularExpressionViolationMessage(TextFieldEntity textEntity)
        {
            if (textEntity == null)
                throw new ArgumentNullException(nameof(textEntity));

            return textEntity.RegularExpressionViolationMessage;
        }

        private static string HandleInputType(string inputType)
        {
            if (inputType.ToUpperInvariant() == nameof(TextType.Phone).ToUpperInvariant())
                return "tel";
            return inputType;
        }
    }
}
