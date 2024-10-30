using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Section;
using Progress.Sitefinity.Renderer.Designers.Attributes;

namespace Progress.Sitefinity.AspNetCore.Widgets.ViewComponents
{
    /// <summary>
    /// The view compnent for Section widget.
    /// </summary>
    [SitefinityWidget(Category = WidgetCategory.Layout, Title = "Section", Section = WidgetSection.EmptySection, IconName = "section", NotPersonalizable = true)]
    [InitialValue("SectionPadding", "{\"Top\":\"S\",\"Bottom\":\"S\"}")]
    [ViewComponent(Name = "SitefinitySection")]
    public class SectionViewComponent : ViewComponent
    {
        private ISectionModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="SectionViewComponent"/> class.
        /// </summary>
        /// <param name="sectionModel">The section model.</param>
        public SectionViewComponent(ISectionModel sectionModel)
        {
            this.model = sectionModel;
        }

        /// <summary>
        /// Invokes the view.
        /// </summary>
        /// <param name="context">The composite view component context.</param>
        /// <returns>The view component result.</returns>
        public virtual async Task<IViewComponentResult> InvokeAsync(ICompositeViewComponentContext<SectionEntity> context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var viewModel = await this.model.InitializeViewModel(context.Entity);
            viewModel.Context = context;

            return this.View("Default", viewModel);
        }
    }
}
