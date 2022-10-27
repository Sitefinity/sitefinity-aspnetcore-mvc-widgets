namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Recommendations
{
    internal class RecommendationsStateFeatureDto
    {
        public bool HasInsight { get; set; }

        public bool LostConnectionToInsight { get; set; }

        public bool IsContentRecommendationsFeatureEnabled { get; set; }

        public bool HasValidConnectionForCurrentSite { get; set; }

        public bool ConversionExists { get; set; }
    }
}
