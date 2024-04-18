using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Progress.Sitefinity.AspNetCore.Widgets.Models.NativeChat;
using static System.Collections.Specialized.BitVector32;

namespace Progress.Sitefinity.AspNetCore.Widgets.ViewComponents
{
    /// <summary>
    /// Test widget with different kind of restrictions for its properties.
    /// </summary>
    [SitefinityWidget(Title = "Native chat", EmptyIconText = "Select a chatbot", EmptyIcon = "pencil", Order = 1, Section = WidgetSection.Marketing)]
    [ViewComponent(Name = "SitefinityNativeChat")]
    public class NativeChatViewComponent : ViewComponent
    {
        private readonly INativeChatModel nativeChatModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="NativeChatViewComponent"/> class.
        /// </summary>
        /// <param name="nativeChatModel">The nativeChatModel parameter.</param>
        public NativeChatViewComponent(INativeChatModel nativeChatModel)
        {
            this.nativeChatModel = nativeChatModel;
        }

        /// <summary>
        /// Invokes the view.
        /// </summary>
        /// <param name="context">The context parameter.</param>
        /// <returns>The view.</returns>
        public async Task<IViewComponentResult> InvokeAsync(IViewComponentContext<NativeChatEntity> context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var viewModel = await this.nativeChatModel.GetViewModel(context.Entity);

            return this.View(viewModel);
        }
    }
}
