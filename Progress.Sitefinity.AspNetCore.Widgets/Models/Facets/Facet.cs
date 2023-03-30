using System.Collections.Generic;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Facets
{
    internal class Facet
    {
        public string FieldName { get; set; }

        public List<CustomFacetRange> CustomIntervals { get; set; }

        public string IntervalRange { get; set; }

        public string FacetFieldType { get; set; }

        public SitefinityFacetType SitefinityFacetType { get; set; }
    }
}
