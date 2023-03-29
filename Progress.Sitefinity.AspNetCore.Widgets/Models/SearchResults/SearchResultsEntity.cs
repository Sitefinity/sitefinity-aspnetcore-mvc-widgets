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

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.SearchResults
{
    /// <summary>
    /// Entity for the search results widget.
    /// </summary>
    public class SearchResultsEntity : IHasMargins<MarginStyle>
    {
        /// <summary>
        /// Gets or sets the list settings.
        /// </summary>
        [ContentSection(Constants.ContentSectionTitles.ResultsListSettings, 1)]
        [DisplayName("Number of list items")]
        public ExtendedContentListSettings ListSettings { get; set; }

        /// <summary>
        /// Gets or sets the sorting applied for the options.
        /// </summary>
        [ContentSection(Constants.ContentSectionTitles.ResultsListSettings, 2)]
        [DefaultValue(SearchResultsSorting.MostRelevantOnTop)]
        [DisplayName("Sort results")]
        [DataType(customDataType: KnownFieldTypes.Choices)]
        public SearchResultsSorting Sorting { get; set; }

        /// <summary>
        /// Gets or sets the allow users to sort results.
        /// </summary>
        [DefaultValue(1)]
        [ContentSection(Constants.ContentSectionTitles.ResultsListSettings, 3)]
        [DisplayName("Allow users to sort results")]
        [DataType(customDataType: KnownFieldTypes.ChipChoice)]
        [Choice("[{\"Title\":\"Yes\",\"Name\":\"Yes\",\"Value\":1,\"Icon\":null},{\"Title\":\"No\",\"Name\":\"No\",\"Value\":0,\"Icon\":null}]")]
        public int AllowUsersToSortResults { get; set; }

        /// <summary>
        /// Gets or sets the view name.
        /// </summary>
        [ContentSection(Constants.ContentSectionTitles.DisplaySettings, 1)]
        [ViewSelector]
        [DisplayName("Search results template")]
        public string SfViewName { get; set; }

        /// <summary>
        /// Gets or sets the margins.
        /// </summary>
        [ContentSection(Constants.ContentSectionTitles.DisplaySettings, 2)]
        [DisplayName("Margins")]
        [TableView("Search results")]
        public MarginStyle Margins { get; set; }

        /// <summary>
        /// Gets or sets the CSS class of the wrapper.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [DisplayName("CSS class")]
        public string CssClass { get; set; }

        /// <summary>
        /// Gets or sets the search fields.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [DisplayName("Search fields")]
        [Description("List of fields to be used in the search results. These fields must be included in the search index. If left empty, all fields from the search index will be used.")]
        public string SearchFields { get; set; }

        /// <summary>
        /// Gets or sets the highlighted fields.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [DisplayName("Highlighted fields")]
        [Description("List of fields to be highlighted in the search results. These fields must be included in the search index. If left empty, all search fields will be highlighted.")]
        public string HighlightedFields { get; set; }

        /// <summary>
        /// Gets or sets the search results placeholder.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [ContentSection("Labels and messages", 0)]
        [DisplayName("Search results header")]
        [DefaultValue("Results for \"{0}\"")]
        public string SearchResultsHeader { get; set; }

        /// <summary>
        /// Gets or sets the no search results placeholder.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [ContentSection("Labels and messages", 0)]
        [DisplayName("No results header")]
        [DefaultValue("No search results for \"{0}\"")]
        public string NoResultsHeader { get; set; }

        /// <summary>
        /// Gets or sets the results number label placeholder.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [ContentSection("Labels and messages", 0)]
        [DisplayName("Results number label")]
        [DefaultValue("results")]
        public string ResultsNumberLabel { get; set; }

        /// <summary>
        /// Gets or sets the languages label placeholder.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [ContentSection("Labels and messages", 0)]
        [DisplayName("Languages label")]
        [DefaultValue("Show results in")]
        public string LanguagesLabel { get; set; }

        /// <summary>
        /// Gets or sets the attributes for the search results widget.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [ContentSection(Constants.ContentSectionTitles.Attributes, 0)]
        [DisplayName("Attributes for...")]
        [LengthDependsOn(null, "", "", ExtraRecords = "[{\"Name\": \"SearchResults\", \"Title\": \"Search results\"}]")]
        [DataType(customDataType: KnownFieldTypes.Attributes)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Must be able to set in property editor.")]
        public IDictionary<string, IList<AttributeModel>> Attributes { get; set; }
    }
}
