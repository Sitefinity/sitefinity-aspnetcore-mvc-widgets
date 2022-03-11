using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.AspNetCore.FormWidgets.Entities.Common;
using Progress.Sitefinity.AspNetCore.FormWidgets.Models.Paragraph;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Progress.Sitefinity.AspNetCore.Web;
using Progress.Sitefinity.Renderer.Forms;

namespace Progress.Sitefinity.AspNetCore.FormWidgets.ViewComponents
{
    /// <summary>
    /// The view component for the text field widget.
    /// </summary>
    [SitefinityFormWidget(FormFieldType.Paragraph, Title = "Paragraph", Order = 2, Section = WidgetSection.Basic)]
    [ViewComponent(Name = "SitefinityParagraph")]
    public class ParagraphViewComponent : ViewComponent
    {
        private IParagraphModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="ParagraphViewComponent"/> class.
        /// </summary>
        /// <param name="paragraphModel">The paragraph model.</param>
        public ParagraphViewComponent(IParagraphModel paragraphModel)
        {
            this.model = paragraphModel;
        }

        /// <summary>
        /// Invokes the Paragraph widget creation.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>A <see cref="IViewComponentResult"/> representing the result of the operation.</returns>
        public async Task<IViewComponentResult> InvokeAsync(IViewComponentContext<TextEntityBase> context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var viewModel = await this.model.InitializeViewModel(context.Entity);

            return this.View(context.Entity.SfViewName, viewModel);
        }
    }
}
