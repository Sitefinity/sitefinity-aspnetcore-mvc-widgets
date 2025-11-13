using System.Threading.Tasks;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.LanguageSelector
{
    /// <summary>
    /// Language Selector Model supported methods interface.
    /// </summary>
    public interface ILanguageSelectorModel
    {
        /// <summary>Initializes the view model.</summary>
        /// <param name="entity">The entity.</param>
        /// <returns>
        /// An instance of <see cref="LanguageSelectorViewModel" /> class.
        /// </returns>
        Task<LanguageSelectorViewModel> InitializeViewModel(LanguageSelectorEntity entity);

        /// <summary>
        /// Determines whether the current request is a page request.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if the current request is a page request; otherwise, <c>false</c>.</returns>
        bool IsPageRequest();
    }
}
