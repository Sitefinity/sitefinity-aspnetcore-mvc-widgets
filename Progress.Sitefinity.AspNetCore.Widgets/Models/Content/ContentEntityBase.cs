using System.ComponentModel;
using Progress.Sitefinity.AspNetCore.Models;
using Progress.Sitefinity.Renderer.Designers;
using Progress.Sitefinity.RestSdk.Dto;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Content
{
    /// <summary>
    /// The base class for content enities.
    /// </summary>
    public class ContentEntityBase
    {
        /// <summary>
        /// Gets or sets the image.
        /// </summary>
        public virtual SdkItem Item { get; set; }

        /// <summary>
        /// Gets or sets the CSS class of the image.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [DisplayName("CSS class")]
        public string CssClass { get; set; }

        /////// <summary>
        /////// Gets or sets a value indicating whether the canonical URL tag should be added to the page when the canonical meta tag should be added to the page.
        /////// If the value is not set, the settings from SystemConfig -> ContentLocationsSettings -> DisableCanonicalURLs will be used.
        /////// </summary>
        /////// <value>The disable canonical URLs.</value>
        ////[Category(PropertyCategory.Advanced)]
        ////[DisplayName("Disable canonical URL meta tag")]
        ////public bool? DisableCanonicalUrlMetaTag { get; set; }
    }
}
