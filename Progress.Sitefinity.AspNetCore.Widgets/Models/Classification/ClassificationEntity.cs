using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Progress.Sitefinity.AspNetCore.Models;
using Progress.Sitefinity.AspNetCore.ViewComponents.AttributeConfigurator.Attributes;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Common;
using Progress.Sitefinity.Renderer.Designers;
using Progress.Sitefinity.Renderer.Designers.Attributes;
using Progress.Sitefinity.Renderer.Entities.Content;
using static Progress.Sitefinity.AspNetCore.Constants;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Classification
{
    /// <summary>
    /// Entity for the Classification widget.
    /// </summary>
    public class ClassificationEntity : IHasMargins<MarginStyle>
    {
        /// <summary>
        /// Gets or sets the order by info.
        /// </summary>
        [ContentSection(ClassificationSectionTitles.SelectClassification, 0)]
        [DisplayName("Classification")]
        [Placeholder("Select classification")]
        [DataType(customDataType: "taxonSelector")]
        [Required(ErrorMessage = "Please select a classification")]
        public ClassificationSettings ClassificationSettings { get; set; }

        /// <summary>
        /// Gets or sets the order by info.
        /// </summary>
        [ContentSection(ClassificationSectionTitles.ListSettings, 0)]
        [DisplayName("Sort items")]
        [DefaultValue("Title ASC")]
        [DataType(customDataType: KnownFieldTypes.Choices)]
        [Choice(ServiceUrl = "{0}/Default.Sorters()?frontend=True", ServiceCallParameters = "[{ \"taxaUrl\" : \"{0}\"}]")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2243:Attribute string literals should parse correctly", Justification = "By design")]
        public string OrderBy { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the item count is visible.
        /// </summary>
        [ContentSection(ClassificationSectionTitles.ListSettings, 1)]
        [DataType(customDataType: KnownFieldTypes.ChipChoice)]
        [DisplayName("Display item count")]
        [DefaultValue(true)]
        [Choice("[{\"Title\":\"Yes\",\"Name\":\"Yes\",\"Value\":\"True\",\"Icon\":null},{\"Title\":\"No\",\"Name\":\"No\",\"Value\":\"False\",\"Icon\":null}]")]
        public bool ShowItemCount { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether empty classifications should be visible.
        /// </summary>
        [ContentSection(ClassificationSectionTitles.ListSettings, 2)]
        [DataType(customDataType: KnownFieldTypes.ChipChoice)]
        [DisplayName("Display empty tags or categories")]
        [DefaultValue(false)]
        [Choice("[{\"Title\":\"Yes\",\"Name\":\"Yes\",\"Value\":\"True\",\"Icon\":null},{\"Title\":\"No\",\"Name\":\"No\",\"Value\":\"False\",\"Icon\":null}]")]
        public bool ShowEmpty { get; set; }

        /// <summary>
        /// Gets or sets the view name.
        /// </summary>
        [ContentSection(ClassificationSectionTitles.DisplaySettings, 0)]
        [ViewSelector]
        [DisplayName("Classification template")]
        public string SfViewName { get; set; }

        /// <summary>
        /// Gets or sets the margins.
        /// </summary>
        [ContentSection(ClassificationSectionTitles.DisplaySettings, 1)]
        [DisplayName("Margins")]
        [TableView("Classification")]
        public MarginStyle Margins { get; set; }

        /// <summary>
        /// Gets or sets the CSS class of the wrapper.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [DisplayName("CSS class")]
        public string CssClass { get; set; }

        /// <summary>
        /// Gets or sets the sort expression.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [DisplayName("Sort expression")]
        [Description("Custom sort expression, used if default sorting options are not suitable.")]
        public string SortExpression { get; set; }

        /// <summary>
        /// Gets or sets the attributes for the search box.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [ContentSection(Constants.ContentSectionTitles.Attributes, 0)]
        [DisplayName("Attributes for...")]
        [LengthDependsOn(null, "", "", ExtraRecords = "[{\"Name\": \"Classification\", \"Title\": \"Classification\"}]")]
        [DataType(customDataType: KnownFieldTypes.Attributes)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Must be able to set in property editor.")]
        public IDictionary<string, IList<AttributeModel>> Attributes { get; set; }
    }
}
