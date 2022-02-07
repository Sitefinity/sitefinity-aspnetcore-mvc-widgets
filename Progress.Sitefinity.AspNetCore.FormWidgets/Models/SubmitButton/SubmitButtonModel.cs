using System;
using System.Threading.Tasks;
using Progress.Sitefinity.AspNetCore.FormWidgets.Entities.SubmitButton;
using Progress.Sitefinity.AspNetCore.FormWidgets.ViewModels.SubmitButton;

namespace Progress.Sitefinity.AspNetCore.FormWidgets.Models.SubmitButton
{
    /// <inheritdoc/>
    public class SubmitButtonModel : ISubmitButtonModel
    {
        /// <inheritdoc/>
        Task<SubmitButtonViewModel> ISubmitButtonModel.InitializeViewModel(SubmitButtonEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var viewModel = new SubmitButtonViewModel();
            viewModel.CssClass = entity.CssClass;
            viewModel.Label = string.IsNullOrEmpty(entity.Label) ? "Submit" : entity.Label;

            return Task.FromResult(viewModel);
        }
    }
}
