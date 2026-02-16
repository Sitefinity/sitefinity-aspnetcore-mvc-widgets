using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Newtonsoft.Json;
using Progress.Sitefinity.AspNetCore.Models;
using Progress.Sitefinity.AspNetCore.RestSdk;
using Progress.Sitefinity.AspNetCore.Web;
using Progress.Sitefinity.AspNetCore.Widgets.ViewComponents.Common;
using Progress.Sitefinity.RestSdk;
using Progress.Sitefinity.RestSdk.Client;
using Progress.Sitefinity.RestSdk.Clients.Pages.Dto;
using Progress.Sitefinity.RestSdk.Dto;
using Progress.Sitefinity.RestSdk.OData;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.IntentBox
{
    /// <summary>
    /// The model for the IntentBox widget.
    /// </summary>
    public class IntentBoxModel : IIntentBoxModel
    {
        private IODataRestClient restClient;
        private IRequestContext requestContext;
        private IStyleClassesProvider styles;
        private IHttpContextAccessor httpContextAccessor;
        private IRenderContext renderContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="IntentBoxModel"/> class.
        /// </summary>
        /// <param name="restClient">The rest client.</param>
        /// <param name="requestContext">The request context.</param>
        /// <param name="renderContext">The render context.</param>
        /// <param name="styles">The styles.</param>
        /// <param name="httpContextAccessor">The http context accessor.</param>
        public IntentBoxModel(IODataRestClient restClient, IRequestContext requestContext, IRenderContext renderContext, IStyleClassesProvider styles, IHttpContextAccessor httpContextAccessor)
        {
            this.restClient = restClient;
            this.requestContext = requestContext;
            this.renderContext = renderContext;
            this.styles = styles;
            this.httpContextAccessor = httpContextAccessor;
        }

        /// <inheritdoc/>
        public virtual async Task<IntentBoxViewModel> InitializeViewModel(IntentBoxEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var viewModel = new IntentBoxViewModel();

            if (entity.AfterIntentIsSubmitted == "redirect" && entity.TargetPage != null)
            {
                viewModel.TargetPageUrl = (await this.restClient.GetItem<PageNodeDto>(entity.TargetPage))?.ViewUrl;
            }

            viewModel.InputId = @"dg-input-box-" + Guid.NewGuid().ToString();
            viewModel.AfterIntentIsSubmitted = entity.AfterIntentIsSubmitted;

            // Get margins classes and set them to the view model
            var margins = this.styles.GetMarginsClasses(entity);
            viewModel.CssClass = (entity.CssClass + " " + margins).Trim();

            viewModel.Label = entity.Label;
            viewModel.PlaceholderText = entity.PlaceholderText;
            viewModel.SuggestionsLabel = entity.SuggestionsLabel;
            viewModel.SubmitButtonTooltip = entity.SubmitButtonTooltip;
            viewModel.Suggestions = new List<string>(entity.Suggestions ?? Array.Empty<string>());

            return viewModel;
        }
    }
}
