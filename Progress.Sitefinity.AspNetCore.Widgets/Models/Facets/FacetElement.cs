namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Facets
{
    /// <summary>
    /// Facet element model.
    /// </summary>
    public class FacetElement
    {
        /// <summary>
        /// Gets or sets the facet value.
        /// </summary>
        public string FacetValue { get; set; }

        /// <summary>
        /// Gets or sets the label that will be displayed for each facet. May include prefix/suffix in case of interval and a custom text in case of a range.
        /// </summary>
        public string FacetLabel { get; set; }

        /// <summary>
        /// Gets or sets the facet count.
        /// </summary>
        public long FacetCount { get; set; }
    }
}
