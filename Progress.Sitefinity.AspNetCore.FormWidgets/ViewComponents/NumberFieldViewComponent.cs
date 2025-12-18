using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.AspNetCore.FormWidgets.Entities.NumberField;
using Progress.Sitefinity.AspNetCore.FormWidgets.Models.NumberField;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Progress.Sitefinity.Renderer.Forms;

namespace Progress.Sitefinity.AspNetCore.FormWidgets.ViewComponents
{
    /// <summary>
    /// The view component for the number field widget.
    /// </summary>
    [SitefinityFormWidget(FormFieldType.Number, Title = "Number", Order = 3, Section = WidgetSection.Basic, IconName = "number")]
    [ViewComponent(Name = "SitefinityNumberField")]
    public class NumberFieldViewComponent : ViewComponent
    {
        private INumberFieldModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="NumberFieldViewComponent"/> class.
        /// </summary>
        /// <param name="textFieldModel">The text field model.</param>
        public NumberFieldViewComponent(INumberFieldModel textFieldModel)
        {
            this.model = textFieldModel;
        }

        /// <summary>
        /// Invokes the Number field widget creation.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>A <see cref="IViewComponentResult"/> representing the result of the operation.</returns>
        public async Task<IViewComponentResult> InvokeAsync(IViewComponentContext<NumberFieldEntity> context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var viewModel = await this.model.InitializeViewModel(context.Entity);

            return this.View(context.Entity.SfViewName, viewModel);
        }
    }
}
