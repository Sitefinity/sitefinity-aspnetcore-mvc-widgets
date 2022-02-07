using System.Collections.Generic;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Button;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Common
{
    /// <summary>
    /// Interface for entities that store css position.
    /// </summary>
    public interface IHasPosition
    {
        /// <summary>
        /// Gets the position.
        /// </summary>
        public IDictionary<string, AlignmentWrapper> Position { get; }
    }
}
