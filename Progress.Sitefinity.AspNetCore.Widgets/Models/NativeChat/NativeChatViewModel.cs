using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Progress.Sitefinity.AspNetCore.Models;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.NativeChat
{
    /// <summary>
    /// The NativeChatViewModel class.
    /// </summary>
    public class NativeChatViewModel
    {
        /// <summary>
        /// Gets or sets BotId.
        /// </summary>
        public string BotId { get; set; }

        /// <summary>
        /// Gets or sets ChannelId.
        /// </summary>
        public string ChannelId { get; set; }

        /// <summary>
        /// Gets or sets ChannelAuthToker.
        /// </summary>
        public string ChannelAuthToken { get; set; }

        /// <summary>
        /// Gets or sets Nickname.
        /// </summary>
        public string Nickname { get; set; }

        /// <summary>
        /// Gets or sets BotAvatarUrl.
        /// </summary>
        public string BotAvatarUrl { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether property Proactive is set.
        /// </summary>
        public bool Proactive { get; set; }

        /// <summary>
        /// Gets or sets UserMessage.
        /// </summary>
        public string UserMessage { get; set; }

        /// <summary>
        /// Gets or sets ChatMode.
        /// </summary>
        public ChatWindowMode ChatMode { get; set; }

        /// <summary>
        /// Gets or sets Placeholder.
        /// </summary>
        public string Placeholder { get; set; }

        /// <summary>
        /// Gets or sets ShowFilePicker.
        /// </summary>
        public string ShowFilePicker { get; set; }

        /// <summary>
        /// Gets or sets ShowLocationPicker.
        /// </summary>
        public string ShowLocationPicker { get; set; }

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
        /// Gets or sets LocationPickerLabel.
        /// </summary>
        public string LocationPickerLabel { get; set; }

        /// <summary>
        /// Gets or sets GoogleApiKey.
        /// </summary>
        public string GoogleApiKey { get; set; }

        /// <summary>
        /// Gets or sets Latitude.
        /// </summary>
        public double? Latitude { get; set; }

        /// <summary>
        /// Gets or sets Longtitude.
        /// </summary>
        public double? Longitude { get; set; }

        /// <summary>
        /// Gets or sets CustomCss.
        /// </summary>
        public string CustomCss { get; set; }

        /// <summary>
        /// Gets or sets Locale.
        /// </summary>
        public string Locale { get; set; }

        /// <summary>
        /// Gets or sets the css class.
        /// </summary>
        public string CssClass { get; set; }

        /// <summary>
        /// Gets or sets the attributes for the breadcrumb.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Must be able to set in property editor.")]
        public IDictionary<string, IList<AttributeModel>> Attributes { get; set; }
    }
}
