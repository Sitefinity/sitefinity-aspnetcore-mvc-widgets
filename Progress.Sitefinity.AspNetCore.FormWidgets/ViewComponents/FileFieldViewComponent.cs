using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.AspNetCore.FormWidgets.Entities.FileField;
using Progress.Sitefinity.AspNetCore.FormWidgets.Models.FileField;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Progress.Sitefinity.AspNetCore.Web;
using Progress.Sitefinity.Renderer.Forms;

namespace Progress.Sitefinity.AspNetCore.FormWidgets.ViewComponents
{
    /// <summary>
    /// The view component for the text field widget.
    /// </summary>
    [SitefinityFormWidget(FormFieldType.File, Title = "File upload", Order = 1, Section = WidgetSection.Other, IconName = "file-upload")]
    [ViewComponent(Name = "SitefinityFileField")]
    public class FileFieldViewComponent : ViewComponent
    {
        private IFileFieldModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileFieldViewComponent"/> class.
        /// </summary>
        /// <param name="fileFieldModel">The text field model.</param>
        public FileFieldViewComponent(IFileFieldModel fileFieldModel)
        {
            this.model = fileFieldModel;
        }

        /// <summary>
        /// Invokes the TextField widget creation.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>A <see cref="IViewComponentResult"/> representing the result of the operation.</returns>
        public async Task<IViewComponentResult> InvokeAsync(IViewComponentContext<FileFieldEntity> context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var viewModel = await this.model.InitializeViewModel(context.Entity);

            return this.View(context.Entity.SfViewName, viewModel);
        }
    }
}
