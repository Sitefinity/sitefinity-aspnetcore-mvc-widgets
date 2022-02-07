using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Form;
using Progress.Sitefinity.Renderer.Designers.Attributes;

namespace Progress.Sitefinity.AspNetCore.Widgets.ViewComponents
{
    /// <summary>
    /// The view component for the Form widget.
    /// </summary>
    [SitefinityWidget(Title = Title, Section = WidgetSection.Basic, Order = 5, EmptyIconText = "Select a form", EmptyIcon = "plus-circle")]
    [InitialValue("ContentViewDisplayMode", "Detail")]
    [ViewComponent(Name = "SitefinityForm")]
    public class FormViewComponent : ViewComponent
    {
        /// <summary>
        /// The default title of the <see cref="FormViewComponent" /> - Form.
        /// </summary>
        public const string Title = "Form";

        private IFormModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="FormViewComponent"/> class.
        /// </summary>
        /// <param name="model">The form model.</param>
        public FormViewComponent(IFormModel model)
        {
            this.model = model;
        }

        /// <summary>
        /// Invokes the Form widget creation.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>A <see cref="IViewComponentResult"/> representing the result of the operation.</returns>
        public virtual async Task<IViewComponentResult> InvokeAsync(IViewComponentContext<FormEntity> context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var viewModel = await this.model.InitializeViewModel(context.Entity, this.HttpContext.Request.Query);

            if (!string.IsNullOrEmpty(viewModel.Warning))
            {
                context.SetWarning(viewModel.Warning);
            }

            return this.View(viewModel);
        }
    }
}
