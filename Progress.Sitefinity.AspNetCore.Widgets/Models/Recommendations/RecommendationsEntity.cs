using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Progress.Sitefinity.AspNetCore.Models;
using Progress.Sitefinity.AspNetCore.ViewComponents.AttributeConfigurator.Attributes;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Common;
using Progress.Sitefinity.Renderer.Designers;
using Progress.Sitefinity.Renderer.Designers.Attributes;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Recommendations
{
    /// <summary>
    /// The entity for the Recommendations widget. Contains all of the data persited in the database.
    /// </summary>
    public class RecommendationsEntity : IHasMargins<MarginStyle>
    {
        /// <summary>
        /// Gets or sets the conversion.
        /// </summary>
        [ContentSection(Constants.RecommendationsSectionTitles.RecommendationSettings, 0)]
        [DataType(customDataType: KnownFieldTypes.Choices)]
        [Placeholder("Select")]
        [Description("Recommendations aim to improve visitor conversions. Select a conversion defined in Sitefinity Insight that you want to improve. Sitefinity Insight AI will generate different content recommendations for each visitor that will likely influence that visitor to complete the conversion.")]
        [DisplayName("Conversion to be improved")]
        [Choice(ServiceUrl = "/Default.GetConversions()", ServiceWarningMessage = "No conversions are found in Sitefinity Insight.")]
        public int Conversion { get; set; }

        /// <summary>
        /// Gets or sets the view name.
        /// </summary>
        [ViewSelector(RegexFilter = "^(?!Sample).*(?<!Sample)$")]
        [ContentSection(Constants.ContentSectionTitles.DisplaySettings, 1)]
        [DisplayName("Template")]
        public string SfViewName { get; set; }

        /// <summary>
        /// Gets or sets the header that will be displayed above the recommendations e.g. Read more.
        /// </summary>
        [ContentSection(Constants.ContentSectionTitles.DisplaySettings, 2)]
        [DefaultValue(RecommendationsConstants.DefaultHeader)]
        public string Header { get; set; }

        /// <summary>
        /// Gets or sets the CSS class of the button.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [DisplayName("CSS class")]
        public string CssClass { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of recmomendations shown.
        /// </summary>
        [Browsable(false)]
        [DefaultValue(1)]
        public int MaxNumberOfItems { get; set; }

        /// <inheritdoc/>
        [ContentSection(Constants.ContentSectionTitles.DisplaySettings, 3)]
        [DisplayName("Margins")]
        [TableView("Content recommendations")]
        public MarginStyle Margins { get; set; }

        /// <summary>
        /// Gets or sets the attributes for the recommendations widget.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [ContentSection(Constants.ContentSectionTitles.Attributes, 0)]
        [DisplayName("Attributes for...")]
        [LengthDependsOn(null, "", "", ExtraRecords = "[{\"Name\": \"ContentRecommendations\", \"Title\": \"Content recommendations\"}]")]
        [DataType(customDataType: KnownFieldTypes.Attributes)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Must be able to set in property editor.")]
        public IDictionary<string, IList<AttributeModel>> Attributes { get; set; }
    }
}
