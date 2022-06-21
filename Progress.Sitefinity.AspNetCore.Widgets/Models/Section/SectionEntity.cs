using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Progress.Sitefinity.AspNetCore.Models;
using Progress.Sitefinity.AspNetCore.ViewComponents.AttributeConfigurator.Attributes;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Common;
using Progress.Sitefinity.Renderer.Designers;
using Progress.Sitefinity.Renderer.Designers.Attributes;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Section
{
    /// <summary>
    /// Entity for the section widget.
    /// </summary>
    public class SectionEntity
    {
        /// <summary>
        /// Gets or sets the number of columns.
        /// </summary>
        [DefaultValue(1)]
        [Category(PropertyCategory.QuickEdit)]
        [Range(1, 12, ErrorMessage = "Column's count must be between {1} and {2}.")]
        public int ColumnsCount { get; set; }

        /// <summary>
        /// Gets or sets the number of columns.
        /// </summary>
        [Category(PropertyCategory.QuickEdit)]
        [ConfigurationDefaultValue("Widgets:Styling:CssGridSystemColumnCount")]
        public int CssSystemGridSize { get; set; }

        /// <summary>
        /// Gets or sets the custom CSS for the columns and for the section.
        /// </summary>
        [Category(PropertyCategory.QuickEdit)]
        [DisplayName("Proportions")]
        [LengthDependsOn(nameof(ColumnsCount), "Column", "Column")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Must be able to set in property editor.")]
        public IList<string> ColumnProportionsInfo { get; set; } = new List<string>() { "12" };

        /// <summary>
        /// Gets or sets the section paddings.
        /// </summary>
        [ContentSection("Section style", 0)]
        [DisplayName("Padding")]
        [TableView("Section")]
        public PaddingStyle SectionPadding { get; set; }

        /// <summary>
        /// Gets or sets the section margins.
        /// </summary>
        [ContentSection("Section style", 1)]
        [DisplayName("Margin")]
        [TableView("Section")]
        public MarginStyle SectionMargin { get; set; }

        /// <summary>
        /// Gets or sets the section backgorund.
        /// </summary>
        [ContentSection("Section style", 1)]
        [DisplayName("Background")]
        [TableView("Section")]
        public BackgroundStyle SectionBackground { get; set; }

        /// <summary>
        /// Gets or sets the paddings for the columns.
        /// </summary>
        [ContentSection("Column style", 0)]
        [DisplayName("Padding")]
        [LengthDependsOn(nameof(ColumnsCount), "Column", "Column ")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Must be able to set in property editor.")]
        public IDictionary<string, PaddingStyle> ColumnsPadding { get; set; }

        /// <summary>
        /// Gets or sets the columns background.
        /// </summary>
        [ContentSection("Column style", 2)]
        [DisplayName("Background")]
        [LengthDependsOn(nameof(ColumnsCount), "Column", "Column ")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Must be able to set in property editor.")]
        public IDictionary<string, SimpleBackgroundStyle> ColumnsBackground { get; set; }

        /// <summary>
        /// Gets or sets the custom CSS for the columns and for the section.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [ContentSection("Custom CSS classes")]
        [DisplayName("Custom CSS class for...")]
        [LengthDependsOn(nameof(ColumnsCount), "Column", "Column ", ExtraRecords = "[{\"Name\": \"Section\", \"Title\": \"Section\"}]")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Must be able to set in property editor.")]
        public IDictionary<string, CustomCssModel> CustomCssClass { get; set; }

        /// <summary>
        /// Gets or sets the custom labels for the columns and for the section.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [ContentSection("Labels")]
        [DisplayName("Section and column labels")]
        [Description("Custom labels are displayed in the page editor for your convenience. They do not appear on the public site. You can change the generic name for this section and add column labels in the section widget.")]
        [LengthDependsOn(nameof(ColumnsCount), "Column", "Column ", ExtraRecords = "[{\"Name\": \"Section\", \"Title\": \"Section\"}]")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Must be able to set in property editor.")]
        public IDictionary<string, LabelModel> Labels { get; set; }

        /// <summary>
        /// Gets or sets the attributes for the columns and for the section.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [ContentSection(Constants.ContentSectionTitles.Attributes)]
        [DisplayName("Attributes for...")]
        [DataType(customDataType: KnownFieldTypes.Attributes)]
        [LengthDependsOn(nameof(ColumnsCount), "Column", "Column ", ExtraRecords = "[{\"Name\": \"Section\", \"Title\": \"Section\"}]")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Must be able to set in property editor.")]
        public IDictionary<string, IList<AttributeModel>> Attributes { get; set; }
    }
}
