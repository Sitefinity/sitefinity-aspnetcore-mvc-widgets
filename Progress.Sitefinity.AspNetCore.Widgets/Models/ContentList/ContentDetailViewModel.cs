using Progress.Sitefinity.RestSdk.Dto;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.ContentList
{
    /// <summary>
    /// The view model for the content list widget.
    /// </summary>
    public class ContentDetailViewModel : ContentListViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContentDetailViewModel"/> class.
        /// </summary>
        /// <param name="sdkItem">The sdk item.</param>
        public ContentDetailViewModel(SdkItem sdkItem)
        {
            this.Item = sdkItem;
        }

        /// <summary>
        /// Gets the item.
        /// </summary>
        public SdkItem Item { get; private set; }

        /// <summary>
        /// Gets or sets the additional CSS class.
        /// </summary>
        public string CssClass { get; set; }
    }
}
