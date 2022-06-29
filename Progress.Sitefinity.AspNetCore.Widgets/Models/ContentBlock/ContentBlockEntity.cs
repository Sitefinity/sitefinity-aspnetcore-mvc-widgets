using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Progress.Sitefinity.AspNetCore.Models;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Common;
using Progress.Sitefinity.Renderer.Designers;
using Progress.Sitefinity.Renderer.Designers.Attributes;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.ContentBlock
{
    /// <summary>
    /// The entity for the Content block widget. Contains all of the data persited in the database.
    /// </summary>
    public class ContentBlockEntity : IHasMargins<MarginVerticalStyle>
    {
        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        [DataType(customDataType: KnownFieldTypes.Html)]
        [ContentContainer]
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the CSS class of the wrapper.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [DisplayName("CSS class")]
        public string WrapperCssClass { get; set; }

        /// <summary>
        ///     Gets or sets the name of the provider.
        /// </summary>
        /// <value>The name of the provider.</value>
        public string ProviderName { get; set; }

        /// <summary>
        ///     Gets or sets the ID of the ContentBlockItem if the Content is shared across multiple controls.
        /// </summary>
        public Guid SharedContentID { get; set; }

        /// <summary>
        /// Gets or sets the tag name.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [DisplayName("Tag name")]
        [DefaultValue("div")]
        public string TagName { get; set; }

        /// <summary>
        /// Gets or sets the margins.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [ContentSection(Constants.ContentSectionTitles.DisplaySettings, 1)]
        [DisplayName("Margins")]
        [TableView("Content")]
        public MarginVerticalStyle Margins { get; set; }

        /// <summary>
        /// Gets or sets the attributes for the content block.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [ContentSection(Constants.ContentSectionTitles.Attributes, 1)]
        [DisplayName("Attributes for...")]
        [LengthDependsOn(null, "", "", ExtraRecords = "[{\"Name\": \"ContentBlock\", \"Title\": \"Content block\"}]")]
        [DataType(customDataType: KnownFieldTypes.Attributes)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Must be able to set in property editor.")]
        public IDictionary<string, IList<AttributeModel>> Attributes { get; set; }
    }
}
