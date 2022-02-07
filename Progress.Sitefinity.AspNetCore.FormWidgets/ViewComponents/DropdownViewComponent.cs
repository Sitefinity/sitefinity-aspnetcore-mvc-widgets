using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.AspNetCore.FormWidgets.Entities.Dropdown;
using Progress.Sitefinity.AspNetCore.FormWidgets.Models.Dropdown;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Progress.Sitefinity.Renderer.Forms;

namespace Progress.Sitefinity.AspNetCore.FormWidgets.ViewComponents
{
    /// <summary>
    /// The view component for the dropdown field widget.
    /// </summary>
    [SitefinityFormWidget(FormFieldType.Dropdown, Title = "Dropdown", Order = 3, Section = WidgetSection.Choices)]
    [ViewComponent(Name = "SitefinityDropdown")]
    public class DropdownViewComponent : ViewComponent
    {
        private IDropdownModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="DropdownViewComponent"/> class.
        /// </summary>
        /// <param name="model">The checkboxes field model.</param>
        public DropdownViewComponent(IDropdownModel model)
        {
            this.model = model;
        }

        /// <summary>
        /// Invokes the Checkboxes widget creation.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>A <see cref="IViewComponentResult"/> representing the result of the operation.</returns>
        public async Task<IViewComponentResult> InvokeAsync(IViewComponentContext<DropdownEntity> context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var viewModel = await this.model.InitializeViewModel(context.Entity);

            return this.View(viewModel);
        }
    }
}
