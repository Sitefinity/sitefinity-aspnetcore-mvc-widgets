using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.NativeChat
{
    /// <summary>
    /// The INativeChatModel interface.
    /// </summary>
    public interface INativeChatModel
    {
        /// <summary>
        /// Gets the ViewModel.
        /// </summary>
        /// <param name="entity">The entity parameter.</param>
        /// <returns>The NativeChatViewModel.</returns>
        Task<NativeChatViewModel> GetViewModel(NativeChatEntity entity);
    }
}
