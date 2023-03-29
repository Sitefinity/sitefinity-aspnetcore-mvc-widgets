using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.NativeChat.Dto
{
    /// <summary>
    /// The NativeChatChannelDto.
    /// </summary>
    internal class NativeChatChannelDto
    {
        /// <summary>
        /// Gets or sets the Id property.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the ProviderName property.
        /// </summary>
        public string ProviderName { get; set; }

        /// <summary>
        /// Gets or sets the Config property.
        /// </summary>
        public NativeChatChannelConfig Config { get; set; }
    }
}
