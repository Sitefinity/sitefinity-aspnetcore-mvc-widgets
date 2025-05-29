using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Progress.Sitefinity.AspNetCore.Configuration;
using Progress.Sitefinity.AspNetCore.Web;
using Progress.Sitefinity.AspNetCore.Widgets.Attributes;
using Progress.Sitefinity.AspNetCore.Widgets.Models.NativeChat.Dto;
using Progress.Sitefinity.Renderer.Designers.Dto;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.NativeChat
{
    /// <summary>
    /// The NativeChatClient class.
    /// </summary>
    internal sealed class NativeChatClient : INativeChatClient, IDisposable
    {
        private const string BotChannelsKey = "sf_native_chat_bot_channels_";
        private static readonly object BotChannelsCacheSync = new object();
        private string nativeChatApiEndpoint = "https://api.nativechat.com/v1/";

        string IExternalChoicesProvider.Name => ExternalChoicesProviderNames.NativeChatClient;

        private HttpClient HttpClient { get; set; }

        private IMemoryCache MemoryCache { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NativeChatClient"/> class.
        /// </summary>
        /// <param name="httpClientFactory">The httpClientFactory parameter.</param>
        /// <param name="config">The configuration parameter.</param>
        /// <param name="memoryCache">The cache parameter.</param>
        public NativeChatClient(IHttpClientFactory httpClientFactory, NativeChatConfig config, IMemoryCache memoryCache)
        {
            this.HttpClient = httpClientFactory.CreateClient();
            this.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("api-token", config.ApiKey);
            this.HttpClient.BaseAddress = new Uri(this.nativeChatApiEndpoint);
            this.MemoryCache = memoryCache;
        }

        /// <inheritdoc/>
        public bool HealthCheck()
        {
            HttpResponseMessage response = this.HttpClient.GetAsync("bots").Result;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }

            return false;
        }

        /// <inheritdoc/>
        public async Task<List<NativeChatBotDto>> Bots()
        {
            var bots = new List<NativeChatBotDto>();
            HttpResponseMessage response = await this.HttpClient.GetAsync("bots");

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var result = await response.Content.ReadAsStringAsync();
                bots = JsonConvert.DeserializeObject<List<NativeChatBotDto>>(result);
            }

            return bots;
        }

        /// <inheritdoc/>
        public async Task<NativeChatConversationDTO> BotConversations(string botId, string name)
        {
            if (!string.IsNullOrEmpty(botId))
            {
                ValidateBotId(botId);
                HttpResponseMessage response = await this.HttpClient.GetAsync($"bots/{botId}/entities/Conversation/Values/{name}");

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<NativeChatConversationDTO>(result);
                }
            }

            return null;
        }

        /// <inheritdoc/>
        public Task<List<NativeChatChannelDto>> BotChannels(string botId)
        {
            List<NativeChatChannelDto> channels = null;
            if (!string.IsNullOrEmpty(botId))
            {
                var cacheKey = BotChannelsKey + botId;
                if (!this.MemoryCache.TryGetValue(cacheKey, out channels))
                {
                    lock (BotChannelsCacheSync)
                    {
                        if (!this.MemoryCache.TryGetValue(cacheKey, out channels))
                        {
                            channels = new List<NativeChatChannelDto>();
                            var response = this.HttpClient.GetAsync($"bots/{botId}/channels").Result;

                            if (response.StatusCode == HttpStatusCode.OK)
                            {
                                var result = response.Content.ReadAsStringAsync().Result;
                                channels = JsonConvert.DeserializeObject<List<NativeChatChannelDto>>(result);

                                var cacheEntryOptions = new MemoryCacheEntryOptions()
                                    .SetSlidingExpiration(TimeSpan.FromHours(1));

                                this.MemoryCache.Set(cacheKey, channels, cacheEntryOptions);
                            }
                        }
                    }
                }
            }

            return Task.FromResult(channels);
        }

        /// <inheritdoc/>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1063:Implement IDisposable Correctly", Justification = "Ignored.")]
        public void Dispose()
        {
            this.HttpClient?.Dispose();

            GC.SuppressFinalize(this);
        }

        async Task<IEnumerable<ChoiceValueDto>> IExternalChoicesProvider.FetchChoicesAsync()
        {
            var choices = new List<ChoiceValueDto>();
            var bots = await this.Bots();

            foreach (var bot in bots)
            {
                choices.Add(new ChoiceValueDto(bot.DisplayName ?? bot.Name, bot.Id));
            }

            return choices;
        }

        private static void ValidateBotId(string botId)
        {
            if (Regex.IsMatch(botId, @"^[a-z0-9]{24}$"))
            {
                return;
            }

            throw new ArgumentException("Invalid bot Id");
        }
    }
}
