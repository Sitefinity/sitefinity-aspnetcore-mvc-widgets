namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Facets
{
    internal class FacetResponseDto
    {
        public string FacetValue { get; set; }

        public long Count { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public SitefinityFacetType FacetType { get; set; }
    }
}
