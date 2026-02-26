using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Results;

namespace Progress.Sitefinity.AspNetCore.Widgets.ViewComponents
{
    /// <summary>
    /// The view component for the Results widget.
    /// </summary>
    [SitefinityWidget(Title = "AI results", Order = 2, Section = WidgetSection.AISearch, IconName = "ai-search-sparkle")]
    [ViewComponent(Name = "SitefinityResults")]
    public class ResultsViewComponent : ViewComponent
    {
        private readonly IResultsModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResultsViewComponent"/> class.
        /// </summary>
        /// <param name="resultsModel">The Results model.</param>
        public ResultsViewComponent(IResultsModel resultsModel)
        {
            this.model = resultsModel;
        }

        /// <summary>
        /// Invokes the Results widget creation.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public virtual async Task<IViewComponentResult> InvokeAsync(IViewComponentContext<ResultsEntity> context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var viewModel = await this.model.InitializeViewModel(context.Entity, this.HttpContext);

            return this.View(context.Entity.SfViewName, viewModel);
        }
    }
}
