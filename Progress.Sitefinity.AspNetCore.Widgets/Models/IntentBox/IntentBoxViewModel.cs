using System.Collections.Generic;
using Progress.Sitefinity.AspNetCore.Models;
using Progress.Sitefinity.RestSdk.Clients.Pages.Dto;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.IntentBox
{
    /// <summary>
    /// The view model for the IntentBox widget.
    /// </summary>
    public class IntentBoxViewModel
    {
        /// <summary>
        /// Gets or sets the input ID.
        /// </summary>
        public string InputId { get; set; }

        /// <summary>
        /// Gets or sets the display mode.
        /// </summary>
        public string DisplayMode { get; set; }

        /// <summary>
        /// Gets or sets the action to take after the intent is submitted.
        /// </summary>
        public string AfterIntentIsSubmitted { get; set; }

        /// <summary>
        /// Gets or sets the target page URL.
        /// </summary>
        public string TargetPageUrl { get; set; }

        /// <summary>
        /// Gets or sets the CSS class.
        /// </summary>
        public string CssClass { get; set; }

        /// <summary>
        /// Gets or sets the list of suggestions.
        /// </summary>
        public List<string> Suggestions { get; set; } = new List<string>();

        /// <summary>
        /// Gets or sets the label for inline mode.
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// Gets or sets the placeholder text for both inline and sticky mode.
        /// </summary>
        public string PlaceholderText { get; set; }

        /// <summary>
        /// Gets or sets the suggestions label.
        /// </summary>
        public string SuggestionsLabel { get; set; }

        /// <summary>
        /// Gets or sets the submit button tooltip.
        /// </summary>
        public string SubmitButtonTooltip { get; set; }
    }
}
