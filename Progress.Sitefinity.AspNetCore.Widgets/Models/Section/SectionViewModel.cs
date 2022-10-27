using System.Collections.Generic;
using Progress.Sitefinity.AspNetCore.Models;
using Progress.Sitefinity.AspNetCore.ViewComponents;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Section
{
    /// <summary>
    /// The view model for the section widget.
    /// </summary>
    public class SectionViewModel
    {
        /// <summary>
        /// Gets or sets the column's count.
        /// </summary>
        public int ColumnsCount { get; set; }

        /// <summary>
        /// Gets or sets the classes applied for the section element(row).
        /// </summary>
        public string SectionClasses { get; set; }

        /// <summary>
        /// Gets or sets the styles applied for the section element(row).
        /// </summary>
        public string SectionStyle { get; set; }

        /// <summary>
        /// Gets or sets the video URL.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1056:Uri properties should not be strings", Justification = "Provie this to the view.")]
        public string VideoUrl { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to show video.
        /// </summary>
        public bool ShowVideo { get; set; }

        /// <summary>
        /// Gets or sets the names of the columns.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Need to set the view model.")]
        public IList<string> ColumnNames { get; set; }

        /// <summary>
        /// Gets or sets the title of the columns.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Need to set the view model.")]
        public IList<string> ColumnTitles { get; set; }

        /// <summary>
        /// Gets or sets the classes applied for the columns.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Need to set the view model.")]
        public IList<string> ColumnsClasses { get; set; }

        /// <summary>
        /// Gets or sets the proportions information for the columns.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Need to set the view model.")]
        public IList<string> ColumnProportions { get; set; }

        /// <summary>
        /// Gets or sets the styles applied for the columns.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Need to set the view model.")]
        public IList<string> ColumnStyles { get; set; }

        /// <summary>
        /// Gets or sets the view context.
        /// </summary>
        public ICompositeViewComponentContext Context { get; set; }

        /// <summary>
        /// Gets or sets the attributes for the columns and for the section.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Must be able to set in property editor.")]
        public IDictionary<string, IList<AttributeModel>> Attributes { get; set; }

        /// <summary>
        /// Gets or sets the attributes for the columns and for the section.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Must be able to set in property editor.")]
        public IList<IList<AttributeModel>> ColumnsAttributes { get; set; }

        /// <summary>
        /// Gets or sets the section attributes.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Must be able to set in property editor.")]
        public IList<AttributeModel> SectionAttributes { get; set; }

        /// <summary>
        /// Gets or sets the tag name.
        /// </summary>
        public string TagName { get; set; }
    }
}
