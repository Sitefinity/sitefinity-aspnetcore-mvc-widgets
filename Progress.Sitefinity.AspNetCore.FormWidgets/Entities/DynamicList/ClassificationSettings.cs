using System.Runtime.Serialization;

namespace Progress.Sitefinity.AspNetCore.FormWidgets.Entities.Dropdown
{
    /// <summary>
    /// Represents the complex property for the chosen taxonomy in the classification widget.
    /// </summary>
    [DataContract]
    public class ClassificationSettings
    {
        /// <summary>
        /// Gets or sets the selected taxonomy's id.
        /// </summary>
        [DataMember]
        public string SelectedTaxonomyId { get; set; }

        /// <summary>
        /// Gets or sets the selected taxonomy's Url.
        /// </summary>
        [DataMember]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1056:URI-like properties should not be strings", Justification = "By design.")]
        public string SelectedTaxonomyUrl { get; set; }

        /// <summary>
        /// Gets or sets the selected taxonomy's Title.
        /// </summary>
        [DataMember]
        public string SelectedTaxonomyTitle { get; set; }

        /// <summary>
        /// Gets or sets the selected taxonomy's Name.
        /// </summary>
        [DataMember]
        public string SelectedTaxonomyName { get; set; }

        /// <summary>
        /// Gets or sets the selection mode.
        /// </summary>
        [DataMember]
        public TaxonSelectionMode SelectionMode { get; set; }

        /// <summary>
        /// Gets or sets the taxa ids, if the selection mode is set to "Selected".
        /// </summary>
        [DataMember]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1819:Properties should not return arrays", Justification = "By design.")]
        public string[] SelectedTaxaIds { get; set; }

        /// <summary>
        /// Gets or sets the Sitefinity content type (by type.FullName), that should be used for filtration.
        /// </summary>
        [DataMember]
        public string ByContentType { get; set; }
    }

    /// <summary>
    /// The taxon selection mode.
    /// </summary>
    public enum TaxonSelectionMode
    {
        /// <summary>
        /// Indicates that all taxa from the current taxonomy should be used
        /// </summary>
        All,

        /// <summary>
        /// Indicates that all top level taxa from from the current taxonomy should be used. Applicable only for hierarchical taxonomies.
        /// </summary>
        TopLevel,

        /// <summary>
        /// Indicates that only the child taxa of a selected taxon should be used. Applicable only for hierarchical taxonomies.
        /// </summary>
        UnderParent,

        /// <summary>
        /// Indicates that only a selected set of taxa should be used.
        /// </summary>
        Selected,

        /// <summary>
        /// Indicates that the taxa that should be used should be filtered by a content type.
        /// </summary>
        ByContentType,
    }
}
