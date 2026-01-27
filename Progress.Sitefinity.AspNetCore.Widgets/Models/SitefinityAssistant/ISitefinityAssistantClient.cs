using System.Threading.Tasks;
using Progress.Sitefinity.AspNetCore.Widgets.Models.SitefinityAssistant.Dto;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.SitefinityAssistant
{
    /// <summary>
    /// The ISitefinityAssistantClient interface.
    /// </summary>
    internal interface ISitefinityAssistantClient
    {
        /// <summary>
        /// Gets version information of the Assistant API.
        /// </summary>
        /// <param name="assistantType">The name of the assistant type.</param>
        /// <returns>Returns the version information.</returns>
        Task<VersionInfoDto> GetVersionInfoAsync(string assistantType);
    }
}
