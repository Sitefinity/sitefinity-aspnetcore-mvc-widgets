﻿using System.Collections.Generic;
using Progress.Sitefinity.AspNetCore.Models;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.SitefinityAssistant
{
    /// <summary>
    /// The SitefinityAssistantViewModel class.
    /// </summary>
    public class SitefinityAssistantViewModel
    {
        /// <summary>
        /// Gets or sets AssistantApiKey.
        /// </summary>
        public string AssistantApiKey { get; set; }

        /// <summary>
        /// Gets or sets the assistant display name.
        /// </summary>
        public string AssistantDisplayName { get; set; }

        /// <summary>
        /// Gets or sets the assistant greeting message.
        /// </summary>
        public string AssistantGreetingMessage { get; set; }

        /// <summary>
        /// Gets or sets assistant avatar url.
        /// </summary>
        public string AssistantAvatarUrl { get; set; }

        /// <summary>
        /// Gets or sets the AI service url.
        /// </summary>
        public string ServiceUrl { get; set; }

        /// <summary>
        /// Gets or sets the product version.
        /// </summary>
        public string ProductVersion { get; set; }

        /// <summary>
        /// Gets or sets the chat service name.
        /// </summary>
        public string ChatServiceName { get; set; }

        /// <summary>
        /// Gets or sets DisplayMode.
        /// </summary>
        public DisplayMode DisplayMode { get; set; }

        /// <summary>
        /// Gets or sets OpeningChatIconUrl.
        /// </summary>
        public string OpeningChatIconUrl { get; set; }

        /// <summary>
        /// Gets or sets ClosingChatIconUrl.
        /// </summary>
        public string ClosingChatIconUrl { get; set; }

        /// <summary>
        /// Gets or sets ContainerId.
        /// </summary>
        public string ContainerId { get; set; }

        /// <summary>
        /// Gets or sets the chat input placeholder text.
        /// </summary>
        public string InputPlaceholder { get; set; }

        /// <summary>
        /// Gets or sets the notice that is displayed below the chat message box.
        /// </summary>
        public string Notice { get; set; }

        /// <summary>
        /// Gets or sets the css class.
        /// </summary>
        public string CssClass { get; set; }

        /// <summary>
        /// Gets or sets CustomCss.
        /// </summary>
        public string CustomCss { get; set; }

        /// <summary>
        /// Gets or sets the attributes for the widget.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Must be able to set in property editor.")]
        public IDictionary<string, IList<AttributeModel>> Attributes { get; set; }
    }
}
