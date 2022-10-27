using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Progress.Sitefinity.AspNetCore.Models;
using Progress.Sitefinity.AspNetCore.ViewComponents.AttributeConfigurator.Attributes;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Common;
using Progress.Sitefinity.Renderer.Designers;
using Progress.Sitefinity.Renderer.Designers.Attributes;
using Progress.Sitefinity.Renderer.Entities.Content;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Breadcrumb
{
    /// <summary>
    /// The entity for the breadcrumb widget. Contains all of the data persited in the database.
    /// </summary>
    public class BreadcrumbEntity : IHasMargins<MarginStyle>
    {
        /// <summary>
        /// Gets or sets a value that controls from where the breadcrumb will start.
        /// </summary>
        [ContentSection("Breadcrumb setup")]
        [DisplayName("Include in the breadcrumb...")]
        [DataType(customDataType: KnownFieldTypes.RadioChoice)]
        public BreadcrumbIncludeOption BreadcrumbIncludeOption { get; set; }

        /// <summary>
        /// Gets or sets the selected page.
        /// </summary>
        [DisplayName("")]
        [Content(Type = KnownContentTypes.Pages, AllowMultipleItemsSelection = false)]
        [ConditionalVisibility("{\"conditions\":[{\"fieldName\":\"BreadcrumbIncludeOption\",\"operator\":\"Equals\",\"value\":\"SpecificPagePath\"}],\"inline\":\"true\"}")]
        public MixedContentContext SelectedPage { get; set; }

        /// <summary>
        ///  Gets or sets a value indicating whether to add the home page at the beginning of the breadcrumb.
        /// </summary>
        [DisplayName("Home page link")]
        [ContentSection("Breadcrumb setup")]
        [DefaultValue(true)]
        [DataType(customDataType: KnownFieldTypes.CheckBox)]
        [Group("Display...")]
        public bool AddHomePageLinkAtBeginning { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to add the current page at the end.
        /// </summary>
        [DisplayName("Current page in the end of the breadcrumb")]
        [DataType(customDataType: KnownFieldTypes.CheckBox)]
        [ContentSection("Breadcrumb setup")]
        [DefaultValue(true)]
        [Group("Display...")]
        public bool AddCurrentPageLinkAtTheEnd { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to include group pages in the breadcrumb.
        /// </summary>
        [DisplayName("Group pages in the breadcrumb")]
        [DataType(customDataType: KnownFieldTypes.CheckBox)]
        [ContentSection("Breadcrumb setup")]
        [Group("Display...")]
        public bool IncludeGroupPages { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to add dynamic detail item pages in the breadcrumb.
        /// </summary>
        [DisplayName("Detail items in the breadcrumb")]
        [Group("Display...")]
        [DataType(customDataType: KnownFieldTypes.CheckBox)]
        [ContentSection("Breadcrumb setup")]
        public bool AllowVirtualNodes { get; set; }

        /// <summary>
        /// Gets or sets the view name.
        /// </summary>
        [ViewSelector]
        [ContentSection(Constants.ContentSectionTitles.DisplaySettings, 1)]
        [DisplayName("View")]
        public string SfViewName { get; set; }

        /// <summary>
        /// Gets or sets the margins.
        /// </summary>
        [ContentSection(Constants.ContentSectionTitles.DisplaySettings, 2)]
        [DisplayName("Margins")]
        [TableView("Breadcrumb")]
        public MarginStyle Margins { get; set; }

        /// <summary>
        /// Gets or sets the CSS class of the wrapper.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [DisplayName("CSS class")]
        public string WrapperCssClass { get; set; }

        /// <summary>
        /// Gets or sets the attributes for the content block.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [ContentSection(Constants.ContentSectionTitles.Attributes, 1)]
        [DisplayName("Attributes for...")]
        [LengthDependsOn(null, "", "", ExtraRecords = "[{\"Name\": \"Breadcrumb\", \"Title\": \"Breadcrumb\"}]")]
        [DataType(customDataType: KnownFieldTypes.Attributes)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Must be able to set in property editor.")]
        public IDictionary<string, IList<AttributeModel>> Attributes { get; set; }
    }
}
