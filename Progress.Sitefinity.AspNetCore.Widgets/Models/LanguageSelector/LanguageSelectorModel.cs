using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Http;
using Progress.Sitefinity.AspNetCore.Web;
using Progress.Sitefinity.AspNetCore.Widgets.ViewComponents.Common;
using Progress.Sitefinity.RestSdk;
using Progress.Sitefinity.RestSdk.Clients.Pages.Dto;
using Progress.Sitefinity.RestSdk.Dto;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.LanguageSelector
{
    /// <summary>
    /// The model for the Language selector widget.
    /// </summary>
    public class LanguageSelectorModel : ILanguageSelectorModel
    {
        private const string PreviewUrlQueryParam = "sfaction=preview";

        // In legacy MVC pages a Path is used
        private const string PreviewUrlPath = "/Action/Preview";
        private readonly IStyleClassesProvider styles;
        private IRequestContext requestContext;
        private IRestClient restClient;
        private IHttpContextAccessor httpContextAccessor;

        /// <summary>Initializes a new instance of the <see cref="LanguageSelectorModel" /> class.</summary>
        /// <param name="requestContext">The request context.</param>
        /// <param name="restClient">The rest client.</param>
        /// <param name="styles">The styles provider.</param>
        /// <param name="httpContextAccessor">The http context accessor.</param>
        public LanguageSelectorModel(
            IRequestContext requestContext,
            IRestClient restClient,
            IStyleClassesProvider styles,
            IHttpContextAccessor httpContextAccessor)
        {
            this.requestContext = requestContext;
            this.restClient = restClient;
            this.styles = styles;
            this.httpContextAccessor = httpContextAccessor;
        }

        /// <summary>Initializes the view model.</summary>
        /// <param name="entity">The entity.</param>
        /// <returns>
        /// An instance of <see cref="LanguageSelectorViewModel" /> class.
        /// </returns>
        public async Task<LanguageSelectorViewModel> InitializeViewModel(LanguageSelectorEntity entity)
        {
            var viewModel = new LanguageSelectorViewModel();
            var margins = this.styles.GetMarginsClasses(entity);
            viewModel.CssClass = (entity.CssClass + " " + margins).Trim();
            viewModel.ViewName = entity.SfViewName;
            viewModel.LanguageSelectorLinkAction = entity.LanguageSelectorLinkAction;
            viewModel.Attributes = entity.Attributes;

            if (this.IsPageRequest())
            {
                await this.AddLanguageEntriesToViewModel(viewModel, entity);
                await this.AddHomePageRedirectUrls(viewModel, entity);
                await this.AddDetailItemUrlSegments(viewModel);
                this.AddQueryParameters(viewModel);
            }

            return viewModel;
        }

        /// <summary>
        /// Determines whether the current request is a page request.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if the current request is a page request; otherwise, <c>false</c>.</returns>
        public bool IsPageRequest()
        {
            if (this.requestContext.PageNode != null)
            {
                return true;
            }

            return false;
        }

        private static string GetLanguageName(CultureInfo culture, LanguageSelectorDisplayFormat format)
        {
            if (culture == null)
            {
                return null;
            }

            switch (format)
            {
                case LanguageSelectorDisplayFormat.English:
                    return culture.EnglishName;
                case LanguageSelectorDisplayFormat.Native:
                    return culture.NativeName;
                case LanguageSelectorDisplayFormat.NativeCapitalized:
                    return !string.IsNullOrEmpty(culture.NativeName) ?
                        char.ToUpper(culture.NativeName[0]) + culture.NativeName.Substring(1) : null;
            }

            return culture.Name;
        }

        private async Task AddLanguageEntriesToViewModel(LanguageSelectorViewModel viewModel, LanguageSelectorEntity entity)
        {
            var cultures = this.requestContext.Site.Cultures;
            var culturePageMap = new Dictionary<string, PageNodeDto>();
            if (this.requestContext.PageNode != null)
            {
                var pageCultures = this.requestContext.PageNode.AvailableLanguages;
                culturePageMap = await this.GetItemsForCultures<PageNodeDto>(pageCultures, new GetItemArgs()
                {
                    Id = this.requestContext.PageNode.Id,
                    Provider = this.requestContext.PageNode.Provider
                });
            }

            foreach (var culture in cultures)
            {
                var ci = CultureInfo.GetCultureInfo(culture.Name);
                var entry = new LanguageEntry()
                {
                    Name = GetLanguageName(ci, entity.LanguageSelectorDisplayFormat),
                    Value = ci.Name,
                    Selected = ci.Name == this.requestContext.Culture.Name
                };

                if (culturePageMap.TryGetValue(culture.Name, out PageNodeDto pageNode) &&
                    pageNode != null &&
                    !pageNode.ViewUrl.Contains(PreviewUrlQueryParam, StringComparison.InvariantCultureIgnoreCase))
                {
                    entry.IsTranslated = true;
                    entry.PageUrl = pageNode.ViewUrl;
                }
                else
                {
                    entry.IsTranslated = false;
                    entry.PageUrl = null;
                }

                viewModel.Languages.Add(entry);
            }
        }

        private async Task AddHomePageRedirectUrls(LanguageSelectorViewModel viewModel, LanguageSelectorEntity entity)
        {
            if (entity.LanguageSelectorLinkAction != MissingTranslationAction.RedirectToHomePage)
            {
                return;
            }

            var entriesWithoutTranslation = viewModel.Languages.Where(e => !e.IsTranslated);
            IDictionary<string, string> homePageUrlsByCulture = await this.GetHomePageUrlsByCulture(entriesWithoutTranslation.Select(e => e.Value));
            foreach (var entry in entriesWithoutTranslation)
            {
                var culture = entry.Value;
                entry.LocalizedHomePageUrl = "/";
                if (homePageUrlsByCulture.ContainsKey(culture))
                {
                    entry.LocalizedHomePageUrl = homePageUrlsByCulture[culture];
                }
            }
        }

        private async Task<IDictionary<string, string>> GetHomePageUrlsByCulture(IEnumerable<string> cultures)
        {
            Guid homePageId = (await this.restClient.Sites().GetCurrentSite()).HomePageId;
            Dictionary<string, PageNodeDto> culturePageMap = await this.GetItemsForCultures<PageNodeDto>(cultures, new GetItemArgs()
            {
                Id = homePageId.ToString(),
                Provider = this.requestContext.PageNode.Provider
            });

            IDictionary<string, string> result = new Dictionary<string, string>();
            foreach (var culture in cultures)
            {
                if (culturePageMap.TryGetValue(culture, out PageNodeDto pageNode) &&
                    pageNode != null &&
                    pageNode.AvailableLanguages != null &&
                    pageNode.AvailableLanguages.Contains(culture) &&
                    !pageNode.ViewUrl.Contains(PreviewUrlQueryParam, StringComparison.InvariantCultureIgnoreCase) &&
                    !pageNode.ViewUrl.Contains(PreviewUrlPath, StringComparison.InvariantCultureIgnoreCase))
                {
                    result[culture] = pageNode.ViewUrl;
                }
            }

            return result;
        }

        private async Task AddDetailItemUrlSegments(LanguageSelectorViewModel viewModel)
        {
            if (this.requestContext.ResolvedDetailItem != null)
            {
                var translatedEntries = viewModel.Languages.Where(l => l.IsTranslated);
                var cultures = translatedEntries.Select(e => e.Value);
                IDictionary<string, string> detailItemUrlsByCulture = await this.GetDetailItemUrlsByCulture(cultures);

                foreach (var language in translatedEntries)
                {
                    if (detailItemUrlsByCulture.ContainsKey(language.Value))
                    {
                        var urlParts = language.PageUrl.Split(['?'], StringSplitOptions.RemoveEmptyEntries);
                        string basePath = urlParts[0];
                        string query = urlParts.Length > 1 ? $"?{urlParts[1]}" : string.Empty;

                        language.PageUrl = $"{basePath.TrimEnd('/')}/{detailItemUrlsByCulture[language.Value].TrimStart('/')}{query}";
                    }
                }
            }
        }

        private async Task<IDictionary<string, string>> GetDetailItemUrlsByCulture(IEnumerable<string> cultures)
        {
            Dictionary<string, SdkItem> cultureItemMap = await this.GetItemsForCultures<SdkItem>(cultures, new GetItemArgs()
            {
                Id = this.requestContext.Model.DetailItem.Id,
                Type = this.requestContext.Model.DetailItem.ItemType,
                Provider = this.requestContext.Model.DetailItem.ProviderName
            });

            IDictionary<string, string> result = new Dictionary<string, string>();
            foreach (var culture in cultures)
            {
                if (cultureItemMap.TryGetValue(culture, out SdkItem sdkItem) &&
                    sdkItem != null)
                {
                    result[culture] = sdkItem.GetValue<string>("ItemDefaultUrl");
                }
            }

            return result;
        }

        private void AddQueryParameters(LanguageSelectorViewModel viewModel)
        {
            foreach (var language in viewModel.Languages.Where(l => l.IsTranslated))
            {
                var urlParts = language.PageUrl.Split(['?'], StringSplitOptions.RemoveEmptyEntries);
                string existingQuery = urlParts.Length > 1 ? urlParts[1] : string.Empty;
                var query = HttpUtility.ParseQueryString(existingQuery);
                foreach (var queryItem in this.httpContextAccessor.HttpContext.Request.Query)
                {
                    query.Add(queryItem.Key, queryItem.Value);
                }

                var newQueryString = query.Count > 0 ? $"?{query.ToString()}" : string.Empty;
                language.PageUrl = $"{urlParts[0]}{newQueryString}";
            }
        }

        private async Task<Dictionary<string, T>> GetItemsForCultures<T>(IEnumerable<string> cultures, GetItemArgs args)
            where T : class
        {
            var tasks = cultures.Distinct().Select(async culture =>
            {
                // Make a fresh copy of args so we don't mutate the shared one
                var localArgs = new GetItemArgs()
                {
                    Id = args.Id,
                    Type = args.Type,
                    Provider = args.Provider,
                    Fields = args.Fields,
                    AdditionalHeaders = args.AdditionalHeaders,
                    AdditionalQueryParams = args.AdditionalQueryParams,
                    Culture = culture,
                };

                try
                {
                    var item = await this.restClient.GetItem<T>(localArgs);
                    return new KeyValuePair<string, T>(culture, item);
                }
                catch
                {
                    return new KeyValuePair<string, T>(culture, null);
                }
            });

            var results = await Task.WhenAll(tasks);

            return results.ToDictionary(kv => kv.Key, kv => kv.Value);
        }
    }
}
