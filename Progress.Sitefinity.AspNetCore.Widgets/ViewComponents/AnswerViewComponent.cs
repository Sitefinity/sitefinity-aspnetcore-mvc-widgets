using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Answer;

namespace Progress.Sitefinity.AspNetCore.Widgets.ViewComponents
{
    /// <summary>
    /// The view component for the Answer widget.
    /// </summary>
    [SitefinityWidget(Title = "AI answer", Order = 1, Section = WidgetSection.AISearch, IconName = "ai-search-sparkle")]
    [ViewComponent(Name = "SitefinityAnswer")]
    public class AnswerViewComponent : ViewComponent
    {
        private readonly IAnswerModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="AnswerViewComponent"/> class.
        /// </summary>
        /// <param name="answerModel">The Answer model.</param>
        public AnswerViewComponent(IAnswerModel answerModel)
        {
            this.model = answerModel;
        }

        /// <summary>
        /// Invokes the Answer widget creation.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public virtual async Task<IViewComponentResult> InvokeAsync(IViewComponentContext<AnswerEntity> context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var viewModel = await this.model.InitializeViewModel(context.Entity, this.HttpContext);

            return this.View(context.Entity.SfViewName, viewModel);
        }
    }
}
