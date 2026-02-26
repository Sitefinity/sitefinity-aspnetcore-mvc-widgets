using System.Collections.Generic;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Results.Dto
{
    /// <summary>
    /// Represents a response from the Agentic RAG find API.
    /// </summary>
    internal class FindResponse
    {
        /// <summary>
        /// Gets or sets the resources found.
        /// </summary>
        public IDictionary<string, ResourceDto> Resources { get; set; }
    }
}
