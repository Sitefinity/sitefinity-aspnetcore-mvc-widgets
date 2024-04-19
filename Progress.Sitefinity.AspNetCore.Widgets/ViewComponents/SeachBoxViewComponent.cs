using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Search;

namespace Progress.Sitefinity.AspNetCore.Widgets.ViewComponents
{
    /// <summary>
    /// The view component for the seach box widget.
    /// </summary>
    [SitefinityWidget(Title = "Search box", Order = 2, Section = WidgetSection.NavigationAndSearch, EmptyIconText = "Set where to search", EmptyIconAction = EmptyLinkAction.Edit, EmptyIcon = "search", Category = WidgetCategory.Content)]
    [ViewComponent(Name = "SitefinitySearchBox")]
    public class SeachBoxViewComponent : ViewComponent
    {
        private ISearchBoxModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="SeachBoxViewComponent"/> class.
        /// </summary>
        /// <param name="model">The <see cref="ISearchBoxModel"/> model.</param>
        public SeachBoxViewComponent(ISearchBoxModel model)
        {
            this.model = model;
        }

        /// <summary>
        /// Invokes the search box widget creation.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public virtual async Task<IViewComponentResult> InvokeAsync(IViewComponentContext<SearchBoxEntity> context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var viewModel = await this.model.InitializeViewModel(context.Entity);
            return this.View(context.Entity.SfViewName, viewModel);
        }
    }
}
