using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Progress.Sitefinity.AspNetCore.Widgets.Models.LanguageSelector;

namespace Progress.Sitefinity.AspNetCore.Widgets.ViewComponents
{
    /// <summary>
    /// Language selector widget.
    /// </summary>
    [SitefinityWidget(Title="Language selector", HideEmptyVisual=true, Section = WidgetSection.NavigationAndSearch, Category = WidgetCategory.Content, IconName="language", NotPersonalizable=true)]
    [ViewComponent(Name = "SitefinityLanguageSelector")]
    public class LanguageSelectorViewComponent : ViewComponent
    {
        private ILanguageSelectorModel languageSelectorModel;

        /// <summary>Initializes a new instance of the <see cref="LanguageSelectorViewComponent" /> class.</summary>
        /// <param name="languageSelectorModel">The language selector model.</param>
        public LanguageSelectorViewComponent(ILanguageSelectorModel languageSelectorModel)
        {
            this.languageSelectorModel = languageSelectorModel;
        }

        /// <summary>
        /// Invokes the language selector widget creation.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public async Task<IViewComponentResult> InvokeAsync(IViewComponentContext<LanguageSelectorEntity> context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (!this.languageSelectorModel.IsPageRequest())
            {
                context.SetWarning("Language selector is visible when you are on a particular page.");
                return new ContentViewComponentResult(string.Empty);
            }

            var viewModel = await this.languageSelectorModel.InitializeViewModel(context.Entity);
            return this.View(viewModel.ViewName, viewModel);
        }
    }
}
