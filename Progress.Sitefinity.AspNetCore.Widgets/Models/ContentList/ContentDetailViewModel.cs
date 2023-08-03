using System;
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

        /// <summary>
        /// Gets or sets the styles.
        /// </summary>
        public string WrapperCssClass { get; set; }

        /// <summary>
        /// Gets or sets the download link label text.
        /// </summary>
        public string DownloadLinkLabel { get; set; }

        /// <summary>
        /// Gets the field value.
        /// </summary>
        /// <typeparam name="T">The value type.</typeparam>
        /// <param name="fieldName">The field name.</param>
        /// <returns>The field value.</returns>
        public virtual T GetFieldValue<T>(string fieldName)
        {
            T value;
            if (fieldName != null)
            {
                this.Item.TryGetValue<T>(fieldName, out value);
                return value;
            }

            return default(T);
        }

        /// <summary>
        /// Gets the file extension.
        /// </summary>
        /// <returns>The extension.</returns>
        public string GetExtension()
        {
            string extension = this.GetFieldValue<string>("Extension");

            if (!string.IsNullOrEmpty(extension))
            {
                return extension.Replace(".", string.Empty, StringComparison.InvariantCulture);
            }

            return extension;
        }

        /// <summary>
        /// Gets the file size.
        /// </summary>
        /// <returns>Humanly readible document size.</returns>
        public string GetFileSize()
        {
            int size = this.GetFieldValue<int>("TotalSize");
            if (size < 1024)
            {
                return $"{size} B";
            }

            var sizeKB = Math.Ceiling((double)size / 1024);
            return $"{sizeKB} KB";
        }
    }
}
