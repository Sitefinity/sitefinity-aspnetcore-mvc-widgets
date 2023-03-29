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
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Progress.Sitefinity.AspNetCore.Configuration;
using Progress.Sitefinity.AspNetCore.Web;
using Progress.Sitefinity.AspNetCore.Widgets.Models.NativeChat.Dto;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.NativeChat
{
    /// <summary>
    /// The NativeChatClient class.
    /// </summary>
    internal sealed class NativeChatClient : INativeChatClient, IDisposable
    {
        private string nativeChatApiEndpoint = "https://api.nativechat.com/v1/";

        private HttpClient HttpClient { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NativeChatClient"/> class.
        /// </summary>
        /// <param name="httpClientFactory">The httpClientFactory parameter.</param>
        /// <param name="configuration">The configuration parameter.</param>
        public NativeChatClient(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            var config = new NativeChatConfig();
            configuration.Bind("NativeChat", config);

            this.HttpClient = httpClientFactory.CreateClient();
            this.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("api-token", config.ApiKey);
            this.HttpClient.BaseAddress = new Uri(this.nativeChatApiEndpoint);
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
        public async Task<List<NativeChatChannelDto>> BotChannels(string botId)
        {
            var channels = new List<NativeChatChannelDto>();
            if (!string.IsNullOrEmpty(botId))
            {
                var response = await this.HttpClient.GetAsync($"bots/{botId}/channels");

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    channels = JsonConvert.DeserializeObject<List<NativeChatChannelDto>>(result);
                }
            }

            return channels;
        }

        /// <inheritdoc/>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1063:Implement IDisposable Correctly", Justification = "Ignored.")]
        public void Dispose()
        {
            this.HttpClient?.Dispose();

            GC.SuppressFinalize(this);
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
