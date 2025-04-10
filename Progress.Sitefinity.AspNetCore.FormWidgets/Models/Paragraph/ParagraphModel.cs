using System.Threading.Tasks;
using Progress.Sitefinity.AspNetCore.FormWidgets.Entities.Common;
using Progress.Sitefinity.AspNetCore.FormWidgets.Models.Common;
using Progress.Sitefinity.AspNetCore.FormWidgets.ViewComponents;
using Progress.Sitefinity.AspNetCore.FormWidgets.ViewComponents.Common;
using Progress.Sitefinity.AspNetCore.FormWidgets.ViewModels.Common;

namespace Progress.Sitefinity.AspNetCore.FormWidgets.Models.Paragraph
{
    /// <summary>
    /// The model object that holds the logic for the <see cref="ParagraphViewComponent" /> widget.
    /// </summary>
    public class ParagraphModel : TextModelBase<TextEntityBase>, IParagraphModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ParagraphModel"/> class.
        /// </summary>
        /// <param name="formWidgetsStyleGenerator">The style generator for forms widgets.</param>
        public ParagraphModel(FormWidgetsStyleGenerator formWidgetsStyleGenerator)
            : base(formWidgetsStyleGenerator)
        {
        }

        /// <inheritdoc/>
        public virtual Task<TextViewModelBase> InitializeViewModel(TextEntityBase entity)
        {
            var model = this.InitializeViewModel<TextViewModelBase>(entity);
            return Task.FromResult<TextViewModelBase>(model);
        }

        /// <inheritdoc/>
        protected override string GetRegularExpression(TextEntityBase textEntity)
        {
            return string.Empty;
        }

        /// <inheritdoc/>
        protected override string GetRegularExpressionViolationMessage(TextEntityBase textEntity)
        {
            return string.Empty;
        }
    }
}
