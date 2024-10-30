using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Classification;

namespace Progress.Sitefinity.AspNetCore.Widgets.ViewComponents
{
    /// <summary>
    /// The view component for the classification widget.
    /// </summary>
    [SitefinityWidget(Title = ClassificationViewComponent.Title, Order = 5, Section = WidgetSection.NavigationAndSearch, EmptyIconText = "Select classification", EmptyIconAction = EmptyLinkAction.Edit, EmptyIcon = "tag", Category = WidgetCategory.Content, IconName = "classification", NotPersonalizable = true)]
    [ViewComponent(Name = "SitefinityClassification")]
    public class ClassificationViewComponent : ViewComponent
    {
        /// <summary>
        /// The default title of the <see cref="ClassificationViewComponent" /> - Classification.
        /// </summary>
        public const string Title = "Classification";

        private IClassificationModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClassificationViewComponent"/> class.
        /// </summary>
        /// <param name="model">The <see cref="IClassificationModel"/> model.</param>
        public ClassificationViewComponent(IClassificationModel model)
        {
            this.model = model;
        }

        /// <summary>
        /// Invokes the search box widget creation.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public virtual async Task<IViewComponentResult> InvokeAsync(IViewComponentContext<ClassificationEntity> context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (!string.IsNullOrEmpty(context.Entity?.ClassificationSettings?.SelectedTaxonomyId))
            {
                context.SetTitle($"{ClassificationViewComponent.Title} - {context.Entity.ClassificationSettings.SelectedTaxonomyTitle}");
            }

            var viewModel = await this.model.InitializeViewModel(context.Entity);

            if (viewModel.Taxons != null && !viewModel.Taxons.Any() && !string.IsNullOrEmpty(context.Entity?.ClassificationSettings?.SelectedTaxonomyId))
            {
                context.SetHideEmptyVisual(true);
            }

            return this.View(context.Entity.SfViewName, viewModel);
        }
    }
}
