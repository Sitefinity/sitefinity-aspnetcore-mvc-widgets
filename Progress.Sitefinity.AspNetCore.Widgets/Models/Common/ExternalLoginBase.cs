using System;
using System.Collections.Generic;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Primitives;
using Progress.Sitefinity.AspNetCore.Widgets.Models.LoginForm.Dto;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Common
{
    /// <summary>
    /// Base class for login with external providers.
    /// </summary>
    public abstract class ExternalLoginBase
    {
        /// <summary>
        /// The error query key.
        /// </summary>
        public const string ErrorQueryKey = "loginerror";

        /// <summary>
        /// The show success message query key.
        /// </summary>
        public const string ShowSuccessMessageQueryKey = "showSuccessMessage";

        /// <summary>
        /// Gets or sets the external providers.
        /// </summary>
        /// <value>
        /// The external providers.
        /// </value>
        public IEnumerable<ExternalProviderItemDto> ExternalProviders { get; set; }

        /// <summary>
        /// Gets or sets the external login handler path.
        /// </summary>
        /// <value>
        /// The external login handler path.
        /// </value>
        public string ExternalLoginHandlerPath { get; set; }

        /// <summary>
        /// Gets or sets the redirect url.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1056:URI-like properties should not be strings", Justification = "Reviewed.")]
        public string RedirectUrl { get; set; }

        /// <summary>
        /// Gets or sets the error redirect url.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1056:URI-like properties should not be strings", Justification = "Reviewed.")]
        public string ErrorRedirectUrl { get; set; }

        /// <summary>
        /// Gets a value indicating whether to show success message query string parameter.
        /// </summary>
        protected virtual bool ShowSuccessMessageQueryParameter
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Checks if the request should prompt error in UI.
        /// </summary>
        /// <param name="context">The http context of the request.</param>
        /// <returns>True if error should show.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1822: Mark members as static", Justification = "Reviewed.")]
        public bool IsError(HttpContext context)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.Request.Query.TryGetValue(ErrorQueryKey, out StringValues value))
            {
                return value[0] == bool.TrueString;
            }

            return false;
        }

        /// <summary>
        /// Checks if the request should prompt success message in UI.
        /// </summary>
        /// <param name="context">The http context of the request.</param>
        /// <returns>True if success message should show.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1822: Mark members as static", Justification = "Reviewed.")]
        public bool ShowSuccessMessage(HttpContext context)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.Request.Query.TryGetValue(ShowSuccessMessageQueryKey, out StringValues value))
            {
                return value[0] == bool.TrueString;
            }

            return false;
        }

        /// <summary>
        /// Gets the default return url.
        /// </summary>
        /// <param name="context">The current request context.</param>
        /// <param name="isError">if set to <c>true</c> isError query parameter is set to true.</param>
        /// <param name="shouldEncode">if set to <c>true</c> [should encode].</param>
        /// <returns>
        /// The encoded default url.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1055:URI-like return values should not be strings", Justification = "Reviewed.")]
        public string GetDefaultReturnUrl(HttpContext context, bool isError, bool shouldEncode)
        {
            if (!string.IsNullOrWhiteSpace(this.ErrorRedirectUrl) && isError)
            {
                return this.ErrorRedirectUrl;
            }

            if (!string.IsNullOrWhiteSpace(this.RedirectUrl) && !isError)
            {
                return this.RedirectUrl;
            }

            var redirectUrlBuilder = new UriBuilder(context?.Request.GetDisplayUrl());
            var currentRequestBuilder = new UriBuilder(context?.Request.GetDisplayUrl());
            var currentRequestQuery = HttpUtility.ParseQueryString(currentRequestBuilder.Query);
            currentRequestQuery.Remove(ErrorQueryKey);
            currentRequestQuery.Remove(ShowSuccessMessageQueryKey);

            if (isError)
            {
                currentRequestQuery[ErrorQueryKey] = isError.ToString();
            }
            else if (this.ShowSuccessMessageQueryParameter)
            {
                currentRequestQuery[ShowSuccessMessageQueryKey] = true.ToString();
            }

            redirectUrlBuilder.Query = currentRequestQuery.ToString();
            var result = redirectUrlBuilder.ToString();
            if (shouldEncode)
            {
                result = HttpUtility.UrlEncode(result);
            }

            return result;
        }

        /// <summary>
        /// Gets the default return url.
        /// </summary>
        /// <param name="context">The current request context.</param>
        /// <param name="provider">The provider.</param>
        /// <returns>
        /// The encoded default url.
        /// </returns>
        public string GetExternalLoginPath(HttpContext context, string provider)
        {
            return this.ExternalLoginHandlerPath + "?provider=" + provider + "&returnUrl=" + this.GetDefaultReturnUrl(context, false, true) + "&errorUrl=" + this.GetDefaultReturnUrl(context, true, true);
        }

        /// <summary>
        /// Gets the CSS class for the current external provider login button.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <returns>
        /// The current provider CSS class.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1308:Normalize strings to uppercase", Justification = "Invalid in this case")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Invalid in this case")]
        public string GetExternalLoginButtonCssClass(string provider)
        {
            if (string.IsNullOrEmpty(provider))
                throw new ArgumentException("Invalid value for provider", nameof(provider));
            return "-sf-" + provider.ToLowerInvariant() + "-button";
        }
    }
}
