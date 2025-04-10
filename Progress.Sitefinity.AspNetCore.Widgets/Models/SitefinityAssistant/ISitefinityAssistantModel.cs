using System.Threading.Tasks;
using Progress.Sitefinity.AspNetCore.ViewComponents;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.SitefinityAssistant
{
    /// <summary>
    /// The ISitefinityAssistantModel interface.
    /// </summary>
    public interface ISitefinityAssistantModel
    {
        /// <summary>
        /// Gets the ViewModel.
        /// </summary>
        /// <param name="context">The entity creation context parameter.</param>
        /// <returns>The SitefinityAssistantViewModel.</returns>
        Task<SitefinityAssistantViewModel> GetViewModel(IViewComponentContext<SitefinityAssistantEntity> context);
    }
}
