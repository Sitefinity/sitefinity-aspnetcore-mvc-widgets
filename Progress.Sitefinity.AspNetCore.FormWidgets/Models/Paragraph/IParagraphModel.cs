using System.Threading.Tasks;
using Progress.Sitefinity.AspNetCore.FormWidgets.Entities.Common;
using Progress.Sitefinity.AspNetCore.FormWidgets.ViewComponents;
using Progress.Sitefinity.AspNetCore.FormWidgets.ViewModels.Common;

namespace Progress.Sitefinity.AspNetCore.FormWidgets.Models.Paragraph
{
    /// <summary>
    /// The model object that holds the logic for the <see cref="ParagraphViewComponent" /> widget.
    /// </summary>
    public interface IParagraphModel
    {
        /// <summary>
        /// Initializes the paragraph view model.
        /// </summary>
        /// <param name="entity">The text field entity.</param>
        /// <returns>The view model of the widget.</returns>
        Task<TextViewModelBase> InitializeViewModel(TextEntityBase entity);
    }
}
