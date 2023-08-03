using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Progress.Sitefinity.AspNetCore.Models;
using Progress.Sitefinity.AspNetCore.Models.Common;
using Progress.Sitefinity.AspNetCore.ViewComponents.AttributeConfigurator.Attributes;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Common;
using Progress.Sitefinity.AspNetCore.Widgets.Models.ContentList;
using Progress.Sitefinity.Renderer.Designers;
using Progress.Sitefinity.Renderer.Designers.Attributes;
using Progress.Sitefinity.Renderer.Entities.Content;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.DocumentList
{
    /// <summary>
    /// The entity for the Document list widget. Contains all of the data persited in the database.
    /// </summary>
    [SectionsOrder(Constants.ContentSectionTitles.SelectDocumentsToDisplay, Constants.ContentSectionTitles.ListSettings, Constants.ContentSectionTitles.SingleItemSettings, Constants.ContentSectionTitles.DisplaySettings, "", Constants.ContentSectionTitles.CustomCssClasses, Constants.ContentSectionTitles.LabelsAndMessages, Constants.ContentSectionTitles.MetadataFields, Constants.ContentSectionTitles.Attributes)]
    public class DocumentListEntity : ContentListEntityBase, IHasMargins<MarginStyle>
    {
        /// <inheritdoc />
        [ContentSection(Constants.ContentSectionTitles.SelectDocumentsToDisplay, 0)]
        [Content(Type = KnownContentTypes.Documents, IsFilterable = true)]
        public override MixedContentContext SelectedItems { get; set; }

        /// <inheritdoc />
        [ContentSection(Constants.ContentSectionTitles.DisplaySettings, 0)]
        public override string SfViewName { get; set; }

        /// <inheritdoc />
        [ContentSection(Constants.ContentSectionTitles.DisplaySettings, 1)]
        public override string SfDetailViewName { get; set; }

        /// <inheritdoc/>
        [ContentSection(Constants.ContentSectionTitles.DisplaySettings, 2)]
        [DisplayName("Margins")]
        [TableView("Document list")]
        public MarginStyle Margins { get; set; }

        /// <summary>
        /// Gets or sets the document view display mode.
        /// </summary>
        [DisplayName("Document view display mode")]
        [Description("[{\"Type\":1,\"Chunks\":[{\"Value\":\"Based on your selection the items will be\",\"Presentation\":[]},{\"Value\":\"displayed as follows:\",\"Presentation\":[2]}]},{\"Type\":1,\"Chunks\":[{\"Value\":\"Automatic\",\"Presentation\":[0]},{\"Value\":\"- handles detail item urls like\",\"Presentation\":[]},{\"Value\":\"page/2021/01/01/documents.\",\"Presentation\":[2]}]},{\"Type\":1,\"Chunks\":[{\"Value\":\"Master\",\"Presentation\":[0]},{\"Value\":\" - as a list that does not handle\",\"Presentation\":[]},{\"Value\":\"detail item urls.\",\"Presentation\":[2]},{\"Value\":\"E.g. page/2021/01/01/documents will throw 404.\",\"Presentation\":[2]}]},{\"Type\":1,\"Chunks\":[{\"Value\":\"Detail\",\"Presentation\":[0]},{\"Value\":\"- shows a selected item in detail\",\"Presentation\":[]},{\"Value\":\"mode only.\",\"Presentation\":[2]}]}]")]
        public override ContentViewDisplayMode ContentViewDisplayMode { get; set; }

        /// <inheritdoc />
        [DataType(KnownFieldTypes.ChipChoice)]
        public override LogicalOperator SelectionGroupLogicalOperator { get; set; }

        /// <inheritdoc />
        [DataType(customDataType: KnownFieldTypes.ChipChoice)]
        [Choice("[{\"Title\":\"Yes\",\"Name\":\"Yes\",\"Value\":\"True\",\"Icon\":null},{\"Title\":\"No\",\"Name\":\"No\",\"Value\":\"False\",\"Icon\":null}]")]
        public override bool DisableCanonicalUrlMetaTag { get; set; }

        /// <inheritdoc />
        [CssFieldMappings]
        public override IList<CssFieldMapping> CssClasses { get; set; }

        /// <summary>
        /// Gets or sets the download link label.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages, 2)]
        [DisplayName("Download link label")]
        [DefaultValue("Download")]
        public string DownloadLinkLabel { get; set; }

        /// <summary>
        /// Gets or sets the title column label.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages, 2)]
        [DisplayName("Title column label")]
        [DefaultValue("Title")]
        public string TitleColumnLabel { get; set; }

        /// <summary>
        /// Gets or sets the type column label.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages, 2)]
        [DisplayName("Type column label")]
        [DefaultValue("Type")]
        public string TypeColumnLabel { get; set; }

        /// <summary>
        /// Gets or sets the size column label.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages, 2)]
        [DisplayName("Size column label")]
        [DefaultValue("Size")]
        public string SizeColumnLabel { get; set; }

        /// <inheritdoc />
        [DefaultValue("website")]
        public override string OpenGraphType { get; set; }

        /// <inheritdoc />
        [LengthDependsOn(null, "", "", ExtraRecords = "[{\"Name\": \"Document list\", \"Title\": \"Document list\"}]")]
        public override IDictionary<string, IList<AttributeModel>> Attributes { get; set; }
    }
}
