using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using Progress.Sitefinity.AspNetCore.Configuration;
using Progress.Sitefinity.AspNetCore.Widgets.Models.SitefinityAssistant.Dto;
using Progress.Sitefinity.RestSdk;
using Progress.Sitefinity.RestSdk.Dto;
using Progress.Sitefinity.RestSdk.OData;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.SitefinityAssistant
{
    internal class SitefinityAssistantClient : ISitefinityAssistantClient
    {
        private readonly IHttpContextAccessor httpAccessor;
        private readonly ILogger<SitefinityAssistantClient> logger;

        public SitefinityAssistantClient(IHttpContextAccessor httpAccessor, ILogger<SitefinityAssistantClient> logger)
        {
            this.httpAccessor = httpAccessor;
            this.logger = logger;
        }

        public async Task<VersionInfoDto> GetVersionInfoAsync(string assistantType)
        {
            try
            {
                IODataRestClient restClient = await this.GetInitializedRestClientAsync();
                var response = await restClient.ExecuteUnboundFunction<VersionInfoDto>(new BoundFunctionArgs()
                {
                    Name = assistantType == "PARAG" ? AssistantApiConstants.SitefinityGetPARAGAssistantVersionInfoFunctionName : AssistantApiConstants.SitefinityGetAssistantVersionInfoFunctionName
                });

                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogInformation($"Error calling Sitefinity GetAiAssistantVersionInfo API? Error message: {ex.Message}");
                return null;
            }
        }

        private async Task<IODataRestClient> GetInitializedRestClientAsync()
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

            return restClient;
        }
    }
}
