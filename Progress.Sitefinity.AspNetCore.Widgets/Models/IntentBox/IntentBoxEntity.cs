using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Progress.Sitefinity.AspNetCore.Models.Common;
using Progress.Sitefinity.AspNetCore.ViewComponents.AttributeConfigurator.Attributes;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Common;
using Progress.Sitefinity.Renderer.Designers;
using Progress.Sitefinity.Renderer.Designers.Attributes;
using Progress.Sitefinity.Renderer.Entities.Content;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.IntentBox
{
    /// <summary>
    /// Widget Entity class with properties definition.
    /// </summary>
    public class IntentBoxEntity : IHasMargins<MarginStyle>
    {
        /// <summary>
        /// Gets or sets the display mode.
        /// </summary>
        [DisplayName("After Intent is submitted...")]
        [Description("Make sure the selected page contains the Intent-driven content widget.")]
        [DataType(KnownFieldTypes.RadioChoice)]
        [Choice("[{\"Title\": \"Stay on the same page\",\"Value\":\"stay\"},{\"Title\":\"Redirect to page...\",\"Value\":\"redirect\"}]")]
        [DefaultValue("stay")]
        public string AfterIntentIsSubmitted { get; set; } = "stay";

        /// <summary>
        /// Gets or sets the target page.
        /// </summary>
        [DisplayName("")]
        [Required]
        [Content(Type = KnownContentTypes.Pages, AllowMultipleItemsSelection = false)]
        [ConditionalVisibility("{\"conditions\":[{\"fieldName\":\"AfterIntentIsSubmitted\",\"operator\":\"Equals\",\"value\":\"redirect\"}], \"inline\":\"true\"}")]
        public MixedContentContext TargetPage { get; set; }

        /// <summary>
        /// Gets or sets the list of suggestions that will be displayed in the intent box.
        /// </summary>
        [DisplayName("Suggestions")]
        [TableView(Reorderable = true)]
        public IList<string> Suggestions { get; set; } = new List<string>();

        /// <summary>
        /// Gets or sets the margins.
        /// </summary>
        [ContentSection(Constants.ContentSectionTitles.DisplaySettings, 2)]
        [DisplayName("Margins")]
        [TableView("IntentDrivenContent")]
        public MarginStyle Margins { get; set; }

        /// <summary>
        /// Gets or sets the view name.
        /// </summary>
        [ViewSelector]
        [ContentSection(Constants.ContentSectionTitles.DisplaySettings, 3)]
        [DisplayName("Intent box template")]
        public string SfViewName { get; set; }

        /// <summary>
        /// Gets or sets the CSS class.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [DisplayName("CSS class")]
        public string CssClass { get; set; }

        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [ContentSection("Labels and messages", 1)]
        [DefaultValue("What are you looking for today?")]
        [DisplayName("Label")]
        public string Label { get; set; }

        /// <summary>
        /// Gets or sets the placeholder text.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [ContentSection("Labels and messages", 2)]
        [DefaultValue("Ask for products, rates, or services...   ")]
        [DisplayName("Placeholder text")]
        public string PlaceholderText { get; set; }

        /// <summary>
        /// Gets or sets the suggestions label.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [ContentSection("Labels and messages", 3)]
        [DefaultValue("You can ask...")]
        [DisplayName("Suggestions label")]
        public string SuggestionsLabel { get; set; }

        /// <summary>
        /// Gets or sets the submit button tool tip.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [ContentSection("Labels and messages", 4)]
        [DefaultValue("Send")]
        [DisplayName("Submit button tooltip")]
        public string SubmitButtonTooltip { get; set; }
    }
}
