using System;
using Microsoft.Extensions.Configuration;
using Progress.Sitefinity.AspNetCore.Configuration;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.SitefinityAssistant
{
    internal class SitefinityAssistantCDN : ISitefinityAssistantCDN
    {
        private readonly string hostName;

        public SitefinityAssistantCDN(IConfiguration configuration)
        {
            var config = new SitefinityAssistantConfig();
            configuration.Bind(SitefinityAssistantConfig.SectionName, config);
            this.hostName = config.CdnHostName;
        }

        public string GetUrl(string filePath, string version)
        {
            if (string.IsNullOrEmpty(this.hostName))
                throw new ArgumentException("CdnHostName is not configured in SitefinityAssistantConfig.");

            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentException("Invalid value for filePath", nameof(filePath));

            string versionSuffix = string.IsNullOrEmpty(version) ? string.Empty : $"?ver={version}";

            return $"https://{this.hostName}/{filePath}{versionSuffix}";
        }
    }
}
