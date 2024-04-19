using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Progress.Sitefinity.AspNetCore.Models;
using Progress.Sitefinity.AspNetCore.ViewComponents.AttributeConfigurator.Attributes;
using Progress.Sitefinity.AspNetCore.Widgets.Models.ContentPager;
using Progress.Sitefinity.Renderer.Designers;
using Progress.Sitefinity.Renderer.Designers.Attributes;
using Progress.Sitefinity.Renderer.Entities.Content;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.ContentList
{
    /// <summary>
    /// The entity for the Content list widget. Contains all of the data persited in the database.
    /// </summary>
    [SectionsOrder(Constants.ContentSectionTitles.SelectContentToDisplay, Constants.ContentSectionTitles.ListSettings, Constants.ContentSectionTitles.SingleItemSettings, "", Constants.ContentSectionTitles.CustomCssClasses, Constants.ContentSectionTitles.DisplayingHierarchicalContent, Constants.ContentSectionTitles.MetadataFields, Constants.ContentSectionTitles.Attributes)]
    public class ContentListEntity : ContentListEntityBase
    {
        /// <inheritdoc />
        [ContentSection(Constants.ContentSectionTitles.SelectContentToDisplay, 0)]
        [Content]
        public override MixedContentContext SelectedItems { get; set; }

        /// <inheritdoc />
        [ContentSection(Constants.ContentSectionTitles.SelectContentToDisplay, 1)]
        public override string SfViewName { get; set; }

        /// <summary>
        /// Gets or sets the field mappings for the list view.
        /// </summary>
        [ContentSection(Constants.ContentSectionTitles.SelectContentToDisplay, 2)]
        [DisplayName("Field mapping")]
        [FieldMappings]
        [Description("Specify which fields from the content type you have selected to be displayed in the list.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "By design")]
        public IList<FieldMapping> ListFieldMapping { get; set; }

        /// <inheritdoc />
        [ContentSection(Constants.ContentSectionTitles.SingleItemSettings, 0)]
        public override DetailPageSelectionMode DetailPageMode { get; set; }

        /// <inheritdoc />
        [ContentSection(Constants.ContentSectionTitles.SingleItemSettings, 1)]
        public override string SfDetailViewName { get; set; }

        /// <summary>
        /// Gets or sets the content view display mode.
        /// </summary>
        [DisplayName("Content view display mode")]
        [Description("[{\"Type\":1,\"Chunks\":[{\"Value\":\"Based on your selection the items will be\",\"Presentation\":[]},{\"Value\":\"displayed as follows:\",\"Presentation\":[2]}]},{\"Type\":1,\"Chunks\":[{\"Value\":\"Automatic\",\"Presentation\":[0]},{\"Value\":\"- handles detail item urls like\",\"Presentation\":[]},{\"Value\":\"page/2021/01/01/news.\",\"Presentation\":[2]}]},{\"Type\":1,\"Chunks\":[{\"Value\":\"Master\",\"Presentation\":[0]},{\"Value\":\" - as a list that does not handle\",\"Presentation\":[]},{\"Value\":\"detail item urls.\",\"Presentation\":[2]},{\"Value\":\"E.g. page/2021/01/01/news will throw 404.\",\"Presentation\":[2]}]},{\"Type\":1,\"Chunks\":[{\"Value\":\"Detail\",\"Presentation\":[0]},{\"Value\":\"- shows a selected item in detail\",\"Presentation\":[]},{\"Value\":\"mode only.\",\"Presentation\":[2]}]}]")]
        public override ContentViewDisplayMode ContentViewDisplayMode { get; set; }

        /// <inheritdoc />
        [DataType(KnownFieldTypes.RadioChoice)]
        public override LogicalOperator SelectionGroupLogicalOperator { get; set; }

        /// <summary>
        /// Gets or sets the list items select expression.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [ContentSection("", 5)]
        [DisplayName("Select expression")]
        [DefaultValue("*")]
        [Description("Custom expression for selecting the fields of the content type. By default '*' (asterisk) selects all. Use ';' (semicolon) when listing specific fields. Example: Id; Title; Thumbnail(Id, Title)")]
        public string SelectExpression { get; set; }

        /// <summary>
        /// Gets or sets the detail item select expression.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [ContentSection("", 6)]
        [DisplayName("Detail item select expression")]
        [DefaultValue("*")]
        [Description("Custom expression for selecting the fields of the content type. By default '*' (asterisk) selects all. Use ';' (semicolon) when listing specific fields. Example: Id; Title; Thumbnail(Id, Title)")]
        public string DetailItemSelectExpression { get; set; }

        /// <inheritdoc />
        [CssFieldMappings(true)]
        public override IList<CssFieldMapping> CssClasses { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the parent list view should be hidden when child details is opened.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [ContentSection(Constants.ContentSectionTitles.DisplayingHierarchicalContent, 2)]
        [DisplayName("Show parent list view on child details view")]
        [Description("Show or hide the parent list view of this widget when on the same page there is another widget displaying details view of a child item.")]
        [DefaultValue(true)]
        public bool ShowListViewOnChildDetailsView { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the parent list widget should render details when child details is opened.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [ContentSection(Constants.ContentSectionTitles.DisplayingHierarchicalContent, 2)]
        [DisplayName("Show parent details view on child details view")]
        [Description("Show or hide the parent details view of this widget when on the same page there is another widget displaying details view of a child item.")]
        [DefaultValue(false)]
        public bool ShowDetailsViewOnChildDetailsView { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the child list widget should show all items, if parent filter is not resolved.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [ContentSection(Constants.ContentSectionTitles.DisplayingHierarchicalContent, 2)]
        [DisplayName("Show child list view if no parent selected")]
        [Description("Show or hide the child list view of this widget when on the same page there is another widget displaying parent items and no parent item is selected to filter the child's list.")]
        [DefaultValue(false)]
        public bool ShowListViewOnEmptyParentFilter { get; set; }

        /// <inheritdoc />
        [DefaultValue("article")]
        public override string OpenGraphType { get; set; }

        /// <inheritdoc />
        [LengthDependsOn(null, "", "", ExtraRecords = "[{\"Name\": \"ContentList\", \"Title\": \"Content list\"}]")]
        public override IDictionary<string, IList<AttributeModel>> Attributes { get; set; }
    }
}
