﻿using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Progress.Sitefinity.AspNetCore.Web;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Common;
using Progress.Sitefinity.RestSdk;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.NativeChat
{
    /// <summary>
    /// The NativeChatModel class.
    /// </summary>
    internal class NativeChatModel : INativeChatModel
    {
        private readonly IRestClient restClient;
        private readonly INativeChatClient nativeChatClient;
        private readonly IRequestContext requestContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="NativeChatModel"/> class.
        /// </summary>
        /// <param name="restClient">The restClient parameter.</param>
        /// <param name="nativeChatClient">The nativeChatClient parameter.</param>
        /// <param name="requestContext">The requestContext parameter.</param>
        public NativeChatModel(IRestClient restClient, INativeChatClient nativeChatClient, IRequestContext requestContext)
        {
            this.restClient = restClient;
            this.nativeChatClient = nativeChatClient;
            this.requestContext = requestContext;
        }

        /// <inheritdoc/>
        public virtual async Task<NativeChatViewModel> GetViewModel(NativeChatEntity entity)
        {
            var viewModel = new NativeChatViewModel
            {
                BotId = entity?.BotId,
            };

            await this.SetChannel(entity.BotId, viewModel);
            viewModel.Nickname = entity.Nickname;
            viewModel.BotAvatarUrl = await this.restClient.GetSingleSelectedImageUrlAsync(entity.BotAvatar);

            viewModel.Proactive = entity.Proactive;
            viewModel.UserMessage = entity.UserMessage;
            viewModel.ChatMode = entity.ChatMode;
            viewModel.Placeholder = entity.Placeholder;
            SetChatPickers(entity.ShowPickers, viewModel);

            viewModel.OpeningChatIconUrl = await this.restClient.GetSingleSelectedImageUrlAsync(entity.OpeningChatIcon);
            viewModel.ClosingChatIconUrl = await this.restClient.GetSingleSelectedImageUrlAsync(entity.ClosingChatIcon);

            viewModel.ContainerId = entity.ContainerId;
            viewModel.LocationPickerLabel = entity.LocationPickerLabel;
            viewModel.GoogleApiKey = entity.GoogleApiKey;
            SetDefaultLocation(entity.DefaultLocation, viewModel);
            viewModel.CustomCss = entity.CustomCss;
            viewModel.Locale = entity.Locale ?? this.requestContext.Culture.Name;

            viewModel.CssClass = entity.CssClass;
            viewModel.Attributes = entity.Attributes;

            await this.SetConversation(entity.BotId, entity.ConversationId, viewModel);

            return viewModel;
        }

        private static void SetChatPickers(ChatPickers showPickers, NativeChatViewModel viewModel)
        {
            var showPickersStr = showPickers.ToString();
            viewModel.ShowFilePicker = showPickersStr.Contains(ChatPickers.FilePicker.ToString(), StringComparison.CurrentCulture).ToString().ToLower(CultureInfo.CurrentCulture);
            viewModel.ShowLocationPicker = showPickersStr.Contains(ChatPickers.LocationPicker.ToString(), StringComparison.CurrentCulture).ToString().ToLower(CultureInfo.CurrentCulture);
        }

        private static void SetDefaultLocation(string location, NativeChatViewModel viewModel)
        {
            if (string.IsNullOrEmpty(location))
                return;

            var coordinates = location.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);

            if (coordinates.Length == 2)
            {
                if (double.TryParse(coordinates[0], out double latitude) && double.TryParse(coordinates[1], out double longitude))
                {
                    viewModel.Latitude = latitude;
                    viewModel.Longitude = longitude;
                }
            }
        }

        private async Task SetChannel(string botId, NativeChatViewModel viewModel)
        {
            if (!string.IsNullOrEmpty(botId))
            {
                var channels = await this.nativeChatClient.BotChannels(botId);

                // NativeChat specific: web channels have a providerName == "darvin"
                var webChannel = channels.Find(x => x.ProviderName == "darvin");
                if (webChannel != null)
                {
                    viewModel.ChannelId = webChannel.Id;
                    viewModel.ChannelAuthToken = webChannel.Config.AuthToken;
                }
            }
        }

        private async Task SetConversation(string botId, string conversationId, NativeChatViewModel viewModel)
        {
            if (!string.IsNullOrWhiteSpace(botId) && !string.IsNullOrWhiteSpace(conversationId))
            {
                var conversation = await this.nativeChatClient.BotConversations(botId, conversationId);
                if (conversation != null)
                    viewModel.UserMessage = conversation.Expressions.FirstOrDefault();
            }
        }
    }
}
