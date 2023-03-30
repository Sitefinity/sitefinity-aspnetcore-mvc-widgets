using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Progress.Sitefinity.AspNetCore.Models;
using Progress.Sitefinity.AspNetCore.Widgets.Attributes;
using Progress.Sitefinity.Renderer.Designers;
using Progress.Sitefinity.Renderer.Designers.Attributes;
using Progress.Sitefinity.Renderer.Entities.Content;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.NativeChat
{
    /// <summary>
    /// The test model for the load tests widget.
    /// </summary>
    public class NativeChatEntity
    {
        /// <summary>
        /// Gets or sets BotId.
        /// </summary>
        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection("Chatbot", 0)]
        [DisplayName("Select a chatbot")]
        [DataType(customDataType: "choices")]
        [ExternalDataChoice]
        [DefaultValue("")]
        public string BotId { get; set; }

        /// <summary>
        /// Gets or sets the Nickname property.
        /// </summary>
        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection("Chatbot", 1)]
        [DisplayName("Nickname of the bot")]
        [Description("Name displayed before bot's messages in the chat.")]
        public string Nickname { get; set; }

        /// <summary>
        /// Gets or sets the BotAvatar property.
        /// </summary>
        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection("Chatbot", 2)]
        [DisplayName("Avatar of the bot")]
        [Content(Type = "Telerik.Sitefinity.Libraries.Model.Image", AllowMultipleItemsSelection = false)]
        public MixedContentContext BotAvatar { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Proactive is enabled.
        /// </summary>
        [ConditionalVisibility("{\"conditions\":[{\"fieldName\":\"BotId\",\"operator\":\"NotEquals\",\"value\":\"\"}]}")]
        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection("Chatbot", 3)]
        [DisplayName("Start conversation automatically")]
        [Description("Chat window is opened on page load and the bot starts the conversation first.")]
        [DataType(customDataType: KnownFieldTypes.ChipChoice)]
        [DefaultValue(false)]
        [Choice("[{\"Title\":\"Yes\",\"Name\":\"Yes\",\"Value\":\"True\",\"Icon\":null},{\"Title\":\"No\",\"Name\":\"No\",\"Value\":\"False\",\"Icon\":null}]")]
        public bool Proactive { get; set; }

        /// <summary>
        /// Gets or sets the UserMessage property.
        /// </summary>
        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection("Chatbot", 4)]
        [DisplayName("Conversation trigger expressions")]
        [Description("You can customize bot's initial conversaton by adding phrases on specific topic. The bot will skip general introduction and strart with questions directly related to this topic.")]
        [DataType(customDataType: "textArea")]
        [ConditionalVisibility("{\"operator\":\"And\",\"conditions\":[{\"fieldName\":\"Proactive\",\"operator\":\"Equals\",\"value\":\"False\"},{\"fieldName\":\"BotId\",\"operator\":\"NotEquals\",\"value\":\"\"}]}")]
        public string UserMessage { get; set; }

        /// <summary>
        /// Gets or sets the ConversationId property.
        /// </summary>
        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection("Chatbot", 4)]
        [DisplayName("Conversation to start with")]
        [DataType(customDataType: KnownFieldTypes.Choices)]
        [ConditionalVisibility("{\"operator\":\"And\",\"conditions\":[{\"fieldName\":\"Proactive\",\"operator\":\"Equals\",\"value\":\"True\"},{\"fieldName\":\"BotId\",\"operator\":\"NotEquals\",\"value\":\"\"}]}")]
        [Choice(ServiceUrl = "Default.GetConversations(botId='{0}')", ServiceCallParameters = "[{ \"botId\" : \"{0}\"}]")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2243:Attribute string literals should parse correctly", Justification = "By design")]
        [Required]
        public string ConversationId { get; set; }

        /// <summary>
        /// Gets or sets the ChatMode property.
        /// </summary>
        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection("Chat window", 0)]
        [DisplayName("Chat window mode")]
        [DataType(customDataType: KnownFieldTypes.RadioChoice)]
        public ChatWindowMode ChatMode { get; set; }

        /// <summary>
        /// Gets or sets the OpeningChatIcon property.
        /// </summary>
        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection("Chat window", 1)]
        [DisplayName("Opening chat icon")]
        [Description("Select a custom icon for opening chat window. If left empty, default icon will be displayed.")]
        [Content(Type = "Telerik.Sitefinity.Libraries.Model.Image", AllowMultipleItemsSelection = false)]
        [ConditionalVisibility("{\"conditions\":[{\"fieldName\":\"ChatMode\",\"operator\":\"Equals\",\"value\":\"modal\"}]}")]
        public MixedContentContext OpeningChatIcon { get; set; }

        /// <summary>
        /// Gets or sets the ClosingChatIcon property.
        /// </summary>
        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection("Chat window", 2)]
        [DisplayName("Closing chat icon")]
        [Description("Select a custom icon for closing chat window. If left empty, default icon will be displayed.")]
        [Content(Type = "Telerik.Sitefinity.Libraries.Model.Image", AllowMultipleItemsSelection = false)]
        [ConditionalVisibility("{\"conditions\":[{\"fieldName\":\"ChatMode\",\"operator\":\"Equals\",\"value\":\"modal\"}]}")]
        public MixedContentContext ClosingChatIcon { get; set; }

        /// <summary>
        /// Gets or sets the ContainerId property.
        /// </summary>
        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection("Chat window", 3)]
        [DisplayName("Container ID")]
        [Description("ID of the HTML element that will host the chat widget.")]
        [ConditionalVisibility("{\"conditions\":[{\"fieldName\":\"ChatMode\",\"operator\":\"Equals\",\"value\":\"inline\"}]}")]
        public string ContainerId { get; set; }

        /// <summary>
        /// Gets or sets the Placeholder property.
        /// </summary>
        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection("Message box", 0)]
        [DisplayName("Placeholder text in the message box")]
        [Placeholder("Type a message...")]
        public string Placeholder { get; set; }

        /// <summary>
        /// Gets or sets the ShowPickers property.
        /// </summary>
        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection("Message box", 1)]
        [DisplayName("Show...")]
        [DefaultValue(ChatPickers.FilePicker | ChatPickers.LocationPicker)]
        [Choice("[{\"Title\":\"FilePicker\",\"Name\":\"File Picker\",\"Value\":1,\"Icon\":null},{\"Title\":\"LocationPicker\",\"Name\":\"Location Picker\",\"Value\":2,\"Icon\":null}]")]
        public ChatPickers ShowPickers { get; set; }

        /// <summary>
        /// Gets or sets the LocationPickerLabel property.
        /// </summary>
        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection("Message box", 2)]
        [DisplayName("Button label for location")]
        [Description("Submit button text used in location picker that can be popped from send message area of widget.")]
        [Placeholder("Submit")]
        [ConditionalVisibility("{\"operator\":\"Or\",\"conditions\":[{\"fieldName\":\"ShowPickers\",\"operator\":\"Equals\",\"value\":\"01\"},{\"fieldName\":\"ShowPickers\",\"operator\":\"Equals\",\"value\":\"11\"}]}")]
        public string LocationPickerLabel { get; set; }

        /// <summary>
        /// Gets or sets the GoogleApiKey property.
        /// </summary>
        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection("Message box", 3)]
        [DisplayName("Google API key")]
        [ConditionalVisibility("{\"operator\":\"Or\",\"conditions\":[{\"fieldName\":\"ShowPickers\",\"operator\":\"Equals\",\"value\":\"01\"},{\"fieldName\":\"ShowPickers\",\"operator\":\"Equals\",\"value\":\"11\"}]}")]
        public string GoogleApiKey { get; set; }

        /// <summary>
        /// Gets or sets the DefaultLocation property.
        /// </summary>
        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection("Message box", 4)]
        [DisplayName("Default latitude and longitude")]
        [Description("Used to center the location picker in case the user declines the prompt to allow geolocation in the browser.")]
        [ConditionalVisibility("{\"operator\":\"Or\",\"conditions\":[{\"fieldName\":\"ShowPickers\",\"operator\":\"Equals\",\"value\":\"01\"},{\"fieldName\":\"ShowPickers\",\"operator\":\"Equals\",\"value\":\"11\"}]}")]
        public string DefaultLocation { get; set; }

        /// <summary>
        /// Gets or sets the CSS class of the button.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [DisplayName("CSS class")]
        public string CssClass { get; set; }

        /// <summary>
        /// Gets or sets the CustomCss property.
        /// </summary>
        [Category("Advanced")]
        [DisplayName("CSS for custom design")]
        [Placeholder("type URL or path to file...")]
        public string CustomCss { get; set; }

        /// <summary>
        /// Gets or sets the Locale property.
        /// </summary>
        [Category("Advanced")]
        [DisplayName("Locale")]
        [DefaultValue("en")]
        [Description("Currently supported major locales by NativeChat: ‘en’, ‘ar’, ‘pt’, ‘de’, ‘es’, ‘fi’, ‘bg’, ‘it’, ‘nl’, ‘hr’.")]
        public string Locale { get; set; }

        /// <summary>
        /// Gets or sets the attributes for the recommendations widget.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [ContentSection(Constants.ContentSectionTitles.Attributes, 0)]
        [DisplayName("Attributes for...")]
        [LengthDependsOn(null, "", "", ExtraRecords = "[{\"Name\": \"NativeChat\", \"Title\": \"NativeChat\"}]")]
        [DataType(customDataType: KnownFieldTypes.Attributes)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Must be able to set in property editor.")]
        public IDictionary<string, IList<AttributeModel>> Attributes { get; set; }
    }
}
