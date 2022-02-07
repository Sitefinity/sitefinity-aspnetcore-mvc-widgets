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
    public class ContentListEntity
    {
        /// <summary>
        /// Sets the choice field to use the ChoiceServiceUrlComponent type. It dynamically populates select options from a provided web service endpoint
        /// taking into consideration the current data item type context of the AdminApp application.
        /// </summary>
        internal const string DynamicChoicePerItemType = "dynamicChoicePerItemType";

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentListEntity"/> class.
        /// </summary>
        public ContentListEntity()
        {
            this.ListSettings = new ContentListSettings();
        }

        /// <summary>
        /// Gets or sets the selected items info.
        /// </summary>
        [ContentSection("Select content to display", 0)]
        [DisplayName("")]
        [Content]
        public MixedContentContext SelectedItems { get; set; }

        /// <summary>
        /// Gets or sets the view name.
        /// </summary>
        [ViewSelector(RegexFilter = "^(?!Details).*(?<!Details)$")]
        [ContentSection("Select content to display", 0)]
        [DisplayName("List template")]
        public string SfViewName { get; set; }

        /// <summary>
        /// Gets or sets the list settings.
        /// </summary>
        [ContentSection("List settings", 1)]
        [DisplayName("Number of list items")]
        public ContentListSettings ListSettings { get; set; }

        /// <summary>
        /// Gets or sets the field mappings for the list view.
        /// </summary>
        [ContentSection("Select content to display", 1)]
        [DisplayName("Field mapping")]
        [FieldMappings]
        [Description("Specify which fields from the content type you have selected to be displayed in the list.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "By design")]
        public IList<FieldMapping> ListFieldMapping { get; set; }

        /// <summary>
        /// Gets or sets the order by info.
        /// </summary>
        [ContentSection("List settings", 1)]
        [DisplayName("Sort items")]
        [DefaultValue("PublicationDate DESC")]
        [DataType(customDataType: DynamicChoicePerItemType)]
        [Choice(ServiceUrl = "/Default.Sorters()?frontend=True")]
        public string OrderBy { get; set; }

        /// <summary>
        /// Gets or sets the detail page selection mode.
        /// </summary>
        [ContentSection("Single item settings", 2)]
        [DisplayName("Open single item in...")]
        [DataType(customDataType: KnownFieldTypes.RadioChoice)]
        public DetailPageSelectionMode DetailPageMode { get; set; }

        /// <summary>
        /// Gets or sets the detail page info.
        /// </summary>
        [ContentSection("Single item settings", 2)]
        [DisplayName("")]
        [Content(Type = KnownContentTypes.Pages, AllowMultipleItemsSelection = false)]
        [ConditionalVisibility("{\"conditions\":[{\"fieldName\":\"DetailPageMode\",\"operator\":\"Equals\",\"value\":\"ExistingPage\"}],\"inline\":\"true\"}")]
        public MixedContentContext DetailPage { get; set; }

        /// <summary>
        /// Gets or sets the detail page info.
        /// </summary>
        [ContentSection("Single item settings", 2)]
        [DisplayName("Single item template")]
        [ViewSelector(RegexFilter = "^(Details)(.*)|.*(Details)$")]
        public string SfDetailViewName { get; set; }

        /// <summary>
        /// Gets or sets the content view display mode.
        /// </summary>
        [DisplayName("Content view display mode")]
        [Description("[{\"Type\":1,\"Chunks\":[{\"Value\":\"Based on your selection the items will be\",\"Presentation\":[]},{\"Value\":\"displayed as follows:\",\"Presentation\":[2]}]},{\"Type\":1,\"Chunks\":[{\"Value\":\"Automatic\",\"Presentation\":[0]},{\"Value\":\"- handles detail item urls like\",\"Presentation\":[]},{\"Value\":\"page/2021/01/01/news.\",\"Presentation\":[2]}]},{\"Type\":1,\"Chunks\":[{\"Value\":\"Master\",\"Presentation\":[0]},{\"Value\":\" - as a list that does not handle\",\"Presentation\":[]},{\"Value\":\"detail item urls.\",\"Presentation\":[2]},{\"Value\":\"E.g. page/2021/01/01/news will throw 404.\",\"Presentation\":[2]}]},{\"Type\":1,\"Chunks\":[{\"Value\":\"Detail\",\"Presentation\":[0]},{\"Value\":\"- shows a selected item in detail\",\"Presentation\":[]},{\"Value\":\"mode only.\",\"Presentation\":[2]}]}]")]
        [Category(PropertyCategory.Advanced)]
        public ContentViewDisplayMode ContentViewDisplayMode { get; set; }

        /// <summary>
        /// Gets or sets the logical operator for selection group.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [DisplayName("Selection group logical operator")]
        [DataType(KnownFieldTypes.RadioChoice)]
        [Description("Controls the logic of the filters - whether all conditions should be true (AND) or whether one of the conditions should be true (OR).")]
        public LogicalOperator SelectionGroupLogicalOperator { get; set; }

        /// <summary>
        /// Gets or sets the filter expression.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [DisplayName("Filter expression")]
        [Description("Custom filter expression added to already selected filters.")]
        public string FilterExpression { get; set; }

        /// <summary>
        /// Gets or sets the sort expression.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [DisplayName("Sort expression")]
        [DefaultValue("PublicationDate DESC")]
        [Description("Custom sort expression, used if default sorting is not suitable.")]
        public string SortExpression { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the canonical URL tag should be added to the page when the canonical meta tag should be added to the page.
        /// If the value is not set, the settings from SystemConfig -> ContentLocationsSettings -> DisableCanonicalURLs will be used.
        /// </summary>
        /// <value>The disable canonical URLs.</value>
        [Description("Disables the canonocal URL generation on widget level.")]
        [DisplayName("Disable canonical URL meta tag")]
        [Category(PropertyCategory.Advanced)]
        public bool DisableCanonicalUrlMetaTag { get; set; }

        /// <summary>
        /// Gets or sets paging modeup.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [DisplayName("Paging mode")]
        [DataType(KnownFieldTypes.RadioChoice)]
        [Description("Controls whether the paging works with URL segments or a query parameter.")]
        public PagerMode PagerMode { get; set; }

        /// <summary>
        /// Gets or sets the template for paging URL segment.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [DisplayName("Template for paging URL segments")]
        [DefaultValue(ContentPagerViewModel.PageNumberDefaultTemplate)]
        [FallbackToDefaultValueWhenEmpty]
        [Description("Template for the URL segments the widget's paging will work with. Use {{pageNumber}} for the current page number.")]
        [ConditionalVisibility("{\"conditions\":[{\"fieldName\":\"PagerMode\",\"operator\":\"Equals\",\"value\":\"URLSegments\"}]}")]
        public string PagerTemplate { get; set; }

        /// <summary>
        /// Gets or sets the template for paging query parameters.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [DisplayName("Template for paging query parameter")]
        [DefaultValue(ContentPagerViewModel.PageNumberDefaultQueryTemplate)]
        [FallbackToDefaultValueWhenEmpty]
        [Description("Template for the query parameter the widget's paging will work with.")]
        [ConditionalVisibility("{\"conditions\":[{\"fieldName\":\"PagerMode\",\"operator\":\"Equals\",\"value\":\"QueryParameter\"}]}")]
        public string PagerQueryTemplate { get; set; }

        /// <summary>
        /// Gets or sets the additional CSS classes.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [ContentSection("Custom CSS classes", 1)]
        [DisplayName("")]
        [CssFieldMappings(true)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "By design")]
        public IList<CssFieldMapping> CssClasses { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the parent list view should be hidden when child details is opened.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [ContentSection("Displaying hierarchical content", 2)]
        [DisplayName("Show parent list view on child details view")]
        [Description("Show or hide the parent list view of this widget when on the same page there is another widget displaying details view of a child item.")]
        [DefaultValue(true)]
        public bool ShowListViewOnChildDetailsView { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the parent list widget should render details when child details is opened.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [ContentSection("Displaying hierarchical content", 2)]
        [DisplayName("Show parent details view on child details view")]
        [Description("Show or hide the parent details view of this widget when on the same page there is another widget displaying details view of a child item.")]
        [DefaultValue(false)]
        public bool ShowDetailsViewOnChildDetailsView { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the child list widget should show all items, if parent filter is not resolved.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [ContentSection("Displaying hierarchical content", 2)]
        [DisplayName("Show child list view if no parent selected")]
        [Description("Show or hide the child list view of this widget when on the same page there is another widget displaying parent items and no parent item is selected to filter the child's list.")]
        [DefaultValue(false)]
        public bool ShowListViewOnEmptyParentFilter { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the widget would be taken in consideration when generating SEO for the page.
        /// </summary>
        [DisplayName("SEO enabled")]
        [Category(PropertyCategory.Advanced)]
        [ContentSection("Metadata fields")]
        [DefaultValue(true)]
        public bool SeoEnabled { get; set; }

        /// <summary>
        /// Gets or sets a value for the SEO title field mapping.
        /// </summary>
        [DisplayName("Meta title")]
        [Category(PropertyCategory.Advanced)]
        [ContentSection("Metadata fields")]
        public string MetaTitle { get; set; }

        /// <summary>
        /// Gets or sets a value for the SEO description field mapping.
        /// </summary>
        [DisplayName("Meta description")]
        [Category(PropertyCategory.Advanced)]
        [ContentSection("Metadata fields")]
        public string MetaDescription { get; set; }

        /// <summary>
        /// Gets or sets a value for the approach to generating the page title.
        /// </summary>
        [DisplayName("Page title mode")]
        [Category(PropertyCategory.Advanced)]
        [ContentSection("Metadata fields")]
        [DataType(KnownFieldTypes.Choices)]
        [DefaultValue(PageTitleMode.Replace)]
        [Description("[{\"Type\": 1,\"Chunks\": [{\"Value\": \"Setting Page title mode\",\"Presentation\": [0]},{\"Value\": \"Replace – page title is replaced by the\",\"Presentation\": [2]},{\"Value\": \"item's title.\",\"Presentation\": [2]},{\"Value\": \"Append – item title is appended to the\",\"Presentation\": [2]},{\"Value\": \"page title.\",\"Presentation\": [2]},{\"Value\": \"Hierarchy – page title will be built by the\",\"Presentation\": [2]},{\"Value\": \"item's title and its parent's title. Value is\",\"Presentation\": [2]},{\"Value\": \"relevant for the Forums widget only.\",\"Presentation\": [2]},{\"Value\": \"Do not set – page title will not be altered.\",\"Presentation\": []}]}]")]
        public PageTitleMode PageTitleMode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Open Graph meta properties are generated regarding the widget.
        /// </summary>
        [DisplayName("OpenGraph enabled")]
        [Category(PropertyCategory.Advanced)]
        [ContentSection("Metadata fields")]
        [DefaultValue(true)]
        public bool OpenGraphEnabled { get; set; }

        /// <summary>
        /// Gets or sets a value for the Open Graph title field mapping.
        /// </summary>
        [DisplayName("OpenGraph title")]
        [Category(PropertyCategory.Advanced)]
        [ContentSection("Metadata fields")]
        public string OpenGraphTitle { get; set; }

        /// <summary>
        /// Gets or sets a value for the Open Graph description field mapping.
        /// </summary>
        [DisplayName("OpenGraph description")]
        [Category(PropertyCategory.Advanced)]
        [ContentSection("Metadata fields")]
        public string OpenGraphDescription { get; set; }

        /// <summary>
        /// Gets or sets a value for the Open Graph image field mapping.
        /// </summary>
        [DisplayName("OpenGraph image")]
        [Category(PropertyCategory.Advanced)]
        [ContentSection("Metadata fields")]
        public string OpenGraphImage { get; set; }

        /// <summary>
        /// Gets or sets a value for the Open Graph video field mapping.
        /// </summary>
        [DisplayName("OpenGraph video")]
        [Category(PropertyCategory.Advanced)]
        [ContentSection("Metadata fields")]
        public string OpenGraphVideo { get; set; }

        /// <summary>
        /// Gets or sets a value for the Open Graph type value.
        /// </summary>
        [DisplayName("OpenGraph type")]
        [Category(PropertyCategory.Advanced)]
        [ContentSection("Metadata fields")]
        public string OpenGraphType { get; set; }

        /// <summary>
        /// Gets or sets the attributes for the content list.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [ContentSection(Constants.ContentSectionTitles.Attributes, 2)]
        [DisplayName("Attributes for...")]
        [DataType(customDataType: KnownFieldTypes.Attributes)]
        [LengthDependsOn(null, "", "", ExtraRecords = "[{\"Name\": \"ContentList\", \"Title\": \"Content list\"}]")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Must be able to set in property editor.")]
        public IDictionary<string, IList<AttributeModel>> Attributes { get; set; }
    }
}
