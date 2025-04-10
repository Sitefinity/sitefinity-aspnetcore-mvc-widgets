using System.Threading.Tasks;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Progress.Sitefinity.AspNetCore.Web;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Common;
using Progress.Sitefinity.RestSdk;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.SitefinityAssistant
{
    /// <summary>
    /// The SitefinityAssistantModel class.
    /// </summary>
    internal class SitefinityAssistantModel : ISitefinityAssistantModel
    {
        private readonly IRestClient restClient;
        private readonly ISitefinityAssistantClient assistantClient;
        private readonly IRequestContext requestContext;
        private readonly IAntiforgery antiforgery;
        private readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="SitefinityAssistantModel"/> class.
        /// </summary>
        /// <param name="restClient">The restClient parameter.</param>
        /// <param name="assistantClient">The Sitefinity Assistant client parameter.</param>
        /// <param name="requestContext">The requestContext parameter.</param>
        /// <param name="antiforgery">The antiforgery parameter.</param>
        /// <param name="httpContextAccessor">The httpContextAccessor parameter.</param>
        public SitefinityAssistantModel(
            IRestClient restClient,
            ISitefinityAssistantClient assistantClient,
            IRequestContext requestContext,
            IAntiforgery antiforgery,
            IHttpContextAccessor httpContextAccessor)
        {
            this.restClient = restClient;
            this.assistantClient = assistantClient;
            this.requestContext = requestContext;
            this.antiforgery = antiforgery;
            this.httpContextAccessor = httpContextAccessor;
        }

        /// <inheritdoc/>
        public virtual async Task<SitefinityAssistantViewModel> GetViewModel(IViewComponentContext<SitefinityAssistantEntity> context)
        {
            var entity = context.Entity;
            var versionInfo = await this.assistantClient.GetVersionInfoAsync();
            var viewModel = new SitefinityAssistantViewModel();
            viewModel.AssistantApiKey = entity.AssistantApiKey;
            viewModel.AssistantDisplayName = entity.Nickname;
            viewModel.AssistantGreetingMessage = entity.GreetingMessage;
            viewModel.AssistantAvatarUrl = await this.restClient.GetSingleSelectedImageUrlAsync(entity.AssistantAvatar);
            viewModel.DisplayMode = entity.DisplayMode;
            viewModel.ChatServiceName = ChatServiceType.AzureAssistantChatService.ToString();
            viewModel.ServiceUrl = "/api/SitefinityAssistantChatService/";
            viewModel.ProductVersion = versionInfo?.ProductVersion;
            viewModel.RequestVerificationToken = this.antiforgery.GetAndStoreTokens(this.httpContextAccessor.HttpContext).RequestToken;
            viewModel.OpeningChatIconUrl = await this.restClient.GetSingleSelectedImageUrlAsync(entity.OpeningChatIcon);
            viewModel.ClosingChatIconUrl = await this.restClient.GetSingleSelectedImageUrlAsync(entity.ClosingChatIcon);
            viewModel.ContainerId = entity.ContainerId;
            viewModel.InputPlaceholder = entity.PlaceholderText;
            viewModel.Notice = entity.Notice;
            viewModel.CustomCss = entity.CustomCss;
            viewModel.CssClass = entity.CssClass;
            viewModel.Attributes = entity.Attributes;

            return viewModel;
        }
    }
}
