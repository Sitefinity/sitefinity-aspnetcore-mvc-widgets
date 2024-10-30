using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.AspNetCore.FormWidgets.Entities.FormSection;
using Progress.Sitefinity.AspNetCore.FormWidgets.Models.FormSection;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Progress.Sitefinity.Renderer.Forms;

namespace Progress.Sitefinity.AspNetCore.FormWidgets.ViewComponents
{
    /// <summary>
    /// The view component for the text field widget.
    /// </summary>
    [SitefinityFormWidget(FormFieldType.FormSection, Category = WidgetCategory.Layout, Title = "Section", Order = 1, IconName = "section")]
    [ViewComponent(Name = "SitefinityFormSection")]
    public class FormSectionViewComponent : ViewComponent
    {
        private IFormSectionModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="FormSectionViewComponent"/> class.
        /// </summary>
        /// <param name="sectionModel">The section model.</param>
        public FormSectionViewComponent(IFormSectionModel sectionModel)
        {
            this.model = sectionModel;
        }

        /// <summary>
        /// Invokes the view.
        /// </summary>
        /// <param name="context">The composite view component context.</param>
        /// <returns>The view component result.</returns>
        public virtual async Task<IViewComponentResult> InvokeAsync(ICompositeViewComponentContext<FormSectionEntity> context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var viewModel = await this.model.InitializeViewModel(context.Entity);
            viewModel.Context = context;

            return this.View(viewModel);
        }
    }
}
