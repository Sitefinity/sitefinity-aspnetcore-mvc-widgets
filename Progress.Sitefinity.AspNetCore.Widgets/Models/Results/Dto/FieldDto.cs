using System.Collections.Generic;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Results.Dto
{
    /// <summary>
    /// Represents a field of a resource returned from the Agentic RAG API.
    /// </summary>
    internal class FieldDto
    {
        /// <summary>
        /// Gets or sets the paragraphs of the field.
        /// </summary>
        public IDictionary<string, ParagraphDto> Paragraphs { get; set; }
    }
}
