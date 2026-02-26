using System.Collections.Generic;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Results.Dto
{
    /// <summary>
    /// Represents a resource returned from the Agentic RAG API.
    /// </summary>
    internal class ResourceDto
    {
        /// <summary>
        /// Gets or sets the title of the resource.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the thumbnail URL of the resource.
        /// </summary>
        public string Thumbnail { get; set; }

        /// <summary>
        /// Gets or sets the summary of the resource.
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// Gets or sets the origin of the resource.
        /// </summary>
        public OriginDto Origin { get; set; }

        /// <summary>
        /// Gets or sets the fields of the resource.
        /// </summary>
        public IDictionary<string, FieldDto> Fields { get; set; }
    }
}
