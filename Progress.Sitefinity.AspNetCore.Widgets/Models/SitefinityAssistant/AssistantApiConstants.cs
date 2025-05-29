namespace Progress.Sitefinity.AspNetCore.Widgets.Models.SitefinityAssistant
{
    internal static class AssistantApiConstants
    {
        public const string AssistantThreadHeaderKey = "thread-id";
        public const string AssistantApiKeyHeaderKey = "assistant-api-key";
        public const string AssistantCustomHeaderKeyPrefix = "sf-assistant-";
        public const string InitAssistantThreadEndpoint = "init";
        public const string ChatEndpoint = "chat";
        public const string SitefinityGetAssistantsFunctionName = "Default.GetAiAssistants()";
        public const string VersionInfoEndpoint = "Version";
    }
}
