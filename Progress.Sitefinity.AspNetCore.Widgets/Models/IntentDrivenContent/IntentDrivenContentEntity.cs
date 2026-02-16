using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Progress.Sitefinity.AspNetCore.Models.Common;
using Progress.Sitefinity.AspNetCore.ViewComponents.AttributeConfigurator.Attributes;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Common;
using Progress.Sitefinity.Renderer.Designers;
using Progress.Sitefinity.Renderer.Designers.Attributes;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.IntentDrivenContent
{
    /// <summary>
    /// Represents the entity model for the dynamically generated widget.
    /// </summary>
    public class IntentDrivenContentEntity : IHasMargins<MarginStyle>
    {
        /// <summary>
        /// Gets or sets the sections configuration.
        /// </summary>
        [DisplayName("Generated content contains..")]
        [Description("Specify the content structure. The components (title and summary, rich text, FAQ, etc.) will be filled with content from your site — contextually assembled or generated. If an element isn’t relevant to the user’s intent, it won’t be generated.")]
        [TableView(Reorderable = true, HideRowsIfEmpty = true)]
        [ContentSection("Content setup", 1)]
        public IList<SectionDto> SectionsConfiguration { get; set; } = new List<SectionDto>();

        /// <summary>
        /// Gets or sets the action to take when no user intent is provided.
        /// </summary>
        [ContentSection("Content setup", 3)]
        [DisplayName("When no intent is provided...")]
        [Choice("[{\"Name\":\"Display nothing\",\"Title\":\"Display nothing\",\"Value\":\"None\"},{\"Name\":\"Generate content for the following intent...\",\"Title\":\"Generate content for the following intent...\",\"Value\":\"GenerateWithPredefinedQuery\"}]")]
        [DataType(customDataType: KnownFieldTypes.RadioChoice)]
        [DefaultValue(NoIntentAction.None)]
        public NoIntentAction NoProvidedIntent { get; set; }

        /// <summary>
        /// Gets or sets the default query to be used when no user intent is provided.
        /// </summary>
        [DisplayName("")]
        [ContentSection("Content setup", 5)]
        [ConditionalVisibility("{\"conditions\":[{\"fieldName\":\"NoProvidedIntent\",\"operator\":\"Equals\",\"value\":\"GenerateWithPredefinedQuery\"}]}")]
        [Required]
        [MaxLength(1024, ErrorMessage = "The default query cannot exceed 1024 characters.")]
        public string DefaultQuery { get; set; } = "Credit cards and home loans";

        /// <summary>
        /// Gets or sets the view name.
        /// </summary>
        [ViewSelector(RegexFilter = "^(?!Section).+")]
        [ContentSection(Constants.ContentSectionTitles.DisplaySettings, 1)]
        [DisplayName("Intent-driven content template")]
        public string SfViewName { get; set; }

        /// <summary>
        /// Gets or sets the margins.
        /// </summary>
        [ContentSection(Constants.ContentSectionTitles.DisplaySettings, 2)]
        [DisplayName("Margins")]
        [TableView("IntentDrivenContent")]
        public MarginStyle Margins { get; set; }

        /// <summary>
        /// Gets or sets the CSS class for the intent-driven content container.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [ContentSection("Custom CSS classes")]
        [DisplayName("Intent-driven content")]
        public string ContentCssClass { get; set; }

        /// <summary>
        /// Gets or sets the CSS class for the page title and summary section.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [ContentSection("Custom CSS classes")]
        [DisplayName("Page title and summary")]
        public string PageTitleAndSummaryCssClass { get; set; }

        /// <summary>
        /// Gets or sets the CSS class for the section title and summary.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [ContentSection("Custom CSS classes")]
        [DisplayName("Section title and summary")]
        public string SectionTitleAndSummaryCssClass { get; set; }

        /// <summary>
        /// Gets or sets the CSS class for the rich text content.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [ContentSection("Custom CSS classes")]
        [DisplayName("Rich Text")]
        public string RichTextCssClass { get; set; }

        /// <summary>
        /// Gets or sets the CSS class for the content items list.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [ContentSection("Custom CSS classes")]
        [DisplayName("Content items - list")]
        public string ContentItemsListCssClass { get; set; }

        /// <summary>
        /// Gets or sets the CSS class for the content items cards.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [ContentSection("Custom CSS classes")]
        [DisplayName("Content items - cards")]
        public string ContentItemsCardsCssClass { get; set; }

        /// <summary>
        /// Gets or sets the CSS class for the hero section.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [ContentSection("Custom CSS classes")]
        [DisplayName("Hero")]
        public string HeroCssClass { get; set; }

        /// <summary>
        /// Gets or sets the CSS class for the FAQ section.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [ContentSection("Custom CSS classes")]
        [DisplayName("FAQ")]
        public string FaqCssClass { get; set; }
    }
}
