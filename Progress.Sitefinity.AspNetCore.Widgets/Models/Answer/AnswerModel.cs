using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Progress.Sitefinity.AspNetCore.Configuration;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Common;
using Progress.Sitefinity.AspNetCore.Widgets.Models.SitefinityAssistant;
using Progress.Sitefinity.AspNetCore.Widgets.ViewComponents.Common;
using Progress.Sitefinity.RestSdk.OData;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Answer
{
    /// <summary>
    /// The model for the Answer widget.
    /// </summary>
    public class AnswerModel : IAnswerModel
    {
        private readonly IStyleClassesProvider styles;
        private readonly ISitefinityAssistantClient assistantClient;
        private readonly IODataRestClient restClient;
        private readonly ISitefinityConfig config;

        /// <summary>
        /// Initializes a new instance of the <see cref="AnswerModel"/> class.
        /// </summary>
        /// <param name="styles">The style classes provider.</param>
        /// <param name="assistantClient">The Sitefinity Assistant client parameter.</param>
        /// <param name="restClient">The REST client for retrieving images.</param>
        /// <param name="config">The Sitefinity configurations.</param>
        public AnswerModel(IStyleClassesProvider styles, ISitefinityAssistantClient assistantClient, IODataRestClient restClient, ISitefinityConfig config)
        {
            this.styles = styles;
            this.assistantClient = assistantClient;
            this.restClient = restClient;
            this.config = config;
        }

        /// <inheritdoc/>
        public async virtual Task<AnswerViewModel> InitializeViewModel(AnswerEntity entity, HttpContext httpContext)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            httpContext.AddVaryByQueryParams(new[] { "searchQuery" });

            var versionInfo = await this.assistantClient.GetVersionInfoAsync(AssistantApiConstants.PARAG);
            var knowledgeBoxName = httpContext.Request.Query["knowledgeBoxName"];
            var searchConfiguarionName = httpContext.Request.Query["searchConfigurationName"];
            var searchQuery = httpContext.Request.Query["searchQuery"];

            var viewModel = new AnswerViewModel();
            viewModel.Title = !string.IsNullOrEmpty(entity.Title) ? entity.Title : "AI answer";
            viewModel.AssistantAvatarUrl = await this.restClient.GetSingleSelectedImageUrlAsync(entity.AssistantAvatar);
            viewModel.ShowSources = entity.ShowSources;
            viewModel.Notice = entity.ShowNotice ?
                (!string.IsNullOrEmpty(entity.Notice) ? entity.Notice : "AI answer may contain mistakes.") :
                null;
            viewModel.ShowFeedback = entity.ShowFeedback.HasValue ? entity.ShowFeedback.Value : true;
            viewModel.SearchedPhraseLabel = entity.ShowSearchedPhrase ?
                (!string.IsNullOrEmpty(entity.SearchedPhraseLabel) ? entity.SearchedPhraseLabel : "Answer for \"{0}\"") :
                null;
            viewModel.PositiveFeedbackTooltip = !string.IsNullOrEmpty(entity.PositiveFeedbackTooltip) ? entity.PositiveFeedbackTooltip : "Helpful";
            viewModel.NegativeFeedbackTooltip = !string.IsNullOrEmpty(entity.NegativeFeedbackTooltip) ? entity.NegativeFeedbackTooltip : "Not helpful";
            viewModel.ThankYouMessage = !string.IsNullOrEmpty(entity.ThankYouMessage) ? entity.ThankYouMessage : "Thank you for your feedback!";
            viewModel.ExpandAnswerLabel = !string.IsNullOrEmpty(entity.ExpandAnswerLabel) ? entity.ExpandAnswerLabel : "Show more";
            viewModel.CollapseAnswerLabel = !string.IsNullOrEmpty(entity.CollapseAnswerLabel) ? entity.CollapseAnswerLabel : "Show less";
            viewModel.LoadingLabel = !string.IsNullOrEmpty(entity.LoadingLabel) ? entity.LoadingLabel : "Putting together an answer";
            viewModel.ProductVersion = versionInfo?.ProductVersion;
            viewModel.ServiceUrl = $"/{this.config.WebServicePath}/AgenticRag/";
            viewModel.ConfigName = searchConfiguarionName;
            viewModel.KnowledgeBoxName = knowledgeBoxName;
            viewModel.SearchQuery = searchQuery;

            viewModel.Attributes = entity.Attributes;

            var margins = this.styles.GetMarginsClasses(entity);
            viewModel.CssClass = (entity.CssClass + " " + margins).Trim();

            return viewModel;
        }
    }
}
