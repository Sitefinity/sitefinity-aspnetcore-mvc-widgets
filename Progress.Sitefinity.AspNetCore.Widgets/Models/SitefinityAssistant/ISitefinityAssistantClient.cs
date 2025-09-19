using System;
using System.Collections.Generic;
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
    }
}
