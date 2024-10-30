using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Progress.Sitefinity.AspNetCore.Models;
using Progress.Sitefinity.AspNetCore.Models.Common;
using Progress.Sitefinity.AspNetCore.ViewComponents.AttributeConfigurator.Attributes;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Common;
using Progress.Sitefinity.Renderer.Designers;
using Progress.Sitefinity.Renderer.Designers.Attributes;
using Progress.Sitefinity.Renderer.Entities.Content;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Navigation
{
    /// <summary>
    /// The entity for the Navigation widget. Contains all of the data persited in the database.
    /// </summary>
    public class NavigationEntity : IHasMargins<MarginStyle>
    {
        /// <summary>
        ///     Gets or sets the page links to display selection mode.
        /// </summary>
        /// <value>The page display mode.</value>
        [ContentSection("Select pages", 0)]
        [DisplayName("Display...")]
        [DataType(customDataType: KnownFieldTypes.RadioChoice)]
        public PageSelectionMode SelectionMode { get; set; }

        /// <summary>
        ///     Gets or sets the levels to include.
        /// </summary>
        [ContentSection("Select pages", 0)]
        [DisplayName("Levels to include")]
        [DataType(customDataType: KnownFieldTypes.ChipChoice)]
        [DefaultValue(1)]
        [Choice("[{\"Title\":\"1 level\",\"Name\":\"1\",\"Value\":1,\"Icon\":null},{\"Title\":\"2 levels\",\"Name\":\"2\",\"Value\":2,\"Icon\":null},{\"Title\":\"3 levels\",\"Name\":\"3\",\"Value\":3,\"Icon\":null},{\"Title\":\"4 levels\",\"Name\":\"4\",\"Value\":4,\"Icon\":null},{\"Title\":\"5 levels\",\"Name\":\"5\",\"Value\":5,\"Icon\":null},{\"Title\":\"All levels\",\"Name\":\"All\",\"Value\":null,\"Icon\":null}]", NotResponsive = true)]
        [IsNullable(true)]
        public int? LevelsToInclude { get; set; }

        /// <summary>
        /// Gets or sets the selected page.
        /// </summary>
        [ContentSection("Select pages", 0)]
        [DisplayName("")]
        [Content(Type = KnownContentTypes.Pages, AllowMultipleItemsSelection = false)]
        [ConditionalVisibility("{\"conditions\":[{\"fieldName\":\"SelectionMode\",\"operator\":\"Equals\",\"value\":\"SelectedPageChildren\"}],\"inline\":\"true\"}")]
        public MixedContentContext SelectedPage { get; set; }

        /// <summary>
        /// Gets or sets the selected page.
        /// </summary>
        [ContentSection("Select pages", 0)]
        [DisplayName("")]
        [Content(Type = KnownContentTypes.Pages, AllowMultipleItemsSelection = true, OpenMultipleItemsSelection = true)]
        [ConditionalVisibility("{\"conditions\":[{\"fieldName\":\"SelectionMode\",\"operator\":\"Equals\",\"value\":\"SelectedPages\"}],\"inline\":\"true\"}")]
        public MixedContentContext CustomSelectedPages { get; set; }

        /// <summary>
        /// Gets or sets the CSS class of the wrapper.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [DisplayName("CSS class")]
        public string WrapperCssClass { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [show parent page].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [show parent page]; otherwise, <c>false</c>.
        /// </value>
        [Category(PropertyCategory.Advanced)]
        [DisplayName("Show parent page")]
        public bool ShowParentPage { get; set; }

        /// <summary>
        /// Gets or sets the margins.
        /// </summary>
        [ContentSection(Constants.ContentSectionTitles.DisplaySettings, 1)]
        [DisplayName("Margins")]
        [TableView("Navigation")]
        public MarginStyle Margins { get; set; }

        /// <summary>
        /// Gets or sets the view name.
        /// </summary>
        [ViewSelector]
        [ContentSection(Constants.ContentSectionTitles.DisplaySettings, 2)]
        [DefaultValue("Horizontal")]
        [DisplayName("View")]
        public string SfViewName { get; set; }

        /// <summary>
        /// Gets or sets the attributes for navigation widget.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [ContentSection(Constants.ContentSectionTitles.Attributes)]
        [DisplayName("Attributes for...")]
        [DataType(customDataType: KnownFieldTypes.Attributes)]
        [LengthDependsOn(null, "", "", ExtraRecords = "[{\"Name\": \"Navigation\", \"Title\": \"Navigation\"}]")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Must be able to set in property editor.")]
        public IDictionary<string, IList<AttributeModel>> Attributes { get; set; }
    }
}
