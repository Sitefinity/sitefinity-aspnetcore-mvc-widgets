using System.Threading.Tasks;
using Progress.Sitefinity.AspNetCore.FormWidgets.Entities.MultipleChoice;
using Progress.Sitefinity.AspNetCore.FormWidgets.Models.Common;
using Progress.Sitefinity.AspNetCore.FormWidgets.ViewComponents;
using Progress.Sitefinity.AspNetCore.FormWidgets.ViewModels.MultipleChoice;

namespace Progress.Sitefinity.AspNetCore.FormWidgets.Models.MultipleChoice
{
    /// <summary>
    /// The model object that holds the logic for the <see cref="MultipleChoiceViewComponent" /> widget.
    /// </summary>
    public class MultipleChoiceModel : ChoiceModelBase<MultipleChoiceEntity>, IMultipleChoiceModel
    {
        /// <inheritdoc/>
        public virtual Task<MultipleChoiceViewModel> InitializeViewModel(MultipleChoiceEntity entity)
        {
            var viewModel = this.InitializeViewModel<MultipleChoiceViewModel>(entity);
            viewModel.HasAdditionalChoice = entity.HasAdditionalChoice;
            viewModel.ColumnsNumber = entity.ColumnsNumber;
            return Task.FromResult(viewModel);
        }
    }
}
