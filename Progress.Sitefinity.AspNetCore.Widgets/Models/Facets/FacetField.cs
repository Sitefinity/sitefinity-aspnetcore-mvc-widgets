using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Progress.Sitefinity.Renderer.Designers;
using Progress.Sitefinity.Renderer.Designers.Attributes;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Facets
{
    /// <summary>
    /// Facet field model.
    /// </summary>
    public class FacetField
    {
        /// <summary>
        /// Gets or sets the additional facetable fieldSearchFacetsQueryStringProcessors names.
        /// </summary>
        [DisplayName("Field")]
        [DataType(customDataType: "facetTaxa")]
        [DefaultValue(new string[0])]
        public string[] FacetableFieldNames { get; set; }

        /// <summary>
        /// Gets or sets the additional facetable fields labels.
        /// </summary>
        [Description("[{\"Type\":1,\"Chunks\":[{\"Value\":\"Add a name of the facetable field that is\",\"Presentation\":[]},{\"Value\":\"visible on your site.\",\"Presentation\":[]}]}]")]
        [DisplayName("Label")]
        [DefaultValue("")]
        public string FacetableFieldLabels { get; set; }

        /// <summary>
        /// Gets or sets the facet field settings.
        /// </summary>
        [DisplayName("Configuration")]
        [DataType(KnownFieldTypes.PencilButton)]
        [Dialog("{\"buttons\":[{\"type\":\"confirm\", \"title\":\"Save\"}, {\"type\":\"cancel\", \"title\":\"Cancel\"}], \"urlKey\":\"settings\"}")]
        public Settings FacetFieldSettings
        {
            get
            {
                if (this.facetFieldSettings == null)
                {
                    this.facetFieldSettings = new Settings();
                }

                return this.facetFieldSettings;
            }

            set
            {
                this.facetFieldSettings = value;
            }
        }

        private Settings facetFieldSettings;
    }
}
