using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Results.Dto;
using Progress.Sitefinity.AspNetCore.Widgets.Models.SitefinityAssistant;
using Progress.Sitefinity.AspNetCore.Widgets.ViewComponents.Common;
using Progress.Sitefinity.RestSdk;
using Progress.Sitefinity.RestSdk.OData;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Results
{
    /// <summary>
    /// The model for the Results widget.
    /// </summary>
    public class ResultsModel : IResultsModel
    {
        private const int DefaultLimitItemsCount = 20;
        private readonly IStyleClassesProvider styles;
        private readonly ISitefinityAssistantClient assistantClient;
        private readonly IODataRestClient restClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResultsModel"/> class.
        /// </summary>
        /// <param name="styles">The style classes provider.</param>
        /// <param name="restClient">The OData REST client.</param>
        /// <param name="assistantClient">The Sitefinity Assistant client parameter.</param>
        public ResultsModel(IStyleClassesProvider styles, IODataRestClient restClient, ISitefinityAssistantClient assistantClient)
        {
            this.styles = styles;
            this.restClient = restClient;
            this.assistantClient = assistantClient;
        }

        /// <inheritdoc/>
        public async virtual Task<ResultsViewModel> InitializeViewModel(ResultsEntity entity, HttpContext httpContext)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            httpContext.AddVaryByQueryParams(["searchQuery", "knowledgeBoxName", "searchConfigurationName"]);

            var searchQuery = httpContext.Request.Query["searchQuery"].ToString();
            var knowledgeBoxName = httpContext.Request.Query["knowledgeBoxName"].ToString();
            var searchConfigurationName = httpContext.Request.Query["searchConfigurationName"].ToString();

            var noResultsHeader = !string.IsNullOrEmpty(entity.NoResultsHeader) ? entity.NoResultsHeader : "No results for \"{0}\"";
            var searchResultsHeader = !string.IsNullOrEmpty(entity.SearchResultsHeader) ? entity.SearchResultsHeader : "Results for \"{0}\"";
            var resultsNumberLabel = !string.IsNullOrEmpty(entity.ResultsNumberLabel) ? entity.ResultsNumberLabel : "results";
            var versionInfo = await this.assistantClient.GetVersionInfoAsync(AssistantApiConstants.PARAG);

            var viewModel = new ResultsViewModel
            {
                ResultsHeader = string.Format(CultureInfo.InvariantCulture, noResultsHeader, searchQuery),
                ResultsNumberLabel = resultsNumberLabel,
                SearchResults = null,
                PageSize = entity.PageSize ?? DefaultLimitItemsCount,
                Attributes = entity.Attributes
            };

            if (!string.IsNullOrEmpty(searchQuery) && !string.IsNullOrEmpty(knowledgeBoxName))
            {
                viewModel.SearchResults = new List<ResultItemViewModel>();
                var response = await this.PerformSearchAsync(searchQuery, knowledgeBoxName, searchConfigurationName);

                if (response?.Resources != null && response.Resources.Count > 0)
                {
                    foreach (var resourceEntry in response.Resources)
                    {
                        var resource = resourceEntry.Value;

                        if (resource.Origin != null)
                        {
                            var result = new ResultItemViewModel
                            {
                                Title = resource.Title
                            };

                            result.Link = resource.Origin.Url;

                            var allParagraphs = new List<ParagraphDto>();
                            if (resource.Fields != null)
                            {
                                foreach (var fieldEntry in resource.Fields)
                                {
                                    if (fieldEntry.Value?.Paragraphs != null)
                                    {
                                        foreach (var paraEntry in fieldEntry.Value.Paragraphs)
                                        {
                                            allParagraphs.Add(paraEntry.Value);
                                        }
                                    }
                                }
                            }

                            allParagraphs.Sort((a, b) => a.Order.CompareTo(b.Order));
                            result.Order = allParagraphs.FirstOrDefault()?.Order ?? 0;
                            viewModel.SearchResults.Add(result);
                        }
                    }

                    viewModel.SearchResults.Sort((a, b) => a.Order.CompareTo(b.Order));

                    if (viewModel.SearchResults.Count > 0)
                    {
                        viewModel.ResultsHeader = string.Format(CultureInfo.InvariantCulture, searchResultsHeader, searchQuery);
                    }
                }
            }

            var margins = this.styles.GetMarginsClasses(entity);
            viewModel.CssClass = (entity.CssClass + " " + margins).Trim();

            return viewModel;
        }

        private async Task<FindResponse> PerformSearchAsync(string searchQuery, string knowledgeBoxName, string searchConfigurationName)
        {
            var findRequest = new FindRequest
            {
                KnowledgeBoxName = knowledgeBoxName,
                Query = searchQuery,
                Take = 200,
            };

            if (!string.IsNullOrEmpty(searchConfigurationName))
            {
                findRequest.ConfigurationName = searchConfigurationName;
            }

            var args = new BoundActionArgs
            {
                Name = "AgenticRag/find",
                Data = findRequest,
            };

            var response = await this.restClient.ExecuteUnboundAction<FindResponse>(args);

            return response;
        }
    }
}
