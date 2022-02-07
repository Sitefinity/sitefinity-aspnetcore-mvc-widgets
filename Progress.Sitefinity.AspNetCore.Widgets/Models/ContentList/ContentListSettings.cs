using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Progress.Sitefinity.Renderer.Designers.Attributes;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.ContentList
{
    /// <summary>
    /// Represents the settings for displaying lists.
    /// </summary>
    [MappedType(DataType = "listSettings")]
    [DataContract]
    public class ContentListSettings
    {
        /// <summary>
        /// Gets or sets a value indicating whether to divide items in the list.
        /// </summary>
        /// <value>
        /// The display mode.
        /// </value>
        [DataMember]
        public ListDisplayMode DisplayMode { get; set; }

        /// <summary>
        /// Gets or sets the number of items per page.
        /// </summary>
        [DataMember]
        [DefaultValue(20)]
        [Range(1, 100)]
        public int ItemsPerPage { get; set; }

        /// <summary>
        /// Gets or sets the number of items limited for display.
        /// </summary>
        [DataMember]
        [DefaultValue(20)]
        [Range(1, 100)]
        public int LimitItemsCount { get; set; }
    }
}
