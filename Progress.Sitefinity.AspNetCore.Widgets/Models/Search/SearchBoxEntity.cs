using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Progress.Sitefinity.AspNetCore.Models;
using Progress.Sitefinity.AspNetCore.ViewComponents.AttributeConfigurator.Attributes;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Common;
using Progress.Sitefinity.Renderer.Designers;
using Progress.Sitefinity.Renderer.Designers.Attributes;
using Progress.Sitefinity.Renderer.Entities.Content;
using static Progress.Sitefinity.AspNetCore.Constants;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Search
{
    /// <summary>
    /// Entity for the search box widget.
    /// </summary>
    public class SearchBoxEntity : IHasMargins<MarginStyle>
    {
        /// <summary>
        /// Gets or sets the order by info.
        /// </summary>
        [ContentSection(SearchSectionTitles.SearchSetup, 0)]
        [DisplayName("Specify content to search in")]
        [Placeholder("Select search index")]
        [Required(ErrorMessage = "Please select a search index")]
        [DataType(customDataType: KnownFieldTypes.Choices)]
        [Choice(ServiceUrl = "/Default.GetSearchIndexes", ServiceWarningMessage = "No search indexes have been created.")]
        [Description("[{\"Type\":1,\"Chunks\":[{\"Value\":\"Use search indexes to define different sets\",\"Presentation\":[]}, {\"Value\":\"of content visitors can search by using the\",\"Presentation\":[2]}, {\"Value\":\"internal search of the your site.\",\"Presentation\":[2]}]},{\"Type\":1,\"Chunks\":[{\"Value\":\"Manage search indexes in\",\"Presentation\":[]},{\"Value\":\"Administration > Search indexes\",\"Presentation\":[2,3]}]}]")]
        public string SearchIndex { get; set; }

        /// <summary>
        /// Gets or sets the search results page.
        /// </summary>
        [ContentSection(SearchSectionTitles.SearchSetup, 1)]
        [DisplayName("Search results page")]
        [Content(Type = KnownContentTypes.Pages, AllowMultipleItemsSelection = false)]
        [Description("This is the page where you have dropped the Search results widget.")]
        [Required(ErrorMessage = "Please select a search results page")]
        public MixedContentContext SearchResultsPage { get; set; }

        /// <summary>
        /// Gets or sets the suggestions trigger count.
        /// </summary>
        [DefaultValue(0)]
        [ContentSection(SearchSectionTitles.SearchSetup, 2)]
        [DisplayName("Display suggestions after typing...")]
        [DataType(customDataType: KnownFieldTypes.ChipChoice)]
        [Choice(Choices = "[{\"Title\":\"Don't display suggestions\",\"Name\":\"0\",\"Value\":0,\"Icon\":\"ban\"},{\"Title\":\"2\",\"Name\":\"2\",\"Value\":2,\"Icon\":null},{\"Title\":\"3\",\"Name\":\"3\",\"Value\":3,\"Icon\":null},{\"Title\":\"4\",\"Name\":\"4\",\"Value\":4,\"Icon\":null}]", SideLabel = "characters")]
        public int? SuggestionsTriggerCharCount { get; set; }

        /// <summary>
        /// Gets or sets the scoring profile while using Azure search provider.
        /// </summary>
        [ContentSection(SearchSectionTitles.BoostSearch, 0)]
        [DisplayName("Scoring profile")]
        [Placeholder("Select scoring")]
        [DataType(customDataType: KnownFieldTypes.Choices)]
        [Choice(ServiceUrl = "/Default.GetScoringProfiles(catalogName='{0}')", ServiceCallParameters = "[{ \"catalogName\" : \"{0}\"}]")]
        [Description("Scoring profiles are part of the search index and consist of weighted fields, functions, and parameters. Use scoring profiles to boost search results by customizing the way different fields are ranked. Manage scoring profiles in the Azure portal.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2243:Attribute string literals should parse correctly", Justification = "By design")]
        public string ScoringProfile { get; set; }

        /// <summary>
        /// Gets or sets the scoring parameters while using Azure search provider.
        /// </summary>
        [ContentSection(SearchSectionTitles.BoostSearch, 1)]
        [DisplayName("Scoring parameters")]
        [Description("[{\"Type\":1,\"Chunks\":[{\"Value\":\"Scoring parameters are part of the scoring\",\"Presentation\":[]}, {\"Value\":\"functions within a scorig profile. Add\",\"Presentation\":[2]}, {\"Value\":\"scoring parameters to boost content to\",\"Presentation\":[2]}, {\"Value\":\"appear higher in the search results by\",\"Presentation\":[2]}, {\"Value\":\"specifying the parameter's name and\",\"Presentation\":[2]}, {\"Value\":\"value.\",\"Presentation\":[2]}, {\"Value\":\"Example: \",\"Presentation\":[]}, {\"Value\":\"testparam:tag1\",\"Presentation\":[3]}, {\"Value\":\"Manage scoring parameters in the Azure\",\"Presentation\":[2]}, {\"Value\":\"portal.\",\"Presentation\":[2]}]}]")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Must be able to set the parameters in editor")]
        [ConditionalVisibility("{\"conditions\":[{\"fieldName\":\"ScoringProfile\",\"operator\":\"NotEquals\",\"value\":null}]}")]
        public IList<string> ScoringParameters { get; set; }

        /// <summary>
        /// Gets or sets the view name.
        /// </summary>
        [ContentSection(Constants.ContentSectionTitles.DisplaySettings, 0)]
        [ViewSelector]
        [DisplayName("Search box template")]
        public string SfViewName { get; set; }

        /// <summary>
        /// Gets or sets the margins.
        /// </summary>
        [ContentSection(Constants.ContentSectionTitles.DisplaySettings, 1)]
        [DisplayName("Margins")]
        [TableView("Search box")]
        public MarginStyle Margins { get; set; }

        /// <summary>
        /// Gets or sets the CSS class of the wrapper.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [DisplayName("CSS class")]
        public string CssClass { get; set; }

        /// <summary>
        /// Gets or sets the suggestion fields.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [DisplayName("Suggestion fields")]
        [DefaultValue("Title,Content")]
        [Description("List the fields to be used in the search suggestions. These fields must be included in the search index.")]
        public string SuggestionFields { get; set; }

        /// <summary>
        /// Gets or sets the search box placeholder.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [ContentSection("Labels and messages", 0)]
        [DisplayName("Search box placeholder text")]
        [DefaultValue("Search...")]
        public string SearchBoxPlaceholder { get; set; }

        /// <summary>
        /// Gets or sets the search button label.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [ContentSection("Labels and messages", 1)]
        [DisplayName("Search button")]
        [DefaultValue("Search")]
        public string SearchButtonLabel { get; set; }

        /// <summary>
        /// Gets or sets the attributes for the search box.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [ContentSection(Constants.ContentSectionTitles.Attributes, 0)]
        [DisplayName("Attributes for...")]
        [LengthDependsOn(null, "", "", ExtraRecords = "[{\"Name\": \"SearchBox\", \"Title\": \"Search box\"}]")]
        [DataType(customDataType: KnownFieldTypes.Attributes)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Must be able to set in property editor.")]
        public IDictionary<string, IList<AttributeModel>> Attributes { get; set; }
    }
}
