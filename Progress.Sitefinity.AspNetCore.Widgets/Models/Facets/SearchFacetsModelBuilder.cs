using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Facets
{
    internal static class SearchFacetsModelBuilder
    {
        internal static IList<SearchFacets> BuildFacetsViewModel(
            IList<FacetField> facetsWidgetDefinition,
            IDictionary<string, IList<FacetResponseDto>> facets,
            IList<string> facetableFieldsKeysFromIndex,
            string sortType)
        {
            IList<SearchFacets> searchFacets = new List<SearchFacets>();
            if (facetableFieldsKeysFromIndex.Any())
            {
                IDictionary<string, FacetField> widgetFacetableFields =
                    facetsWidgetDefinition
                        .Where(f => facetableFieldsKeysFromIndex.Contains(f.FacetableFieldNames[0]))
                        .GroupBy(x => x.FacetableFieldNames[0])
                        .Select(f => f.LastOrDefault())
                        .ToDictionary(x => x.FacetableFieldNames[0], v => v);

                foreach (var facet in facets)
                {
                    List<FacetResponseDto> facetResponses = null;
                    if (facet.Value.Any(f => f.FacetType == SitefinityFacetType.Value))
                    {
                        facetResponses = facet.Value.Where(f => !string.IsNullOrEmpty(f.FacetValue)).ToList();
                    }
                    else
                    {
                        facetResponses = facet.Value.ToList();
                    }

                    List<FacetElement> facetElementValues = MapToFacetElementModel(facetResponses, facet.Key, widgetFacetableFields);
                    FacetField facetField = widgetFacetableFields[facet.Key];
                    var searchFacetViewModel = new SearchFacets(facetField, facetElementValues);
                    searchFacets.Add(searchFacetViewModel);
                }

                searchFacets = SortFacetsModel(widgetFacetableFields, searchFacets, sortType);
            }

            return searchFacets;
        }

        private static IList<SearchFacets> SortFacetsModel(IDictionary<string, FacetField> facetableFieldsFromIndex, IList<SearchFacets> searchFacets, string sortType)
        {
            if (sortType == AlphabeticallySort)
            {
                searchFacets = searchFacets
                                        .OrderBy(f => f.FacetTitle)
                                        .ToList();
            }
            else
            {
                var facetsOrder = facetableFieldsFromIndex
                                    .Values
                                    .Select(x => x.FacetableFieldNames[0])
                                    .ToList();

                searchFacets = searchFacets
                                     .OrderBy(f => facetsOrder.IndexOf(f.FacetFieldName))
                                     .ToList();
            }

            return searchFacets;
        }

        private static List<FacetElement> MapToFacetElementModel(List<FacetResponseDto> facetResponses, string facetName, IDictionary<string, FacetField> widgetFacetableFields)
        {
            var facetElementsViewModel = new List<FacetElement>();

            foreach (var facet in facetResponses)
            {
                var facetViewModel = new FacetElement();
                string facetElementLabel;
                if (TryGetFacetLabel(facet, widgetFacetableFields[facetName], out facetElementLabel))
                {
                    facetViewModel.FacetLabel = facetElementLabel;
                    facetViewModel.FacetCount = facet.Count;
                    facetViewModel.FacetValue = ComputeFacetValue(facet);

                    facetElementsViewModel.Add(facetViewModel);
                }
            }

            return facetElementsViewModel;
        }

        private static bool TryGetFacetLabel(FacetResponseDto facetResponse, FacetField facetField, out string facetLabel)
        {
            facetLabel = string.Empty;
            if (facetField.FacetFieldSettings.IsValueFacet())
            {
                facetLabel = facetResponse.FacetValue;
                return true;
            }

            var facetTableFieldType = facetField.FacetFieldSettings.FacetType;
            if (facetTableFieldType == SearchIndexAdditonalFieldType.NumberDecimal.ToString()
                    || facetTableFieldType == SearchIndexAdditonalFieldType.NumberWhole.ToString())
            {
                if (facetResponse.FacetType == SitefinityFacetType.Interval)
                {
                    facetLabel = GetIntervalNumberLabel(facetResponse, facetField);
                    return true;
                }
                else if (facetResponse.FacetType == SitefinityFacetType.Range)
                {
                    facetLabel = GetRangeNumberLabel(facetResponse, facetField);
                    return !string.IsNullOrEmpty(facetLabel);
                }
            }
            else if (facetTableFieldType == SearchIndexAdditonalFieldType.DateAndTime.ToString())
            {
                if (facetResponse.FacetType == SitefinityFacetType.Range)
                {
                    facetLabel = GetRangeDateLabel(facetResponse, facetField);
                    return !string.IsNullOrEmpty(facetLabel);
                }
                else
                {
                    if (DateTime.TryParse(facetResponse.From?.ToString(), out DateTime fromValue))
                    {
                        var dateStep = WidgetSettingsFacetFieldMapper.GetIntervalDateTime(facetField.FacetFieldSettings.DateStep);
                        facetLabel = FormatDateInterval(dateStep, fromValue);
                        return !string.IsNullOrEmpty(facetLabel);
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            facetLabel = facetResponse.FacetValue;
            return true;
        }

        private static string GetRangeDateLabel(FacetResponseDto facetResponse, FacetField facetableFieldSettings)
        {
            var dateRanges = facetableFieldSettings.FacetFieldSettings.DateRanges;

            var dateRange = dateRanges.FirstOrDefault(r =>
                r.From.HasValue && r.From.Value.ToUniversalTime().ToString(WidgetSettingsFacetFieldMapper.DateTimeFormat, CultureInfo.InvariantCulture) == facetResponse.From &&
                r.To.HasValue && r.To.Value.ToUniversalTime().ToString(WidgetSettingsFacetFieldMapper.DateTimeFormat, CultureInfo.InvariantCulture) == facetResponse.To);

            if (dateRange != null)
            {
                return dateRange.Label;
            }

            return string.Empty;
        }

        private static string FormatDateInterval(string dateStep, DateTime intervalValue)
        {
            switch (dateStep)
            {
                case "day":
                case "week":
                case "quarter":
                    return intervalValue.ToString("MMM dd yyyy", CultureInfo.InvariantCulture);
                case "month":
                    return intervalValue.ToString("MMM yyyy", CultureInfo.InvariantCulture);
                case "year":
                    return intervalValue.ToString("yyyy", CultureInfo.InvariantCulture);
                default:
                    return null;
            }
        }

        private static string GetRangeNumberLabel(FacetResponseDto facetResponse, FacetField facetableFieldSettings)
        {
            double? from = ParseRangeValue(facetResponse.From);
            double? to = ParseRangeValue(facetResponse.To);

            var numberRanges = facetableFieldSettings.FacetFieldSettings.NumberRanges;
            if (numberRanges != null)
            {
                var numberRange = numberRanges.FirstOrDefault(r => r.From == from && r.To == to);
                if (numberRange != null)
                {
                    return numberRange.Label;
                }
            }

            var decimalNumbeRanges = facetableFieldSettings.FacetFieldSettings.NumberRangesDecimal;
            if (decimalNumbeRanges != null)
            {
                var numberRangeDecimal = decimalNumbeRanges.FirstOrDefault(r => r.From == from && r.To == to);
                if (numberRangeDecimal != null)
                {
                    return numberRangeDecimal.Label;
                }
            }

            return string.Empty;
        }

        private static string GetIntervalNumberLabel(FacetResponseDto facetResponse, FacetField facetableFieldSettings)
        {
            string facetLabel;
            string prefix = facetableFieldSettings.FacetFieldSettings.Prefix;
            string suffix = facetableFieldSettings.FacetFieldSettings.Suffix;

            facetLabel = $"{prefix}{facetResponse.From}{suffix} - {prefix}{facetResponse.To}{suffix}";
            return facetLabel;
        }

        private static double? ParseRangeValue(string val)
        {
            double? parsedValue = double.TryParse(val, NumberStyles.Float, CultureInfo.InvariantCulture, out var tempVal) ? tempVal : (double?)null;
            return parsedValue;
        }

        private static string ComputeFacetValue(FacetResponseDto f)
        {
            return f.FacetType == SitefinityFacetType.Value ? f.FacetValue : $"{f.From}{RangeSeparator}{f.To}";
        }

        private const string AlphabeticallySort = "2";
        private const string RangeSeparator = "__sf-range__";
    }
}
