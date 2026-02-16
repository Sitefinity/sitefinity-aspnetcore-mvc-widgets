using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Nodes;
using Progress.Sitefinity.Renderer.Designers;
using Progress.Sitefinity.Renderer.Designers.Attributes;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.IntentDrivenContent
{
    /// <summary>
    /// Represents a section configuration for the dynamically generated widget.
    /// </summary>
    public class SectionDto
    {
        /// <summary>
        /// Gets or sets the section type.
        /// </summary>
        [DisplayName(" ")]
        [Choice("[{\"Title\": \"Page title and summary\",\"Value\":\"TitleAndSummary\"},{\"Title\":\"Rich Text\",\"Value\":\"RichText\"},{\"Title\":\"FAQ\",\"Value\":\"FAQ\"},{\"Title\":\"Hero\",\"Value\":\"Hero\"},{\"Title\":\"Content items - list\",\"Value\":\"ContentList\"},{\"Title\":\"Content items - cards\",\"Value\":\"ContentListCards\"}]")]
        [DataType(KnownFieldTypes.Choices)]
        public SectionType SectionType { get; set; }
    }
}
