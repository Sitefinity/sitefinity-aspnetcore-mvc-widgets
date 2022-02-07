using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Progress.Sitefinity.AspNetCore.FormWidgets.Entities.FormSection;
using Progress.Sitefinity.AspNetCore.FormWidgets.ViewModels.FormSection;

namespace Progress.Sitefinity.AspNetCore.FormWidgets.Models.FormSection
{
    /// <inheritdoc/>
    public class FormSectionModel : IFormSectionModel
    {
        /// <inheritdoc/>
        public virtual Task<FormSectionViewModel> InitializeViewModel(FormSectionEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var viewModel = new FormSectionViewModel();
            viewModel.ColumnsCount = entity.ColumnsCount;
            viewModel.ColumnProportions = entity.ColumnProportionsInfo;
            viewModel.ColumnNames = PopulateColumnNames(entity);

            return Task.FromResult(viewModel);
        }

        private static IList<string> PopulateColumnNames(FormSectionEntity entity)
        {
            var columnNames = new List<string>();
            for (var i = 0; i < entity.ColumnsCount; i++)
            {
                columnNames.Add(FormSectionModel.ColumnNamePrefix + (i + 1));
            }

            return columnNames;
        }

        private const string ColumnNamePrefix = "Column";
    }
}
