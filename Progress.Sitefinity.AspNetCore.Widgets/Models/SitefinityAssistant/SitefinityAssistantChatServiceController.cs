using System;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
        private const string ThrottlingError = "AI assistant service is temporarily unavailable due to high activity. Please try again later.";
        private const string QuotaExceededError = "AI assistant service is not available at the moment. Please try again later.";
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

        private static IActionResult ProxyResendErrorResponse(HttpResponseMessage response, string message)
        {
            return new BadRequestObjectResult(message)
            {
                StatusCode = (int)response.StatusCode,
                ContentTypes = { MediaTypeNames.Text.Plain }
            };
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

            if (response.StatusCode == HttpStatusCode.TooManyRequests)
            {
                return ProxyResendErrorResponse(response, ThrottlingError);
            }
            else if (response.StatusCode == HttpStatusCode.PaymentRequired)
            {
                return ProxyResendErrorResponse(response, QuotaExceededError);
            }

            response.EnsureSuccessStatusCode();
            var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);

            return new FileStreamResult(responseStream, MediaTypeNames.Text.Plain);
        }
    }
}
