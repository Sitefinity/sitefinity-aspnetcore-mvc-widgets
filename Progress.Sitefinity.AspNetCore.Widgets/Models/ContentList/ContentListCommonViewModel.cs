using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Progress.Sitefinity.AspNetCore.Widgets.Models.ContentPager;
using Progress.Sitefinity.RestSdk.Dto;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.ContentList
{
    /// <summary>
    /// The common view model for content lists.
    /// </summary>
    public class ContentListCommonViewModel : ContentListViewModelBase
    {
        /// <summary>
        /// Gets or sets the default item url.
        /// </summary>
        public Uri DetailItemUrl { get; set; }

        /// <summary>
        /// Gets or sets the list of items.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "DTO")]
        public IList<SdkItem> Items { get; set; } = new List<SdkItem>();

        /// <summary>
        /// Gets or sets the pager view model.
        /// </summary>
        public ContentPagerViewModel Pager { get; set; }

        /// <summary>
        /// Gets or sets the additional url segments to be marked as resolved.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "By design.")]
        public IList<string> ResolvedUrlSegments { get; set; }

        /// <summary>
        /// Gets or sets the css classes for the list view.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "By design")]
        public IList<CssFieldMapping> CssClasses { get; set; }

        /// <summary>
        /// Gets the field value.
        /// </summary>
        /// <typeparam name="T">The value type.</typeparam>
        /// <param name="item">The item.</param>
        /// <param name="listFieldName">The list field name, as displayed in the widget editor.</param>
        /// <returns>The field value.</returns>
        public virtual T GetFieldValue<T>(SdkItem item, string listFieldName)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            T value;
            if (listFieldName != null)
            {
                item.TryGetValue<T>(listFieldName, out value);
                return value;
            }

            return default(T);
        }

        /// <summary>
        /// Gets the CSS class of a field.
        /// </summary>
        /// <param name="fieldName">The field name.</param>
        /// <returns>The CSS class.</returns>
        public string GetFieldCss(string fieldName)
        {
            return this.CssClasses?.FirstOrDefault(x => x.FieldName == fieldName)?.CssClass;
        }

        /// <summary>
        /// Attempts to build the url of the item if it has the property ItemDefaultUrl.
        /// </summary>
        /// <param name="httpContext">The http context.</param>
        /// <param name="item">The sdk item.</param>
        /// <returns>The url.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "used from views")]
        public Uri GetItemUrl(HttpContext httpContext, SdkItem item)
        {
            if (httpContext == null)
                throw new ArgumentNullException(nameof(httpContext));

            if (item == null)
                throw new ArgumentNullException(nameof(item));

            string currentUrl = null;
            if (this.DetailItemUrl.IsAbsoluteUri)
            {
                currentUrl = this.DetailItemUrl.GetComponents(UriComponents.Path, UriFormat.SafeUnescaped);
            }
            else
            {
                var stringifiedUrl = this.DetailItemUrl.ToString();
                var questionMarkIndex = stringifiedUrl.IndexOf("?", StringComparison.OrdinalIgnoreCase);
                if (questionMarkIndex != -1)
                    currentUrl = stringifiedUrl.Substring(0, questionMarkIndex);
                else
                    currentUrl = stringifiedUrl;
            }

            var pagerTemplate = this.Pager != null && this.Pager.PagerMode == PagerMode.URLSegments ? this.Pager.PagerSegmentTemplate : null;
            if (!string.IsNullOrEmpty(pagerTemplate))
            {
                if (this.Pager.IsSegmentMatch(currentUrl, out string pattern))
                {
                    currentUrl = Regex.Replace(currentUrl, pattern, string.Empty);
                }
            }

            if (currentUrl.EndsWith("/", StringComparison.OrdinalIgnoreCase))
                currentUrl = currentUrl.Substring(0, currentUrl.Length - 1);

            if (item.TryGetValue<string>("ItemDefaultUrl", out string defaultUrl))
            {
                if (!defaultUrl.StartsWith("/", StringComparison.OrdinalIgnoreCase))
                    defaultUrl = "/" + defaultUrl;

                if (!currentUrl.StartsWith("/", StringComparison.OrdinalIgnoreCase))
                    currentUrl = "/" + currentUrl;

                currentUrl += defaultUrl;
                return new Uri(currentUrl + httpContext.Request.QueryString.ToString(), UriKind.Relative);
            }

            return null;
        }
    }
}
