using System.Threading.Tasks;
using Progress.Sitefinity.AspNetCore.Configuration;
using Progress.Sitefinity.AspNetCore.ViewComponents;
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
        private readonly ISitefinityConfig config;
        private readonly ISitefinityAssistantClient assistantClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="SitefinityAssistantModel"/> class.
        /// </summary>
        /// <param name="restClient">The restClient parameter.</param>
        /// <param name="config">The Sitefinity configurations.</param>
        /// <param name="assistantClient">The Sitefinity Assistant client parameter.</param>
        public SitefinityAssistantModel(
            IRestClient restClient,
            ISitefinityConfig config,
            ISitefinityAssistantClient assistantClient)
        {
            this.restClient = restClient;
            this.config = config;
            this.assistantClient = assistantClient;
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
            viewModel.ServiceUrl = $"/{this.config.WebServicePath}/SitefinityAssistantChatService/";
            viewModel.ProductVersion = versionInfo?.ProductVersion;
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
