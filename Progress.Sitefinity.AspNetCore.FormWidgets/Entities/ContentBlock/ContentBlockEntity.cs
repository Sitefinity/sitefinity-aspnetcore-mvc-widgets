using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Progress.Sitefinity.Renderer.Designers;
using Progress.Sitefinity.Renderer.Designers.Attributes;

namespace Progress.Sitefinity.AspNetCore.FormWidgets.Models.ContentBlock
{
    /// <summary>
    /// The entity for the Content block widget. Contains all of the data persited in the database.
    /// </summary>
    public class ContentBlockEntity
    {
        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        [DataType(customDataType: KnownFieldTypes.Html)]
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the CSS class of the wrapper.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [DisplayName("CSS class")]
        public string WrapperCssClass { get; set; }

        /// <summary>
        /// Gets or sets the tag name.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [DisplayName("Tag name")]
        [DefaultValue("div")]
        [Description("Up to twenty characters in the range a-z are allowed")]
        [RegularExpression(@"^[a-z]{1,20}$", ErrorMessage = "Up to twenty characters in the range a-z are allowed.")]
        public string TagName { get; set; }
    }
}
