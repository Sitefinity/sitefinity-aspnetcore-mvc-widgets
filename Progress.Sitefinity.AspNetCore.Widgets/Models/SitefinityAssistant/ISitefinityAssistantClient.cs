using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Progress.Sitefinity.AspNetCore.Widgets.Attributes;
using Progress.Sitefinity.AspNetCore.Widgets.Models.SitefinityAssistant.Dto;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.SitefinityAssistant
{
    /// <summary>
    /// The ISitefinityAssistantClient interface.
    /// </summary>
    internal interface ISitefinityAssistantClient : IExternalChoicesProvider, IDisposable
    {
        /// <summary>
        /// Gets version information of the Assistant API.
        /// </summary>
        /// <returns>Returns the version information.</returns>
        Task<VersionInfoDto> GetVersionInfoAsync();

        /// <summary>
        /// Get a list of assistants.
        /// </summary>
        /// <returns>A list of <see cref="AssistantDto"/> representing the AI assistants.</returns>
        Task<List<AssistantDto>> GetAssistantsAsync();

        /// <summary>
        /// Sends request to the assistant API.
        /// </summary>
        /// <param name="requestMessage">The request message specifying API endpoint, body content, assistant API key and other headers.</param>
        /// <returns>Returns the Http response.</returns>
        Task<HttpResponseMessage> SendAssistantRequestAsync(SitefinityAssistantClientRequest requestMessage);
    }
}
