using System.Linq;
using System.Threading.Tasks;
using Progress.Sitefinity.AspNetCore.FormWidgets.Entities.Dropdown;
using Progress.Sitefinity.AspNetCore.FormWidgets.Models.Common;
using Progress.Sitefinity.AspNetCore.FormWidgets.ViewComponents;
using Progress.Sitefinity.AspNetCore.FormWidgets.ViewComponents.Common;
using Progress.Sitefinity.AspNetCore.FormWidgets.ViewModels.Common;

namespace Progress.Sitefinity.AspNetCore.FormWidgets.Models.Dropdown
{
    /// <summary>
    /// The model object that holds the logic for the <see cref="DropdownViewComponent" /> widget.
    /// </summary>
    public class DropdownModel : ChoiceModelBase<DropdownEntity>, IDropdownModel
    {
        private FormWidgetsStyleGenerator formWidgetsStyleGenerator;

        /// <summary>
        /// Initializes a new instance of the <see cref="DropdownModel"/> class.
        /// </summary>
        /// <param name="formWidgetsStyleGenerator">The style generator for forms widgets.</param>
        public DropdownModel(FormWidgetsStyleGenerator formWidgetsStyleGenerator)
        {
            this.formWidgetsStyleGenerator = formWidgetsStyleGenerator;
        }

        /// <inheritdoc/>
        public Task<ChoiceViewModelBase> InitializeViewModel(DropdownEntity entity)
        {
            var viewModel = this.InitializeViewModel<ChoiceViewModelBase>(entity);
            if (entity.Sorting == DropdownSorting.Alphabetical)
            {
                viewModel.Choices = viewModel.Choices.OrderBy(x => x.Name).ToList();
            }

            viewModel.CssClass = $"{entity.CssClass} {this.formWidgetsStyleGenerator.GetFieldSizeCss(entity.FieldSize)}";

            return Task.FromResult(viewModel);
        }
    }
}
