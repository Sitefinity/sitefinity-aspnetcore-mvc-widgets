using System;
using Microsoft.Extensions.Configuration;
using Progress.Sitefinity.AspNetCore.Configuration;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.SitefinityAssistant
{
    /// <summary>
    /// Provides urls to static files served from the Sitefinity Assistant CDN.
    /// </summary>
    public class SitefinityAssistantCDN : ISitefinityAssistantCDN
    {
        private readonly string hostName;

        /// <summary>
        /// Initializes a new instance of the <see cref="SitefinityAssistantCDN"/> class.
        /// </summary>
        /// <param name="configuration">The application configuration used to resolve the <see cref="SitefinityAssistantConfig"/> section.</param>
        public SitefinityAssistantCDN(IConfiguration configuration)
        {
            var config = new SitefinityAssistantConfig();
            configuration.Bind(SitefinityAssistantConfig.SectionName, config);
            this.hostName = config.CdnHostName;
        }

        /// <summary>
        /// Gets the url of a static file served from the CDN.
        /// </summary>
        /// <param name="filePath">The relative file path.</param>
        /// <param name="version">The version which will be appended as a query string to the url.</param>
        /// <returns>The absolute url to the static file.</returns>
        /// <exception cref="ArgumentException">Thrown when the CDN host name is not configured or <paramref name="filePath"/> is null or empty.</exception>
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
