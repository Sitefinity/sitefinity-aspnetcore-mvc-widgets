using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Progress.Sitefinity.AspNetCore.Models;
using Progress.Sitefinity.AspNetCore.Models.Common;
using Progress.Sitefinity.AspNetCore.ViewComponents.AttributeConfigurator.Attributes;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Common;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Content;
using Progress.Sitefinity.Renderer.Designers;
using Progress.Sitefinity.Renderer.Designers.Attributes;
using Progress.Sitefinity.Renderer.Models;
using Progress.Sitefinity.RestSdk.Dto;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Image
{
    /// <summary>
    /// The entity for the Image widget. Contains all of the data persited in the database.
    /// </summary>
    public class ImageEntity : ContentEntityBase, IHasMargins<MarginStyle>
    {
        /// <summary>
        /// Gets or sets the image.
        /// </summary>
        [MediaItem("images", false, true)]
        [DataType(customDataType: "media")]
        [DisplayName("")]
        public override SdkItem Item { get; set; }

        /// <summary>
        /// Gets or sets the image title for the page.
        /// </summary>
        [DescriptionExtended(InlineDescription = "(for current page)")]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the image alternative text for the page.
        /// </summary>
        [DisplayName("Alternative text")]
        [DescriptionExtended(InlineDescription = "(for current page)")]
        public string AlternativeText { get; set; }

        /// <summary>
        /// Gets or sets the click action for the image.
        /// </summary>
        [DisplayName("When image is clicked...")]
        public ImageClickAction ClickAction { get; set; }

        /// <summary>
        /// Gets or sets the action link.
        /// </summary>
        [DisplayName("Link to...")]
        [ConditionalVisibility("{\"conditions\":[{\"fieldName\":\"ClickAction\",\"operator\":\"Equals\",\"value\":\"OpenLink\"}]}")]
        [Required(ErrorMessage = "Please select a link")]
        [DataType(customDataType: "linkSelector")]
        public LinkModel ActionLink { get; set; }

        /// <summary>
        /// Gets or sets the image size.
        /// </summary>
        [ContentSection(Constants.ContentSectionTitles.DisplaySettings, 1)]
        [DisplayName("Image size")]
        public ImageDisplayMode ImageSize { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether an image should fit in the container.
        /// </summary>
        [DisplayName("Fit to container")]
        [ContentSection(Constants.ContentSectionTitles.DisplaySettings, 1)]
        [DefaultValue(true)]
        [DataType(customDataType: KnownFieldTypes.CheckBox)]
        public bool FitToContainer { get; set; }

        /// <summary>
        /// Gets or sets the custom size.
        /// </summary>
        [DisplayName("")]
        [ContentSection(Constants.ContentSectionTitles.DisplaySettings, 1)]
        [ConditionalVisibility("{\"conditions\":[{\"fieldName\":\"ImageSize\",\"operator\":\"Equals\",\"value\":\"CustomSize\"}]}")]
        [DataType(customDataType: "customSize")]
        public CustomSizeModel CustomSize { get; set; }

        /// <summary>
        /// Gets or sets the custom size.
        /// </summary>
        [DisplayName("Thumbnail")]
        [ContentSection(Constants.ContentSectionTitles.DisplaySettings, 1)]
        [ConditionalVisibility("{\"conditions\":[{\"fieldName\":\"ImageSize\",\"operator\":\"Equals\",\"value\":\"Thumbnail\"}]}")]
        [DataType(customDataType: "thumbnail")]
        public ThumbnailEntity Thumnail { get; set; }

        /// <summary>
        /// Gets or sets the view name.
        /// </summary>
        [DisplayName("Image template")]
        [ContentSection(Constants.ContentSectionTitles.DisplaySettings, 1)]
        [ViewSelector]
        [DefaultValue("Image")]
        public string ViewName { get; set; }

        /// <summary>
        /// Gets or sets the margins.
        /// </summary>
        [ContentSection(Constants.ContentSectionTitles.DisplaySettings, 1)]
        [DisplayName("Margins")]
        [TableView("Image")]
        public MarginStyle Margins { get; set; }

        /// <summary>
        /// Gets or sets the attributes for the image.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [ContentSection(Constants.ContentSectionTitles.Attributes)]
        [DisplayName("Attributes for...")]
        [LengthDependsOn(null, "", "", ExtraRecords = "[{\"Name\": \"Image\", \"Title\": \"Image\"}]")]
        [DataType(customDataType: KnownFieldTypes.Attributes)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Must be able to set in property editor.")]
        public IDictionary<string, IList<AttributeModel>> Attributes { get; set; }
    }
}
