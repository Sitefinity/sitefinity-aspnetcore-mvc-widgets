using System.Collections.Generic;
using System.Threading.Tasks;
using Progress.Sitefinity.Renderer.Designers.Dto;

namespace Progress.Sitefinity.AspNetCore.Widgets.Attributes
{
    /// <summary>
    /// Represents provider of choices that are fetched asynchronously from an external service.
    /// </summary>
    internal interface IExternalChoicesProvider
    {
        /// <summary>
        /// Gets the unique provider name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Fetches the choises.
        /// </summary>
        /// <returns>The choices used for the Sitefinity designer UI.</returns>
        public Task<IEnumerable<ChoiceValueDto>> FetchChoicesAsync();
    }
}
