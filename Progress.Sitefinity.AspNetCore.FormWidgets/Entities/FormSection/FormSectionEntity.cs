using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Progress.Sitefinity.AspNetCore.ViewComponents.AttributeConfigurator.Attributes;
using Progress.Sitefinity.Renderer.Designers;
using Progress.Sitefinity.Renderer.Designers.Attributes;

namespace Progress.Sitefinity.AspNetCore.FormWidgets.Entities.FormSection
{
    /// <summary>
    /// Entity for the form section widget.
    /// </summary>
    public class FormSectionEntity
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
    }
}
