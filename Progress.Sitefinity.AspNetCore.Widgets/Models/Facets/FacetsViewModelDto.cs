using System.Runtime.Serialization;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Facets
{
    internal class FacetsViewModelDto
    {
        [DataMember]
        public string FacetableFieldLabels { get; set; }

        [DataMember]
        public string[] FacetableFieldNames { get; set; }

        [DataMember]
        public string FacetableFieldType { get; set; }
    }
}
