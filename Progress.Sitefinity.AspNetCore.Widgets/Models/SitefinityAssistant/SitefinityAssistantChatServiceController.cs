using System;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.SitefinityAssistant
{
    /// <summary>
    /// Controller which provides proxy communication between the widget frontend and the azure AI service.
    /// </summary>
    [Route("api/[controller]")]
    public class SitefinityAssistantChatServiceController : Controller
    {
        private readonly ISitefinityAssistantClient assistantClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="SitefinityAssistantChatServiceController"/> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider which will provide access to internal dependency services.</param>
        public SitefinityAssistantChatServiceController(IServiceProvider serviceProvider)
        {
            this.assistantClient = serviceProvider.GetRequiredService<ISitefinityAssistantClient>();
        }

        /// <summary>
        /// Initializes the assistant thread.
        /// </summary>
        /// <returns>Returns the initialized assistant thread.</returns>
        [HttpPost(AssistantApiConstants.InitAssistantThreadEndpoint)]
        public Task<IActionResult> InitializeAssistantThreadAsync()
        {
            return this.ProxyResendRequestAsync(AssistantApiConstants.InitAssistantThreadEndpoint);
        }

        /// <summary>
        /// Sends user chat message.
        /// </summary>
        /// <returns>Returns the task while streaming the response.</returns>
        [HttpPost(AssistantApiConstants.ChatEndpoint)]
        public Task<IActionResult> ChatAsync()
        {
            return this.ProxyResendRequestAsync(AssistantApiConstants.ChatEndpoint);
        }

        private static bool IsAssistantServiceRelatedHeader(string headerKey)
        {
            return headerKey.Equals(AssistantApiConstants.AssistantThreadHeaderKey, StringComparison.OrdinalIgnoreCase) ||
                   headerKey.Equals(AssistantApiConstants.AssistantApiKeyHeaderKey, StringComparison.OrdinalIgnoreCase) ||
                   headerKey.StartsWith(AssistantApiConstants.AssistantCustomHeaderKeyPrefix, StringComparison.OrdinalIgnoreCase);
        }

        private async Task<IActionResult> ProxyResendRequestAsync(string endpoint)
        {
            var request = new SitefinityAssistantClientRequest();
            request.AssistantEndpoint = endpoint;
            request.Body = this.Request.Body;

            foreach (var pair in this.Request.Headers)
            {
                if (IsAssistantServiceRelatedHeader(pair.Key))
                {
                    request.Headers[pair.Key] = pair.Value;
                }
            }

            var response = await this.assistantClient.SendAssistantRequestAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);

            return new FileStreamResult(responseStream, MediaTypeNames.Text.Plain);
        }
    }
}
