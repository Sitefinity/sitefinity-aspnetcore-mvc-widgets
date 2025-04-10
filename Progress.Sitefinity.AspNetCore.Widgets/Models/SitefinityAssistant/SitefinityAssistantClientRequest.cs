using System.Collections.Generic;
using System.IO;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.SitefinityAssistant
{
    internal class SitefinityAssistantClientRequest
    {
        public string AssistantEndpoint { get; set; }

        public Dictionary<string, string> Headers { get; } = new Dictionary<string, string>();

        public Stream Body { get; set; }
    }
}
