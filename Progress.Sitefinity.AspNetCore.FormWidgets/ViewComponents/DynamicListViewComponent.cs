using System;
using System.Threading.Tasks;
using AngleSharp.Dom;
using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.AspNetCore.FormWidgets.Entities.Dropdown;
using Progress.Sitefinity.AspNetCore.FormWidgets.Models.Dropdown;
using Progress.Sitefinity.AspNetCore.FormWidgets.Models.DynamicList;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Progress.Sitefinity.Renderer.Forms;

namespace Progress.Sitefinity.AspNetCore.FormWidgets.ViewComponents.DynamicList
{
    /// <summary>
    /// The view component for the dropdown field widget.
    /// </summary>
    [SitefinityFormWidget(FormFieldType.DynamicList, Title = "Dynamic list", Section = WidgetSection.Choices, IconName = "dropdown")]
    [ViewComponent(Name = "SitefinityDynamicList")]
    public class DynamicListViewComponent : ViewComponent
    {
        private IDynamicListModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicListViewComponent"/> class.
        /// </summary>
        /// <param name="model">The dynamic list field model.</param>
        public DynamicListViewComponent(IDynamicListModel model)
        {
            this.model = model;
        }

        /// <summary>
        /// Invokes the DynamicList widget creation.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>A <see cref="IViewComponentResult"/> representing the result of the operation.</returns>
        public async Task<IViewComponentResult> InvokeAsync(IViewComponentContext<DynamicListEntity> context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var viewModel = await this.model.InitializeViewModel(context.Entity);
            var hasContent = context.Entity.ListType == Selection.Content && context.Entity.SelectedContent != null && context.Entity.SelectedContent.Content != null && context.Entity.SelectedContent.Content[0].Type != null;
            var hasClassifications = context.Entity.ListType == Selection.Classification && context.Entity.ClassificationSettings != null && context.Entity.ClassificationSettings.SelectedTaxonomyName != null;

            if (!hasContent && !hasClassifications)
            {
                context.SetWarning("No list type have been selected.");
            }
            else if (viewModel.Choices.Count == 0)
            {
                context.SetWarning("Selected list is empty.");
            }

            return this.View(context.Entity.SfViewName, viewModel);
        }
    }
}
