using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.AspNetCore.FormWidgets.Entities.TextField;
using Progress.Sitefinity.AspNetCore.FormWidgets.Models.TextField;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Progress.Sitefinity.AspNetCore.Web;
using Progress.Sitefinity.Renderer.Forms;

namespace Progress.Sitefinity.AspNetCore.FormWidgets.ViewComponents
{
    /// <summary>
    /// The view component for the text field widget.
    /// </summary>
    [SitefinityFormWidget(FormFieldType.ShortText, Title = "Textbox", Order = 1, Section = WidgetSection.Basic)]
    [ViewComponent(Name = "SitefinityTextField")]
    public class TextFieldViewComponent : ViewComponent
    {
        private ITextFieldModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextFieldViewComponent"/> class.
        /// </summary>
        /// <param name="textFieldModel">The text field model.</param>
        public TextFieldViewComponent(ITextFieldModel textFieldModel)
        {
            this.model = textFieldModel;
        }

        /// <summary>
        /// Invokes the TextField widget creation.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>A <see cref="IViewComponentResult"/> representing the result of the operation.</returns>
        public async Task<IViewComponentResult> InvokeAsync(IViewComponentContext<TextFieldEntity> context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var viewModel = await this.model.InitializeViewModel(context.Entity);

            return this.View(viewModel);
        }
    }
}
