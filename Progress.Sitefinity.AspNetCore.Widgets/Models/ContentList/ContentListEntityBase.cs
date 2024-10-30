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
    /// The base entity for content list widgets.
    /// </summary>
    public class ContentListEntityBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContentListEntityBase"/> class.
        /// </summary>
        public ContentListEntityBase()
        {
            this.ListSettings = new ContentListSettings();
        }

        /// <summary>
        /// Gets or sets the selected items info.
        /// </summary>
        [DisplayName("")]
        public virtual MixedContentContext SelectedItems { get; set; }

        /// <summary>
        /// Gets or sets the list settings.
        /// </summary>
        [ContentSection(Constants.ContentSectionTitles.ListSettings, 0)]
        [DisplayName("Number of list items")]
        public ContentListSettings ListSettings { get; set; }

        /// <summary>
        /// Gets or sets the view name.
        /// </summary>
        [ViewSelector(RegexFilter = "^(?!Details).*(?<!Details)$")]
        [DisplayName("List template")]
        public virtual string SfViewName { get; set; }

        /// <summary>
        /// Gets or sets the order by info.
        /// </summary>
        [ContentSection(Constants.ContentSectionTitles.ListSettings, 1)]
        [DisplayName("Sort items")]
        [DefaultValue("PublicationDate DESC")]
        [DataType(customDataType: "dynamicChoicePerItemType")]
        [Choice(ServiceUrl = "/Default.Sorters()?frontend=True")]
        public string OrderBy { get; set; }

        /// <summary>
        /// Gets or sets the detail page selection mode.
        /// </summary>
        [ContentSection(Constants.ContentSectionTitles.SingleItemSettings, 0)]
        [DisplayName("Open single item in...")]
        [DataType(customDataType: KnownFieldTypes.RadioChoice)]
        public virtual DetailPageSelectionMode DetailPageMode { get; set; }

        /// <summary>
        /// Gets or sets the detail page info.
        /// </summary>
        [DisplayName("Single item template")]
        [ViewSelector(RegexFilter = "^(Details)(.*)|.*(Details)$")]
        public virtual string SfDetailViewName { get; set; }

        /// <summary>
        /// Gets or sets the detail page info.
        /// </summary>
        [ContentSection(Constants.ContentSectionTitles.SingleItemSettings, 1)]
        [DisplayName("")]
        [Content(Type = KnownContentTypes.Pages, AllowMultipleItemsSelection = false)]
        [ConditionalVisibility("{\"conditions\":[{\"fieldName\":\"DetailPageMode\",\"operator\":\"Equals\",\"value\":\"ExistingPage\"}],\"inline\":\"true\"}")]
        [Required(ErrorMessage = "Please select a details page.")]
        public MixedContentContext DetailPage { get; set; }

        /// <summary>
        /// Gets or sets the item view display mode.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [ContentSection("", 1)]
        [Description("[{\"Type\":1,\"Chunks\":[{\"Value\":\"Based on your selection the items will be\",\"Presentation\":[]},{\"Value\":\"displayed as follows:\",\"Presentation\":[2]}]},{\"Type\":1,\"Chunks\":[{\"Value\":\"Automatic\",\"Presentation\":[0]},{\"Value\":\"- handles detail item urls like\",\"Presentation\":[]},{\"Value\":\"page/2021/01/01/documents.\",\"Presentation\":[2]}]},{\"Type\":1,\"Chunks\":[{\"Value\":\"Master\",\"Presentation\":[0]},{\"Value\":\" - as a list that does not handle\",\"Presentation\":[]},{\"Value\":\"detail item urls.\",\"Presentation\":[2]},{\"Value\":\"E.g. page/2021/01/01/documents will throw 404.\",\"Presentation\":[2]}]},{\"Type\":1,\"Chunks\":[{\"Value\":\"Detail\",\"Presentation\":[0]},{\"Value\":\"- shows a selected item in detail\",\"Presentation\":[]},{\"Value\":\"mode only.\",\"Presentation\":[2]}]}]")]
        public virtual ContentViewDisplayMode ContentViewDisplayMode { get; set; }

        /// <summary>
        /// Gets or sets the logical operator for selection group.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [ContentSection("", 2)]
        [DisplayName("Selection group logical operator")]
        [Description("Controls the logic of the filters - whether all conditions should be true (AND) or whether one of the conditions should be true (OR).")]
        public virtual LogicalOperator SelectionGroupLogicalOperator { get; set; }

        /// <summary>
        /// Gets or sets the filter expression.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [ContentSection("", 3)]
        [DisplayName("Filter expression")]
        [Description("Custom filter expression added to already selected filters.")]
        public string FilterExpression { get; set; }

        /// <summary>
        /// Gets or sets the sort expression.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [ContentSection("", 4)]
        [DisplayName("Sort expression")]
        [DefaultValue("PublicationDate DESC")]
        [Description("Custom sort expression, used if default sorting is not suitable.")]
        public string SortExpression { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the canonical URL tag should be added to the page when the canonical meta tag should be added to the page.
        /// If the value is not set, the settings from SystemConfig -> ContentLocationsSettings -> DisableCanonicalURLs will be used.
        /// </summary>
        /// <value>The disable canonical URLs.</value>
        [Category(PropertyCategory.Advanced)]
        [ContentSection("", 6)]
        [Description("Disables the canonocal URL generation on widget level.")]
        [DisplayName("Disable canonical URL meta tag")]
        public virtual bool DisableCanonicalUrlMetaTag { get; set; }

        /// <summary>
        /// Gets or sets paging modeup.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [ContentSection("", 7)]
        [DisplayName("Paging mode")]
        [DataType(KnownFieldTypes.RadioChoice)]
        [Description("Controls whether the paging works with URL segments or a query parameter.")]
        public PagerMode PagerMode { get; set; }

        /// <summary>
        /// Gets or sets the template for paging URL segment.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [ContentSection("", 8)]
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
        [ContentSection("", 9)]
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
        [ContentSection(Constants.ContentSectionTitles.CustomCssClasses)]
        [DisplayName("")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "By design")]
        public virtual IList<CssFieldMapping> CssClasses { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the widget would be taken in consideration when generating SEO for the page.
        /// </summary>
        [DisplayName("SEO enabled")]
        [Category(PropertyCategory.Advanced)]
        [ContentSection(Constants.ContentSectionTitles.MetadataFields, 0)]
        [DefaultValue(true)]
        [DataType(customDataType: KnownFieldTypes.ChipChoice)]
        [Choice("[{\"Title\":\"Yes\",\"Name\":\"Yes\",\"Value\":\"True\",\"Icon\":null},{\"Title\":\"No\",\"Name\":\"No\",\"Value\":\"False\",\"Icon\":null}]")]
        public bool SeoEnabled { get; set; }

        /// <summary>
        /// Gets or sets a value for the SEO title field mapping.
        /// </summary>
        [DisplayName("Meta title")]
        [Category(PropertyCategory.Advanced)]
        [ContentSection(Constants.ContentSectionTitles.MetadataFields, 1)]
        public string MetaTitle { get; set; }

        /// <summary>
        /// Gets or sets a value for the SEO description field mapping.
        /// </summary>
        [DisplayName("Meta description")]
        [Category(PropertyCategory.Advanced)]
        [ContentSection(Constants.ContentSectionTitles.MetadataFields, 2)]
        public string MetaDescription { get; set; }

        /// <summary>
        /// Gets or sets a value for the approach to generating the page title.
        /// </summary>
        [DisplayName("Page title mode")]
        [Category(PropertyCategory.Advanced)]
        [ContentSection(Constants.ContentSectionTitles.MetadataFields, 3)]
        [DataType(KnownFieldTypes.Choices)]
        [DefaultValue(PageTitleMode.Replace)]
        [Description("[{\"Type\": 1,\"Chunks\": [{\"Value\": \"Setting Page title mode\",\"Presentation\": [0]},{\"Value\": \"Replace – page title is replaced by the\",\"Presentation\": [2]},{\"Value\": \"item's title.\",\"Presentation\": [2]},{\"Value\": \"Append – item title is appended to the\",\"Presentation\": [2]},{\"Value\": \"page title.\",\"Presentation\": [2]},{\"Value\": \"Hierarchy – page title will be built by the\",\"Presentation\": [2]},{\"Value\": \"item's title and its parent's title. Value is\",\"Presentation\": [2]},{\"Value\": \"relevant for the Forums widget only.\",\"Presentation\": [2]},{\"Value\": \"Do not set – page title will not be altered.\",\"Presentation\": []}]}]")]
        public PageTitleMode PageTitleMode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Open Graph meta properties are generated regarding the widget.
        /// </summary>
        [DisplayName("OpenGraph enabled")]
        [Category(PropertyCategory.Advanced)]
        [ContentSection(Constants.ContentSectionTitles.MetadataFields, 4)]
        [DefaultValue(true)]
        [DataType(customDataType: KnownFieldTypes.ChipChoice)]
        [Choice("[{\"Title\":\"Yes\",\"Name\":\"Yes\",\"Value\":\"True\",\"Icon\":null},{\"Title\":\"No\",\"Name\":\"No\",\"Value\":\"False\",\"Icon\":null}]")]
        public bool OpenGraphEnabled { get; set; }

        /// <summary>
        /// Gets or sets a value for the Open Graph title field mapping.
        /// </summary>
        [DisplayName("OpenGraph title")]
        [Category(PropertyCategory.Advanced)]
        [ContentSection(Constants.ContentSectionTitles.MetadataFields, 5)]
        public string OpenGraphTitle { get; set; }

        /// <summary>
        /// Gets or sets a value for the Open Graph description field mapping.
        /// </summary>
        [DisplayName("OpenGraph description")]
        [Category(PropertyCategory.Advanced)]
        [ContentSection(Constants.ContentSectionTitles.MetadataFields, 6)]
        public string OpenGraphDescription { get; set; }

        /// <summary>
        /// Gets or sets a value for the Open Graph image field mapping.
        /// </summary>
        [DisplayName("OpenGraph image")]
        [Category(PropertyCategory.Advanced)]
        [ContentSection(Constants.ContentSectionTitles.MetadataFields, 7)]
        public string OpenGraphImage { get; set; }

        /// <summary>
        /// Gets or sets a value for the Open Graph video field mapping.
        /// </summary>
        [DisplayName("OpenGraph video")]
        [Category(PropertyCategory.Advanced)]
        [ContentSection(Constants.ContentSectionTitles.MetadataFields, 8)]
        public string OpenGraphVideo { get; set; }

        /// <summary>
        /// Gets or sets a value for the Open Graph type value.
        /// </summary>
        [DisplayName("OpenGraph type")]
        [Category(PropertyCategory.Advanced)]
        [ContentSection(Constants.ContentSectionTitles.MetadataFields, 9)]
        public virtual string OpenGraphType { get; set; }

        /// <summary>
        /// Gets or sets the attributes for the list.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [ContentSection(Constants.ContentSectionTitles.Attributes)]
        [DisplayName("Attributes for...")]
        [DataType(customDataType: KnownFieldTypes.Attributes)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Must be able to set in property editor.")]
        public virtual IDictionary<string, IList<AttributeModel>> Attributes { get; set; }
    }
}
