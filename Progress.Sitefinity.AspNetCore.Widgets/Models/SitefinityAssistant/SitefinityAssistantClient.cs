using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using Progress.Sitefinity.AspNetCore.Configuration;
using Progress.Sitefinity.AspNetCore.Widgets.Attributes;
using Progress.Sitefinity.AspNetCore.Widgets.Models.SitefinityAssistant.Dto;
using Progress.Sitefinity.Renderer.Designers.Dto;
using Progress.Sitefinity.RestSdk;
using Progress.Sitefinity.RestSdk.Dto;
using Progress.Sitefinity.RestSdk.OData;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.SitefinityAssistant
{
    internal class SitefinityAssistantClient : ISitefinityAssistantClient
    {
        private readonly IHttpContextAccessor httpAccessor;
        private readonly ILogger<SitefinityAssistantClient> logger;

        private readonly string aiServiceAccessKeyId;
        private readonly string aiServiceAccessKeySecret;

        string IExternalChoicesProvider.Name => ExternalChoicesProviderNames.SitefinityAssistantClient;

        private HttpClient AssistantClient { get; set; }

        private HttpClient AdministrationClient { get; set; }

        public SitefinityAssistantClient(
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration,
            IHttpContextAccessor httpAccessor,
            ILogger<SitefinityAssistantClient> logger)
        {
            this.AssistantClient = httpClientFactory.CreateClient();
            this.AdministrationClient = httpClientFactory.CreateClient();

            var config = new SitefinityAssistantConfig();
            configuration.Bind(SitefinityAssistantConfig.SectionName, config);

            if (!string.IsNullOrEmpty(config.AIServiceBaseUrl))
            {
                this.AssistantClient.BaseAddress = new Uri(config.AIServiceBaseUrl);
            }

            if (!string.IsNullOrEmpty(config.AssistantsAdminApiBaseUrl))
            {
                this.AdministrationClient.BaseAddress = new Uri(config.AssistantsAdminApiBaseUrl);
            }

            this.aiServiceAccessKeyId = config.AIServiceAccessKeyId;
            this.aiServiceAccessKeySecret = config.AIServiceAccessKeySecret;

            this.httpAccessor = httpAccessor;
            this.logger = logger;
        }

        public async Task<VersionInfoDto> GetVersionInfoAsync()
        {
            try
            {
                return await this.AdministrationClient.GetFromJsonAsync<VersionInfoDto>(AssistantApiConstants.VersionInfoEndpoint);
            }
            catch (Exception ex)
            {
                this.logger.LogInformation($"Error calling Sitefinity Assistant version info API? Error message: {ex.Message}");
                return null;
            }
        }

        public async Task<List<AssistantDto>> GetAssistantsAsync()
        {
            List<AssistantDto> result = new List<AssistantDto>();

            try
            {
                var httpContext = this.httpAccessor.HttpContext;
                var restClient = httpContext.RequestServices.GetRequiredService<IODataRestClient>();
                var args = new RequestArgs();
                var requestCookie = httpContext.Request.Headers[HeaderNames.Cookie];

                if (!string.IsNullOrEmpty(requestCookie))
                {
                    args.AdditionalHeaders.Add(HeaderNames.Cookie, requestCookie);
                }

                if (httpContext.Request.Query.TryGetValue(QueryParamNames.Site, out var siteId))
                {
                    args.AdditionalQueryParams.Add(QueryParamNames.Site, siteId);
                }

                var sitefinityConfig = httpContext.RequestServices.GetRequiredService<ISitefinityConfig>();
                if (!string.IsNullOrEmpty(sitefinityConfig.WebServiceApiKey))
                {
                    args.AdditionalHeaders.Remove(Constants.Headers.WebServiceApiKey);
                    args.AdditionalHeaders.Add(Constants.Headers.WebServiceApiKey, sitefinityConfig.WebServiceApiKey);
                }

                await restClient.Init(args);

                var response = await restClient.ExecuteUnboundFunction<ODataWrapper<List<AssistantDto>>>(new BoundFunctionArgs()
                {
                    Name = AssistantApiConstants.SitefinityGetAssistantsFunctionName
                });

                var assistants = response.Value;

                if (assistants?.Count > 0)
                {
                    result.AddRange(assistants);
                }
            }
            catch (Exception ex)
            {
                this.logger.LogInformation($"Error calling Sitefinity GetAiAssistants API? Error message: {ex.Message}");
            }

            return result;
        }

        public async Task<HttpResponseMessage> SendAssistantRequestAsync(
            SitefinityAssistantClientRequest requestMessage)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, requestMessage.AssistantEndpoint);

            foreach (var pair in requestMessage.Headers)
            {
                request.Headers.TryAddWithoutValidation(pair.Key, pair.Value);
            }

            if (requestMessage.Body != null)
            {
                using (var reader = new StreamReader(requestMessage.Body, Encoding.UTF8))
                {
                    var requestBody = await reader.ReadToEndAsync();
                    AddSignitureHeaders(
                        request.Headers, requestBody, this.aiServiceAccessKeyId, this.aiServiceAccessKeySecret);

                    var stream = new MemoryStream(Encoding.UTF8.GetBytes(requestBody));

                    request.Content = new StreamContent(stream);
                }
            }

            return await this.AssistantClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
        }

        /// <inheritdoc/>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1063:Implement IDisposable Correctly", Justification = "Ignored.")]
        public void Dispose()
        {
            this.AssistantClient?.Dispose();
            this.AdministrationClient?.Dispose();

            GC.SuppressFinalize(this);
        }

        async Task<IEnumerable<ChoiceValueDto>> IExternalChoicesProvider.FetchChoicesAsync()
        {
            var choices = new List<ChoiceValueDto>();
            var assistants = await this.GetAssistantsAsync();

            foreach (var assistant in assistants)
            {
                choices.Add(new ChoiceValueDto(assistant.Name, assistant.ApiKey));
            }

            return choices;
        }

        private static void AddSignitureHeaders(
             HttpRequestHeaders headers,
             string body,
             string accessKeyId,
             string accessKeySecret)
        {
            var timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            var stringToSign = $"{body}_{timestamp}";

            using (var hmacsha = new HMACSHA256(Encoding.UTF8.GetBytes(accessKeySecret)))
            {
                var signature = Convert.ToBase64String(hmacsha.ComputeHash(Encoding.UTF8.GetBytes(stringToSign)));

                if (!string.IsNullOrWhiteSpace(accessKeyId))
                {
                    headers.Add("Sf-Credential", accessKeyId);
                }

                headers.Add("Sf-Signature", signature);
                headers.Add("Sf-Signature-Timestamp", timestamp.ToString());
            }
        }
    }
}
