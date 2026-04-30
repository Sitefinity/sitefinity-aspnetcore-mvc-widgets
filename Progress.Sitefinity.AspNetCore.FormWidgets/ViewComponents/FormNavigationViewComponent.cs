using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.AspNetCore.FormWidgets.Entities.FormNavigation;
using Progress.Sitefinity.AspNetCore.FormWidgets.Models.FormNavigation;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Progress.Sitefinity.Renderer.Forms;

namespace Progress.Sitefinity.AspNetCore.FormWidgets.ViewComponents
{
    /// <summary>
    /// The view component for the form navigation widget.
    /// </summary>
    [SitefinityFormWidget(FormFieldType.FormNavigation, Category = WidgetCategory.Layout, Title = "Form navigation", Order = 3, IconName = "navigation", HideEmptyVisual = true)]
    [ViewComponent(Name = "SitefinityFormNavigation")]
    public class FormNavigationViewComponent : ViewComponent
    {
        private IFormNavigationModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="FormNavigationViewComponent"/> class.
        /// </summary>
        /// <param name="formNavigationModel">The form navigation model.</param>
        public FormNavigationViewComponent(IFormNavigationModel formNavigationModel)
        {
            this.model = formNavigationModel;
        }

        /// <summary>
        /// Invokes the view.
        /// </summary>
        /// <param name="context">The composite view component context.</param>
        /// <returns>The view component result.</returns>
        public virtual async Task<IViewComponentResult> InvokeAsync(ICompositeViewComponentContext<FormNavigationEntity> context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var viewModel = await this.model.InitializeViewModel(context.Entity);

            return this.View(context.Entity.SfViewName, viewModel);
        }
    }
}
