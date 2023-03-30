using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Progress.Sitefinity.AspNetCore.Models;
using Progress.Sitefinity.AspNetCore.Models.Common;
using Progress.Sitefinity.AspNetCore.ViewComponents.AttributeConfigurator.Attributes;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Common;
using Progress.Sitefinity.Renderer.Designers;
using Progress.Sitefinity.Renderer.Designers.Attributes;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Facets
{
    /// <summary>
    /// Entity for the facets widget.
    /// </summary>
    public class FacetsEntity : IHasMargins<MarginStyle>
    {
        /// <summary>
        /// Gets or sets the search indexes.
        /// </summary>
        [ContentSection("Search facets setup", 0)]
        [DisplayName("Search index")]
        [Placeholder("Select search index")]
        [Required(ErrorMessage = "Select search index")]
        [DataType(customDataType: KnownFieldTypes.Choices)]
        [Choice(ServiceUrl = "Default.GetFacetableIndexes", ServiceWarningMessage = "No search index with facetable fields has been created yet. To manage search indexes, go to Administration > Search indexes, or contact your administrator for assistance.")]
        [Description("[{\"Type\":1,\"Chunks\":[{\"Value\":\"To display facetable fields on your site,\",\"Presentation\":[]},{\"Value\":\"select the same search index as the one\",\"Presentation\":[]},{\"Value\":\"selected in the Search box widget.\",\"Presentation\":[]}]}]")]
        public string IndexCatalogue { get; set; }

        /// <summary>
        /// Gets or sets the additional fileds for facet widget.
        /// </summary>
        [ContentSection("Search facets setup", 0)]
        [TableView(Reorderable = true, Selectable = false, MultipleSelect = false)]
        [DisplayName("Set facetable fields")]
        [ConditionalVisibility("{\"conditions\":[{\"fieldName\":\"IndexCatalogue\",\"operator\":\"NotEquals\",\"value\":null }]}")]
        public IList<FacetField> SelectedFacets { get; set; }

        /// <summary>
        /// Gets or sets the sort type.
        /// </summary>
        [ContentSection("Search facets setup", 0)]
        [DisplayName("Sort fields")]
        [DataType(customDataType: KnownFieldTypes.Choices)]
        [ConditionalVisibility("{\"conditions\":[{\"fieldName\":\"IndexCatalogue\",\"operator\":\"NotEquals\",\"value\":null }]}")]
        [Choice(Choices = "[{\"Title\":\"As set manually\",\"Name\":\"0\",\"Value\":0,\"Icon\":\"ban\"},{\"Title\":\"Alphabetically\",\"Name\":\"2\",\"Value\":2,\"Icon\":null}]")]
        public string SortType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the facets display item count.
        /// </summary>
        [ContentSection("Search facets setup", 1)]
        [DisplayName("Display item count")]
        [DefaultValue(true)]
        [DataType(customDataType: KnownFieldTypes.ChipChoice)]
        [Choice("[{\"Title\":\"Yes\",\"Name\":\"Yes\",\"Value\":\"True\",\"Icon\":null},{\"Title\":\"No\",\"Name\":\"No\",\"Value\":\"False\",\"Icon\":null}]")]
        [ConditionalVisibility("{\"conditions\":[{\"fieldName\":\"IndexCatalogue\",\"operator\":\"NotEquals\",\"value\":null }]}")]
        public bool DisplayItemCount { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the show more/less option is active or not.
        /// </summary>
        [ContentSection("Search facets setup", 1)]
        [DisplayName("Collapse large facet lists")]
        [DefaultValue(true)]
        [DataType(customDataType: KnownFieldTypes.ChipChoice)]
        [Choice("[{\"Title\":\"Yes\",\"Name\":\"Yes\",\"Value\":\"True\",\"Icon\":null},{\"Title\":\"No\",\"Name\":\"No\",\"Value\":\"False\",\"Icon\":null}]")]
        [ConditionalVisibility("{\"conditions\":[{\"fieldName\":\"IndexCatalogue\",\"operator\":\"NotEquals\",\"value\":null }]}")]
        [Description("[{\"Type\":1,\"Chunks\":[{\"Value\":\"Specifies whether to collapse facet lists on\",\"Presentation\":[]}, {\"Value\":\"your site with more than 10 entries. If 'No'\",\"Presentation\":[2]}, {\"Value\":\"is selected, all facets are displayed.\",\"Presentation\":[2]}]}]")]
        public bool IsShowMoreLessButtonActive { get; set; }

        /// <summary>
        /// Gets or sets the view name.
        /// </summary>
        [ContentSection(Constants.ContentSectionTitles.DisplaySettings, 0)]
        [ViewSelector]
        [DisplayName("Template")]
        public string SfViewName { get; set; }

        /// <summary>
        /// Gets or sets the margins.
        /// </summary>
        [ContentSection(Constants.ContentSectionTitles.DisplaySettings, 1)]
        [DisplayName("Margins")]
        [TableView("Search facets")]
        public MarginStyle Margins { get; set; }

        /// <inheritdoc />
        [Category(PropertyCategory.Advanced)]
        [DisplayName("CSS class")]
        public string WidgetCssClass { get; set; }

        /// <summary>
        /// Gets or sets the name of the fields that the search will be done by.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [DisplayName("Search fields")]
        [Description("[{\"Type\":1,\"Chunks\":[{\"Value\":\"List of fields to be used in the search facets. These fields must be the same as those specified in the Search results widget.\",\"Presentation\":[]}]}]")]
        public string SearchFields { get; set; }

        /// <summary>
        /// Gets or sets the Search facets header.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [ContentSection("Labels and messages", 0)]
        [DisplayName("Search facets header")]
        [DefaultValue("Filter results")]
        public string FilterResultsLabel { get; set; }

        /// <summary>
        /// Gets or sets the applied filters label.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [ContentSection("Labels and messages", 1)]
        [DisplayName("Search facets label")]
        [DefaultValue("Applied filters")]
        public string AppliedFiltersLabel { get; set; }

        /// <summary>
        /// Gets or sets the clear all label.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [ContentSection("Labels and messages", 2)]
        [DisplayName("Clear facets link")]
        [DefaultValue("Clear all")]
        public string ClearAllLabel { get; set; }

        /// <summary>
        /// Gets or sets the show more label.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [ContentSection("Labels and messages", 3)]
        [DisplayName("Show more link")]
        [DefaultValue("Show more")]
        public string ShowMoreLabel { get; set; }

        /// <summary>
        /// Gets or sets the show less label.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [ContentSection("Labels and messages", 4)]
        [DisplayName("Show less link")]
        [DefaultValue("Show less")]
        public string ShowLessLabel { get; set; }

        /// <summary>
        /// Gets or sets the attributes for the facets.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [ContentSection(Constants.ContentSectionTitles.Attributes, 0)]
        [DisplayName("Data attributes for...")]
        [LengthDependsOn(null, "", "", ExtraRecords = "[{\"Name\": \"SearchFacets\", \"Title\": \"Search facets\"}]")]
        [DataType(customDataType: KnownFieldTypes.Attributes)]
        public IDictionary<string, IList<AttributeModel>> Attributes { get; set; }
    }
}
