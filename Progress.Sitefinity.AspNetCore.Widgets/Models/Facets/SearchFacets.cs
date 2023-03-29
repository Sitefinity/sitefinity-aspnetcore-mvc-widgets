using System;
using System.Collections.Generic;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Facets
{
    /// <summary>
    /// Model representing search facets for a particular field.
    /// </summary>
    public class SearchFacets
    {
        private FacetField facetField;

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchFacets"/> class.
        /// </summary>
        public SearchFacets()
        {
            this.FacetElements = new List<FacetElement>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchFacets"/> class.
        /// </summary>
        /// <param name="facetField">The facet widget field model which contains the name of the facet field and its settings.</param>
        /// <param name="facetElements">A collection of facet values.</param>
        public SearchFacets(FacetField facetField, List<FacetElement> facetElements)
        {
            if (facetField == null)
                throw new ArgumentNullException(nameof(facetField));

            this.facetField = facetField;
            this.FacetElements = facetElements;

            this.FacetTitle = facetField.FacetableFieldLabels;
            this.FacetFieldName = facetField.FacetableFieldNames[0];
            this.FacetFieldType = facetField.FacetFieldSettings.FacetType;
        }

        /// <summary>
        /// Gets or sets the search facets.
        /// </summary>
        public IList<FacetElement> FacetElements { get; set; }

        /// <summary>
        /// Gets or sets the title of the field that the facets are for.
        /// </summary>
        public string FacetTitle { get; set; }

        /// <summary>
        /// Gets or sets the field name of the field that the facets are for.
        /// </summary>
        public string FacetFieldName { get; set; }

        /// <summary>
        /// Gets the facet field type.
        /// </summary>
        public string FacetFieldType { get; }

        /// <summary>
        /// Gets a value indicating whether number custom ranges should be shown.
        /// </summary>
        public bool ShowNumberCustomRange
        {
            get
            {
                return this.facetField.FacetFieldSettings.DisplayCustomRange &&
                (this.FacetFieldType == SearchIndexAdditonalFieldType.NumberWhole.ToString() ||
                 this.FacetFieldType == SearchIndexAdditonalFieldType.NumberDecimal.ToString());
            }
        }

        /// <summary>
        /// Gets a value indicating whether date custom ranges should be shown.
        /// </summary>
        public bool ShowDateCustomRanges
        {
            get
            {
                return this.facetField.FacetFieldSettings.RangeType == 1 &&
                this.facetField.FacetFieldSettings.DisplayCustomRange &&
                this.FacetFieldType == SearchIndexAdditonalFieldType.DateAndTime.ToString();
            }
        }
    }
}
