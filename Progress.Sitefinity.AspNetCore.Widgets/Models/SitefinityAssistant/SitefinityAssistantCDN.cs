using Microsoft.Extensions.Configuration;
using Progress.Sitefinity.AspNetCore.Configuration;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.SitefinityAssistant
{
    internal class SitefinityAssistantCDN : ISitefinityAssistantCDN
    {
        private readonly string hostName;
        private readonly string rootRelativePath;

        public SitefinityAssistantCDN(IConfiguration configuration)
        {
            var config = new SitefinityAssistantConfig();
            configuration.Bind(SitefinityAssistantConfig.SectionName, config);
            this.hostName = config.CdnHostName;
            this.rootRelativePath = config.CdnRootFolderRelativePath == null ?
                "staticfiles/" :
                (string.IsNullOrEmpty(config.CdnRootFolderRelativePath) ? string.Empty : $"{config.CdnRootFolderRelativePath.Trim('/')}/");
        }

        public string GetUrl(string filePath, string version)
        {
            string versionSuffix = string.IsNullOrEmpty(version) ? string.Empty : $"?ver={version}";

            return $"https://{this.hostName}/{this.rootRelativePath}{filePath}{versionSuffix}";
        }
    }
}
