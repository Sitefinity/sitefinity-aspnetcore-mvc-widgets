using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.AspNetCore.FormWidgets.Entities.MultipleChoice;
using Progress.Sitefinity.AspNetCore.FormWidgets.Models.MultipleChoice;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Progress.Sitefinity.Renderer.Forms;

namespace Progress.Sitefinity.AspNetCore.FormWidgets.ViewComponents
{
    /// <summary>
    /// The view component for the multiple choice field widget.
    /// </summary>
    [SitefinityFormWidget(FormFieldType.MultipleChoice, Title = "Multiple choice", Order = 1, Section = WidgetSection.Choices)]
    [ViewComponent(Name = "SitefinityMultipleChoice")]
    public class MultipleChoiceViewComponent : ViewComponent
    {
        private IMultipleChoiceModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="MultipleChoiceViewComponent"/> class.
        /// </summary>
        /// <param name="model">The multiple choice field model.</param>
        public MultipleChoiceViewComponent(IMultipleChoiceModel model)
        {
            this.model = model;
        }

        /// <summary>
        /// Invokes the MultipleChoice widget creation.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>A <see cref="IViewComponentResult"/> representing the result of the operation.</returns>
        public async Task<IViewComponentResult> InvokeAsync(IViewComponentContext<MultipleChoiceEntity> context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var viewModel = await this.model.InitializeViewModel(context.Entity);

            return this.View(viewModel);
        }
    }
}
