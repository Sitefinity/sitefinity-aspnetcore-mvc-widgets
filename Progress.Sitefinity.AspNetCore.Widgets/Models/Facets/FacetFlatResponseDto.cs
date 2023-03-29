using System.Collections.Generic;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Facets
{
    internal class FacetFlatResponseDto
    {
        public string FacetKey { get; set; }

        public IList<FacetResponseDto> FacetResponses { get; set; }
    }
}
