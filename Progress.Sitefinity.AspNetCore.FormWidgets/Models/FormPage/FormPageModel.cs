using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Progress.Sitefinity.AspNetCore.FormWidgets.Entities.FormPage;
using Progress.Sitefinity.AspNetCore.FormWidgets.ViewModels.FormPage;
using Progress.Sitefinity.AspNetCore.Models;

namespace Progress.Sitefinity.AspNetCore.FormWidgets.Models.FormPage
{
    /// <inheritdoc/>
    public class FormPageModel : IFormPageModel
    {
        /// <inheritdoc/>
        public virtual Task<FormPageViewModel> InitializeViewModel(FormPageEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var viewModel = new FormPageViewModel();
            viewModel.CssClass = entity.CssClass;
            viewModel.PageLabel = entity.PageLabel;
            viewModel.ButtonLabel = entity.ButtonLabel;
            viewModel.AllowStepBackward = entity.AllowStepBackward;
            viewModel.BackLinkLabel = entity.BackLinkLabel;

            return Task.FromResult(viewModel);
        }
    }
}
