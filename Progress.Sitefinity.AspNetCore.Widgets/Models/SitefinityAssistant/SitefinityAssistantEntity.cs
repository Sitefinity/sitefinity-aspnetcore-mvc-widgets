using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Progress.Sitefinity.AspNetCore.Models;
using Progress.Sitefinity.Renderer.Designers;
using Progress.Sitefinity.Renderer.Designers.Attributes;
using Progress.Sitefinity.Renderer.Entities.Content;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.SitefinityAssistant
{
    /// <summary>
    /// The entity for the SitefinityAssistant widget.
    /// </summary>
    public class SitefinityAssistantEntity
    {
        /// <summary>
        /// Gets or sets the assistant type.
        /// </summary>
        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection("AI assistant", 0)]
        [DisplayName("AI assistant type")]
        [DataType(customDataType: KnownFieldTypes.Choices)]
        [Choice(ServiceUrl = "/Default.GetAvailableAssistantModules()", ServiceWarningMessage = "No AI assistants are found.")]
        [Placeholder("Select assistant type")]
        [Description("[{\"Type\":1,\"Chunks\":[{\"Value\":\"Sitefinity AI Assistant: \",\"Presentation\":[0]},{\"Value\":\"Answers from your site's published content only.\",\"Presentation\":[]}]},{\"Type\":1,\"Chunks\":[{\"Value\":\"Progress Agentic RAG: \",\"Presentation\":[0]},{\"Value\":\"Answers from the selected Agentic RAG connection.\",\"Presentation\":[]}]}]")]
        public string AssistantType { get; set; }

        /// <summary>
        /// Gets or sets the Progress agentic RAG knowledge box identifier.
        /// </summary>
        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection("AI assistant", 1)]
        [DisplayName("Agentic RAG connection")]
        [Description("[{\"Type\":1,\"Chunks\":[{\"Value\":\"A connection to a specific knowledge box in Progress Agentic RAG. Select which connection this widget should use to search and answer questions.\",\"Presentation\":[]}]},{\"Type\":1,\"Chunks\":[{\"Value\":\"Manage connections in \",\"Presentation\":[]},{\"Value\":\"Administration > Progress Agentic Rag connections\",\"Presentation\":[3]}]}]")]
        [DataType(customDataType: KnownFieldTypes.Choices)]
        [Choice(ServiceUrl = "/Default.GetConfiguredKnowledgeBoxes()", ServiceWarningMessage = "No Agentic RAG connections are found.")]
        [Placeholder("Select connection")]
        [ConditionalVisibility("{\"conditions\":[{\"fieldName\":\"AssistantType\",\"operator\":\"Equals\",\"value\":\"PARAG\"}]}")]
        public string KnowledgeBoxName { get; set; }

        /// <summary>
        /// Gets or sets the search configuration name.
        /// </summary>
        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection("AI assistant", 2)]
        [DisplayName("Search configuration")]
        [Description("[{\"Type\":1,\"Chunks\":[{\"Value\":\"A saved set of search settings that the AI uses to find content.\",\"Presentation\":[]}]},{\"Type\":1,\"Chunks\":[{\"Value\":\"Can be found in Progress Agentic Rag portal \",\"Presentation\":[]},{\"Value\":\"Search > Saved configurations\",\"Presentation\":[3]}]}]")]
        [DataType(customDataType: KnownFieldTypes.Choices)]
        [Choice(ServiceUrl = "/Default.GetSearchConfigurations(knowledgeBoxName=\'{0}\')", ServiceCallParameters = "[{ \"knowledgeBoxName\" : \"{0}\"}]")]
        [ConditionalVisibility("{\"conditions\":[{\"fieldName\":\"AssistantType\",\"operator\":\"Equals\",\"value\":\"PARAG\"}]}")]
        public string ConfigurationName { get; set; }

        /// <summary>
        /// Gets or sets Assistant identifier.
        /// </summary>
        [ContentSection("AI assistant", 1)]
        [DisplayName("Select an AI assistant")]
        [Description("[{\"Type\":1,\"Chunks\":[{\"Value\":\"AI assistants are created and managed in\",\"Presentation\":[]},{\"Value\":\"Administration > AI assistants\",\"Presentation\":[2]}]}]")]
        [DataType(customDataType: "choices")]
        [Placeholder("Select")]
        [Choice(ServiceUrl = "/Default.GetAiAssistantChoices()", ServiceWarningMessage = "No AI assistants are found.")]
        [DefaultValue("")]
        [ConditionalVisibility("{\"conditions\":[{\"fieldName\":\"AssistantType\",\"operator\":\"Equals\",\"value\":\"SAIA\"}]}")]
        public string AssistantApiKey { get; set; }

        /// <summary>
        /// Gets or sets the Nickname property.
        /// </summary>
        [ContentSection("AI assistant", 3)]
        [DisplayName("Nickname of the assistant")]
        [Description("Name displayed before assistant's messages in the chat.")]
        [ConditionalVisibility("{\"operator\":\"Or\",\"conditions\":[{\"fieldName\":\"AssistantType\",\"operator\":\"Equals\",\"value\":\"PARAG\" },{\"fieldName\":\"AssistantType\",\"operator\":\"Equals\",\"value\":\"SAIA\" }]}")]
        public string Nickname { get; set; }

        /// <summary>
        /// Gets or sets the GreetingMessage property.
        /// </summary>
        [ContentSection("AI assistant", 4)]
        [DisplayName("Greeting message")]
        [Description("You can customize the bot's initial words by adding a phrase that triggers conversation on a specific topic.")]
        [DataType(customDataType: "textArea")]
        [ConditionalVisibility("{\"operator\":\"Or\",\"conditions\":[{\"fieldName\":\"AssistantType\",\"operator\":\"Equals\",\"value\":\"PARAG\" },{\"fieldName\":\"AssistantType\",\"operator\":\"Equals\",\"value\":\"SAIA\" }]}")]
        public string GreetingMessage { get; set; }

        /// <summary>
        /// Gets or sets the AssistantAvatar property.
        /// </summary>
        [ContentSection("AI assistant", 5)]
        [DisplayName("Avatar of the assistant")]
        [Content(Type = "Telerik.Sitefinity.Libraries.Model.Image", AllowMultipleItemsSelection = false)]
        [ConditionalVisibility("{\"operator\":\"Or\",\"conditions\":[{\"fieldName\":\"AssistantType\",\"operator\":\"Equals\",\"value\":\"PARAG\" },{\"fieldName\":\"AssistantType\",\"operator\":\"Equals\",\"value\":\"SAIA\" }]}")]
        public MixedContentContext AssistantAvatar { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to show sources.
        /// </summary>
        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection("AI assistant", 6)]
        [DisplayName("Display sources")]
        [Description("In answers, display links to sources of information.")]
        [DefaultValue(true)]
        [DataType(customDataType: KnownFieldTypes.ChipChoice)]
        [Choice("[{\"Title\":\"Yes\",\"Name\":\"Yes\",\"Value\":\"True\",\"Icon\":null},{\"Title\":\"No\",\"Name\":\"No\",\"Value\":\"False\",\"Icon\":null}]")]
        [ConditionalVisibility("{\"conditions\":[{\"fieldName\":\"AssistantType\",\"operator\":\"Equals\",\"value\":\"PARAG\"}]}")]
        public bool ShowSources { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to show feedback buttons.
        /// </summary>
        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection("AI assistant", 7)]
        [DisplayName("Enable visitor feedback")]
        [Description("If enabled, site visitors can provide feedback on the assistant's answer in the chat window.")]
        [DefaultValue(true)]
        [DataType(customDataType: KnownFieldTypes.ChipChoice)]
        [Choice("[{\"Title\":\"Yes\",\"Name\":\"Yes\",\"Value\":\"True\",\"Icon\":null},{\"Title\":\"No\",\"Name\":\"No\",\"Value\":\"False\",\"Icon\":null}]")]
        [ConditionalVisibility("{\"conditions\":[{\"fieldName\":\"AssistantType\",\"operator\":\"Equals\",\"value\":\"PARAG\"}]}")]
        public bool ShowFeedback { get; set; }

        /// <summary>
        /// Gets or sets the ChatMode property.
        /// </summary>
        [ContentSection("Chat window", 0)]
        [DisplayName("Chat window mode")]
        [Description("[{\"Type\":1,\"Chunks\":[{\"Value\":\"Display overlay: \",\"Presentation\":[0]},{\"Value\":\"Chat appears in a small window, usually in the bottom right corner of the screen. It requires user interaction to open and overlays parts of the page content.\",\"Presentation\":[]}]},{\"Type\":1,\"Chunks\":[{\"Value\":\"Display inline: \",\"Presentation\":[0]},{\"Value\":\"Chat area is integrated into the page layout and does not overlay other elements. Suitable for long assistant responses and prompts.\",\"Presentation\":[]}]}]")]
        [DataType(customDataType: KnownFieldTypes.RadioChoice)]
        public DisplayMode DisplayMode { get; set; }

        /// <summary>
        /// Gets or sets the OpeningChatIcon property.
        /// </summary>
        [ContentSection("Chat window", 1)]
        [DisplayName("Opening chat icon")]
        [Description("Select a custom icon for opening chat window. If left empty, default icon will be displayed.")]
        [Content(Type = "Telerik.Sitefinity.Libraries.Model.Image", AllowMultipleItemsSelection = false)]
        [ConditionalVisibility("{\"conditions\":[{\"fieldName\":\"DisplayMode\",\"operator\":\"Equals\",\"value\":\"Modal\"}]}")]
        public MixedContentContext OpeningChatIcon { get; set; }

        /// <summary>
        /// Gets or sets the ClosingChatIcon property.
        /// </summary>
        [ContentSection("Chat window", 2)]
        [DisplayName("Closing chat icon")]
        [Description("Select a custom icon for closing chat window. If left empty, default icon will be displayed.")]
        [Content(Type = "Telerik.Sitefinity.Libraries.Model.Image", AllowMultipleItemsSelection = false)]
        [ConditionalVisibility("{\"conditions\":[{\"fieldName\":\"DisplayMode\",\"operator\":\"Equals\",\"value\":\"Modal\"}]}")]
        public MixedContentContext ClosingChatIcon { get; set; }

        /// <summary>
        /// Gets or sets the ContainerId property.
        /// </summary>
        [ContentSection("Chat window", 3)]
        [DisplayName("Container ID")]
        [Description("ID of the HTML element that will host the chat widget.")]
        [DefaultValue("sf-assistant-chat-container")]
        [ConditionalVisibility("{\"conditions\":[{\"fieldName\":\"DisplayMode\",\"operator\":\"Equals\",\"value\":\"Inline\"}]}")]
        public string ContainerId { get; set; }

        /// <summary>
        /// Gets or sets the PlaceholderText property.
        /// </summary>
        [ContentSection("Message box", 0)]
        [DisplayName("Placeholder text in the message box")]
        [DefaultValue("Ask anything...")]
        public string PlaceholderText { get; set; }

        /// <summary>
        /// Gets or sets the Notice property.
        /// </summary>
        [ContentSection("Message box", 1)]
        [DisplayName("Notice")]
        [DefaultValue("You are interacting with an AI-powered assistant and the responses are generated by AI.")]
        [Description("Text displayed under the message box, informing users that they are interacting with AI.")]
        [DataType(customDataType: "textArea")]
        public string Notice { get; set; }

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
        /// Gets or sets the attributes for the widget.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [ContentSection(Constants.ContentSectionTitles.Attributes, 0)]
        [DisplayName("Attributes for...")]
        [LengthDependsOn(null, "", "", ExtraRecords = "[{\"Name\": \"SitefinityAssistant\", \"Title\": \"SitefinityAssistant\"}]")]
        [DataType(customDataType: KnownFieldTypes.Attributes)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Must be able to set in property editor.")]
        public IDictionary<string, IList<AttributeModel>> Attributes { get; set; }
    }
}
