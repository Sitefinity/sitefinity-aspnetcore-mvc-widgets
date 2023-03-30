using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Progress.Sitefinity.AspNetCore.Widgets.Models.NativeChat.Dto;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.NativeChat
{
    /// <summary>
    /// The INativeChatClient interface.
    /// </summary>
    internal interface INativeChatClient : IDisposable
    {
        /// <summary>
        /// The HealthCheck method.
        /// </summary>
        /// <returns>The health of the bots.</returns>
        bool HealthCheck();

        /// <summary>
        /// Get a list of bots.
        /// </summary>
        /// <returns>A list of <see cref="NativeChatBotDto"/> representing the bots.</returns>
        Task<List<NativeChatBotDto>> Bots();

        /// <summary>
        /// Get a list of bot channels.
        /// </summary>
        /// <param name="botId">The botId parameter.</param>
        /// <returns>A <see cref="NativeChatChannelDto"/> representing the bot channels.</returns>
        Task<List<NativeChatChannelDto>> BotChannels(string botId);

        /// <summary>
        /// Gets a bot conversation.
        /// </summary>
        /// <param name="botId">The botId parameter.</param>
        /// <param name="name">The name parameter.</param>
        /// <returns>A <see cref="NativeChatConversationDTO"/> representing the conversation.</returns>
        Task<NativeChatConversationDTO> BotConversations(string botId, string name);
    }
}
