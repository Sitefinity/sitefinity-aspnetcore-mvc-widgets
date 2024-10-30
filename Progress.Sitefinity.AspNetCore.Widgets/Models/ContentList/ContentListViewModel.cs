using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Progress.Sitefinity.RestSdk.Dto;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.ContentList
{
    /// <summary>
    /// The view model for the content list widget.
    /// </summary>
    public class ContentListViewModel : ContentListCommonViewModel
    {
        /// <summary>
        /// Gets or sets the field mappings for the list view.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "By design")]
        public IList<FieldMapping> ListFieldMapping { get; set; }

        /// <inheritdoc />
        public override T GetFieldValue<T>(SdkItem item, string listFieldName)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            if (this.ListFieldMapping == null)
                return default(T);

            T value;
            var fieldMapping = this.ListFieldMapping.FirstOrDefault(fm => fm.FriendlyName == listFieldName);
            if (fieldMapping != null)
            {
                item.TryGetValue<T>(fieldMapping.Name, out value);
                return value;
            }

            return default(T);
        }

        /// <summary>
        /// Gets the first related media item.
        /// </summary>
        /// <param name="item">The SDK item.</param>
        /// <param name="listFieldName">The field name.</param>
        /// <returns>The first media item.</returns>
        public ImageDto GetFirstRelatedMedia(SdkItem item, string listFieldName)
        {
            try
            {
                var mediaList = this.GetFieldValue<List<ImageDto>>(item, listFieldName);

                return mediaList != null ? mediaList.FirstOrDefault() : null;
            }
            catch (JsonSerializationException)
            {
                var media = this.GetFieldValue<ImageDto>(item, listFieldName);

                return media;
            }
        }
    }
}
