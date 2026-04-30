using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.AspNetCore.FormWidgets.Entities.FormPage;
using Progress.Sitefinity.AspNetCore.FormWidgets.Models.FormPage;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Progress.Sitefinity.Renderer.Forms;

namespace Progress.Sitefinity.AspNetCore.FormWidgets.ViewComponents
{
    /// <summary>
    /// The view component for the form page widget.
    /// </summary>
    [SitefinityFormWidget(FormFieldType.FormPage, Category = WidgetCategory.Layout, Title = "Form page", Order = 2, IconName = "content-section")]
    [ViewComponent(Name = "SitefinityFormPage")]
    public class FormPageViewComponent : ViewComponent
    {
        private IFormPageModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="FormPageViewComponent"/> class.
        /// </summary>
        /// <param name="formPageModel">The form page model.</param>
        public FormPageViewComponent(IFormPageModel formPageModel)
        {
            this.model = formPageModel;
        }

        /// <summary>
        /// Invokes the view.
        /// </summary>
        /// <param name="context">The composite view component context.</param>
        /// <returns>The view component result.</returns>
        public virtual async Task<IViewComponentResult> InvokeAsync(ICompositeViewComponentContext<FormPageEntity> context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var viewModel = await this.model.InitializeViewModel(context.Entity);

            viewModel.Context = context;

            return this.View("Default", viewModel);
        }
    }
}
