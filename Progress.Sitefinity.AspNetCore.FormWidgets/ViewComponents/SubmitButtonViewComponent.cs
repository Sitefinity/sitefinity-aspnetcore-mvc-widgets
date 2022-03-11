using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.AspNetCore.FormWidgets.Entities.SubmitButton;
using Progress.Sitefinity.AspNetCore.FormWidgets.Models.SubmitButton;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Progress.Sitefinity.Renderer.Forms;

namespace Progress.Sitefinity.AspNetCore.FormWidgets.ViewComponents
{
    /// <summary>
    /// The view component for the text field widget.
    /// </summary>
    [SitefinityFormWidget(FormFieldType.SubmitButton, Title = "Submit button", Order = 3, Section = WidgetSection.Basic)]
    [ViewComponent(Name = "SitefinitySubmitButton")]
    public class SubmitButtonViewComponent : ViewComponent
    {
        private ISubmitButtonModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="SubmitButtonViewComponent"/> class.
        /// </summary>
        /// <param name="textFieldModel">The text field model.</param>
        public SubmitButtonViewComponent(ISubmitButtonModel textFieldModel)
        {
            this.model = textFieldModel;
        }

        /// <summary>
        /// Invokes the TextField widget creation.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>A <see cref="IViewComponentResult"/> representing the result of the operation.</returns>
        public async Task<IViewComponentResult> InvokeAsync(IViewComponentContext<SubmitButtonEntity> context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var viewModel = await this.model.InitializeViewModel(context.Entity);

            return this.View(context.Entity.SfViewName, viewModel);
        }
    }
}
