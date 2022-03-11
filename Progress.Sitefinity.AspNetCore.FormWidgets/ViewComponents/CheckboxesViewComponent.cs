using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.AspNetCore.FormWidgets.Entities.Checkboxes;
using Progress.Sitefinity.AspNetCore.FormWidgets.Models.MultipleChoice;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Progress.Sitefinity.Renderer.Forms;

namespace Progress.Sitefinity.AspNetCore.FormWidgets.ViewComponents
{
    /// <summary>
    /// The view component for the checkboxes field widget.
    /// </summary>
    [SitefinityFormWidget(FormFieldType.Checkboxes, Title = "Checkboxes", Order = 2, Section = WidgetSection.Choices)]
    [ViewComponent(Name = "SitefinityCheckboxes")]
    public class CheckboxesViewComponent : ViewComponent
    {
        private IMultipleChoiceModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckboxesViewComponent"/> class.
        /// </summary>
        /// <param name="model">The checkboxes field model.</param>
        public CheckboxesViewComponent(IMultipleChoiceModel model)
        {
            this.model = model;
        }

        /// <summary>
        /// Invokes the Checkboxes widget creation.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>A <see cref="IViewComponentResult"/> representing the result of the operation.</returns>
        public async Task<IViewComponentResult> InvokeAsync(IViewComponentContext<CheckboxesEntity> context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var viewModel = await this.model.InitializeViewModel(context.Entity);

            return this.View(context.Entity.SfViewName, viewModel);
        }
    }
}
