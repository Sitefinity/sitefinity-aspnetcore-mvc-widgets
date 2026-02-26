namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Results.Dto
{
    /// <summary>
    /// Represents a paragraph of a field returned from the Agentic RAG API.
    /// </summary>
    internal class ParagraphDto
    {
        /// <summary>
        /// Gets or sets the text of the paragraph.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the order of the paragraph.
        /// </summary>
        public int Order { get; set; }
    }
}
