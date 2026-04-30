using System;
using System.Threading.Tasks;
using Progress.Sitefinity.AspNetCore.FormWidgets.Entities.FormNavigation;
using Progress.Sitefinity.AspNetCore.FormWidgets.ViewModels.FormNavigation;

namespace Progress.Sitefinity.AspNetCore.FormWidgets.Models.FormNavigation
{
    /// <inheritdoc/>
    public class FormNavigationModel : IFormNavigationModel
    {
        /// <inheritdoc/>
        public virtual Task<FormNavigationViewModel> InitializeViewModel(FormNavigationEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var viewModel = new FormNavigationViewModel();
            viewModel.CssClass = entity.CssClass;
            viewModel.NavigationSteps = entity.NavigationSteps;

            return Task.FromResult(viewModel);
        }
    }
}
