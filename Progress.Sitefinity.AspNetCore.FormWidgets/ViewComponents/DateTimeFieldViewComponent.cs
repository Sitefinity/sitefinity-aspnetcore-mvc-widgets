using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.AspNetCore.FormWidgets.Entities.DateTimeField;
using Progress.Sitefinity.AspNetCore.FormWidgets.Models.DateField;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Progress.Sitefinity.Renderer.Forms;

namespace Progress.Sitefinity.AspNetCore.FormWidgets.ViewComponents
{
    /// <summary>
    /// The view component for the date and time field widget.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ViewComponent" />
    [SitefinityFormWidget(FormFieldType.DateTime, Title = "Date and time", Order = 2, Section = WidgetSection.Other, IconName = "date-time")]
    [ViewComponent(Name = "SitefinityDateTimeField")]
    public class DateTimeFieldViewComponent : ViewComponent
    {
        private IDateTimeFieldModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="DateTimeFieldViewComponent"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public DateTimeFieldViewComponent(IDateTimeFieldModel model)
        {
            this.model = model;
        }

        /// <summary>
        /// Invokes the Date and time field widget creation.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>A <see cref="IViewComponentResult"/> representing the result of the operation.</returns>
        public async Task<IViewComponentResult> InvokeAsync(IViewComponentContext<DateTimeFieldEntity> context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var viewModel = await this.model.InitializeViewModel(context.Entity);

            return this.View(context.Entity.SfViewName, viewModel);
        }
    }
}
