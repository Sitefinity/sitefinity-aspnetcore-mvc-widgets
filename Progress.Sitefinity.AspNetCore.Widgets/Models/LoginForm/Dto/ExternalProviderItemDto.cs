using Newtonsoft.Json;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.LoginForm.Dto
{
    /// <summary>
    /// External provider item dto.
    /// </summary>
    public class ExternalProviderItemDto
    {
        /// <summary>
        /// Gets or sets the external provider item name.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the external provider item title.
        /// </summary>
        [JsonProperty("value")]
        public string Title { get; set; }
    }
}
