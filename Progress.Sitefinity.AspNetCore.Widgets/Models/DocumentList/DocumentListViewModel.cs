using System;
using Progress.Sitefinity.AspNetCore.Widgets.Models.ContentList;
using Progress.Sitefinity.RestSdk.Dto;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.DocumentList
{
    /// <summary>
    /// The view model for the document list widget.
    /// </summary>
    public class DocumentListViewModel : ContentListCommonViewModel
    {
        /// <summary>
        /// Gets or sets the view name.
        /// </summary>
        public string ViewName { get; set; }

        /// <summary>
        /// Gets or sets the styles.
        /// </summary>
        public string WrapperCssClass { get; set; }

        /// <summary>
        /// Gets or sets the download link label text.
        /// </summary>
        public string DownloadLinkLabel { get; set; }

        /// <summary>
        /// Gets or sets the size column text.
        /// </summary>
        public string SizeColumnLabel { get; set; }

        /// <summary>
        /// Gets or sets the title column text.
        /// </summary>
        public string TitleColumnLabel { get; set; }

        /// <summary>
        /// Gets or sets the type column text.
        /// </summary>
        public string TypeColumnLabel { get; set; }

        /// <summary>
        /// Gets the file extension css class.
        /// </summary>
        /// <param name="extension">File extension.</param>
        /// <returns>File extension css class.</returns>
        public static string GetFileExtensionCssClass(string extension)
        {
            string color;

            switch (extension)
            {
                case "xlsx":
                case "xls":
                    color = "--bs-green";
                    break;
                case "doc":
                case "docx":
                    color = "--bs-blue";
                    break;
                case "ppt":
                case "pptx":
                    color = "--bs-orange";
                    break;
                case "pdf":
                    color = "--bs-red";
                    break;
                case "zip":
                case "rar":
                    color = "--bs-purple";
                    break;
                default:
                    color = "--bs-gray";
                    break;
            }

            return color;
        }

        /// <summary>
        /// Gets the document extension.
        /// </summary>
        /// <param name="item">The document item.</param>
        /// <returns>The extension.</returns>
        public string GetExtension(SdkItem item)
        {
            string extension = this.GetFieldValue<string>(item, "Extension");

            if (!string.IsNullOrEmpty(extension))
            {
                return extension.Replace(".", string.Empty, StringComparison.InvariantCulture);
            }

            return extension;
        }

        /// <summary>
        /// Gets the file size.
        /// </summary>
        /// <param name="item">The document item.</param>
        /// <returns>Humanly readible document size.</returns>
        public string GetFileSize(SdkItem item)
        {
            int size = this.GetFieldValue<int>(item, "TotalSize");
            if (size < 1024)
            {
                return $"{size} B";
            }

            var sizeKB = Math.Ceiling((double)size / 1024);
            return $"{sizeKB} KB";
        }
    }
}
