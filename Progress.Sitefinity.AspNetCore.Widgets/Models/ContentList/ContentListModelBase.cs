using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using Progress.Sitefinity.AspNetCore.RestSdk;
using Progress.Sitefinity.AspNetCore.Web;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Classification;
using Progress.Sitefinity.AspNetCore.Widgets.Models.ContentPager;
using Progress.Sitefinity.AspNetCore.Widgets.ViewComponents.Common;
using Progress.Sitefinity.Renderer.Entities.Content;
using Progress.Sitefinity.RestSdk;
using Progress.Sitefinity.RestSdk.Client;
using Progress.Sitefinity.RestSdk.Clients.Pages.Dto;
using Progress.Sitefinity.RestSdk.Dto;
using Progress.Sitefinity.RestSdk.Filters;
using Progress.Sitefinity.RestSdk.OData;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.ContentList
{
    /// <summary>
    /// The base model for the content lists widgets.
    /// </summary>
    public abstract class ContentListModelBase
    {
        /// <summary>
        /// Gets the <see cref="IODataRestClient"/> client.
        /// </summary>
        private protected IODataRestClient RestService { get; private set; }

        /// <summary>
        /// Gets the <see cref="IRequestContext"/> context.
        /// </summary>
        private protected IRequestContext RequestContext { get; private set; }

        /// <summary>
        /// Gets the <see cref="IStyleClassesProvider"/> context.
        /// </summary>
        private protected IStyleClassesProvider StylesProvider { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentListModelBase"/> class.
        /// </summary>
        /// <param name="restService">The HTTP client.</param>
        /// <param name="requestContext">The request context.</param>
        /// <param name="styles">The style classes provider.</param>
        public ContentListModelBase(IODataRestClient restService, IRequestContext requestContext, IStyleClassesProvider styles)
        {
            this.RestService = restService;
            this.RequestContext = requestContext;
            this.StylesProvider = styles;
        }

        /// <summary>
        /// Initializes the view model.
        /// </summary>
        /// <param name="entity">The list entity.</param>
        /// <param name="urlParameters">The url parameters.</param>
        /// <param name="httpContext">The http context.</param>
        /// <returns>The view model of the widget.</returns>
        public abstract Task<object> HandleListView(ContentListEntityBase entity, ReadOnlyCollection<string> urlParameters, HttpContext httpContext);

        private protected static int GetPageValue(ContentListEntityBase entity, ReadOnlyCollection<string> urlParameters, HttpContext httpContext, out IList<string> processedUrlSegments)
        {
            string pageValue = null;

            processedUrlSegments = new List<string>();

            if (urlParameters == null)
                urlParameters = new ReadOnlyCollection<string>(Array.Empty<string>());

            if (entity.PagerMode == PagerMode.URLSegments)
            {
                var template = string.IsNullOrEmpty(entity.PagerTemplate) ? ContentPagerViewModel.PageNumberDefaultTemplate : entity.PagerTemplate;
                var segments = template.Split('/', System.StringSplitOptions.RemoveEmptyEntries);

                foreach (var segment in segments)
                {
                    if (segment.Contains(ContentPagerViewModel.PageNumberSlot, System.StringComparison.InvariantCulture))
                    {
                        var prefix = segment.Split(ContentPagerViewModel.PageNumberSlot)[0];
                        var suffix = segment.Split(ContentPagerViewModel.PageNumberSlot)[1];
                        var pattern = "^" + prefix + "(\\d{1,})" + suffix + "$";
                        var numberSegmentMatches = urlParameters
                            .Select(x => Regex.Match(x, pattern))
                            .Where(m => m.Success);

                        if (numberSegmentMatches.Count() == 1)
                        {
                            pageValue = numberSegmentMatches.Select(m => m.Groups[1]).FirstOrDefault().Value;
                            processedUrlSegments.Add(numberSegmentMatches.Select(m => m.Value).FirstOrDefault());
                        }
                    }
                    else
                    {
                        if (urlParameters.Contains(segment))
                        {
                            processedUrlSegments.Add(segment);
                        }
                    }
                }
            }
            else if (entity.PagerMode == PagerMode.QueryParameter && httpContext != null)
            {
                StringValues queryValue;
                var template = string.IsNullOrEmpty(entity.PagerQueryTemplate) ? ContentPagerViewModel.PageNumberDefaultQueryTemplate : entity.PagerQueryTemplate;
                httpContext.AddVaryByQueryParams(template);
                httpContext.Request.Query.TryGetValue(template, out queryValue);

                if (queryValue.Count > 0)
                    pageValue = queryValue;
            }

            var pageNumber = int.TryParse(pageValue, out int number) ? number : 1;

            return pageNumber;
        }

        private protected static async Task<Tuple<CombinedFilter, IList<string>>> GetClassificationFilter(string typename, ReadOnlyCollection<string> urlParameters, IRestClient restService)
        {
            var processedUrlSegments = new List<string>();
            if (urlParameters == null)
                urlParameters = new ReadOnlyCollection<string>(Array.Empty<string>());

            var template = ClassificationViewModel.ClassificationDefaultUlrSegment;
            CombinedFilter filter = null;
            var prefix = template.Split(ClassificationViewModel.ClassificationSlot)[0];
            var pattern = "(^" + prefix + "((?:\\w|-){1,}),(.+?);?$)+";
            var taxonomySegmentMatches = urlParameters
                .Select(x => Regex.Match(x, pattern))
                .Where(m => m.Success);

            if (taxonomySegmentMatches.Count() == 1)
            {
                filter = new CombinedFilter
                {
                    Operator = CombinedFilter.LogicalOperators.And,
                };

                var taxonomyFilters = taxonomySegmentMatches.First().Value.Split(';');

                var rootIndex = urlParameters.IndexOf(taxonomySegmentMatches.First().Groups[0].Value);
                processedUrlSegments.Add(urlParameters.ElementAt(rootIndex));

                foreach (var taxonomyFilter in taxonomyFilters)
                {
                    var match = Regex.Match(taxonomyFilter, pattern);
                    var taxonomyName = match.Groups[2].Value;
                    var taxaUrlSegments = match.Groups[3].Value;
                    var restClient = restService as RestClient;

                    var taxonUrl = string.Join('/', taxaUrlSegments);
                    Dictionary<string, string> additionalParams = new Dictionary<string, string>
                    {
                        { "@param", $"'{HttpUtility.UrlEncode(taxonUrl)}'" },
                    };
                    var taxonId = await restClient.ExecuteBoundFunction<ODataWrapper<Guid>>(new BoundFunctionArgs
                    {
                        Name = $"Default.GetTaxonByUrl(taxonomyName='{taxonomyName}',taxonUrl=@param)",
                        Type = "taxonomies",
                        AdditionalQueryParams = additionalParams,
                    });

                    var relatedClassificationFieldName = restClient.ServiceMetadata.GetTaxonomyFieldName(typename, taxonomyName);

                    if (relatedClassificationFieldName != null)
                    {
                        filter.ChildFilters.Add(new FilterClause { FieldValue = taxonId.Value.ToString(), Operator = FilterClause.Operators.ContainsOr, FieldName = relatedClassificationFieldName });
                    }
                    else
                    {
                        // in case there is no such field in the selected type - do not filter the widget by this classification
                        break;
                    }
                }
            }

            return new Tuple<CombinedFilter, IList<string>>(filter, processedUrlSegments);
        }

        private protected static void ChangeLogicalOperator(ContentListEntityBase entity, string dynamicFilterByParent, CombinedFilter classificationFilter)
        {
            MixedContentContext mixedContentContext = entity.SelectedItems;
            LogicalOperator logicalOperator = entity.SelectionGroupLogicalOperator;
            string additionalFilter = entity.FilterExpression;

            if (mixedContentContext != null && mixedContentContext.Content != null && mixedContentContext.Content.Length > 0 && mixedContentContext.Content[0].Variations != null)
            {
                for (var i = 0; i < mixedContentContext.Content[0].Variations.Length; i++)
                {
                    var variation = mixedContentContext.Content[0].Variations[i];
                    object complexFilter = null;
                    object idFilter = null;
                    object filter = null;

                    if (!string.IsNullOrEmpty(variation.Filter.Value))
                    {
                        if (variation.Filter.Key == FilterConverter.Types.Complex)
                        {
                            complexFilter = JsonConvert.DeserializeObject<CombinedFilter>(variation.Filter.Value);
                            if (logicalOperator == LogicalOperator.AND)
                            {
                                (complexFilter as CombinedFilter).Operator = CombinedFilter.LogicalOperators.And;
                            }
                            else
                            {
                                (complexFilter as CombinedFilter).Operator = CombinedFilter.LogicalOperators.Or;
                            }
                        }
                        else if (variation.Filter.Key == FilterConverter.Types.Ids)
                        {
                            idFilter = FilterConverter.From(variation.Filter);
                        }
                    }

                    filter = complexFilter ?? idFilter;

                    if (!string.IsNullOrEmpty(additionalFilter))
                    {
                        var deserializedFilter = FilterConverter.From(new KeyValuePair<string, string>("Complex", additionalFilter));
                        if (filter != null)
                        {
                            complexFilter = new CombinedFilter()
                            {
                                Operator = logicalOperator == LogicalOperator.AND ? CombinedFilter.LogicalOperators.And : CombinedFilter.LogicalOperators.Or,
                                ChildFilters = new[] { filter, deserializedFilter },
                            };
                        }
                        else
                        {
                            complexFilter = deserializedFilter;
                        }
                    }

                    if (!string.IsNullOrEmpty(dynamicFilterByParent))
                    {
                        var deserializedDynamicFilter = FilterConverter.From(new KeyValuePair<string, string>("Complex", dynamicFilterByParent));
                        if (filter != null)
                        {
                            complexFilter = new CombinedFilter()
                            {
                                Operator = logicalOperator == LogicalOperator.AND ? CombinedFilter.LogicalOperators.And : CombinedFilter.LogicalOperators.Or,
                                ChildFilters = new[] { filter, deserializedDynamicFilter },
                            };
                        }
                        else
                        {
                            complexFilter = deserializedDynamicFilter;
                        }
                    }

                    if (classificationFilter != null)
                    {
                        if (complexFilter != null)
                        {
                            if (complexFilter is CombinedFilter)
                            {
                                (complexFilter as CombinedFilter).ChildFilters.Add(classificationFilter);
                            }
                            else
                            {
                                complexFilter = new CombinedFilter
                                {
                                    Operator = logicalOperator == LogicalOperator.AND ? CombinedFilter.LogicalOperators.And : CombinedFilter.LogicalOperators.Or,
                                    ChildFilters = new[] { complexFilter, classificationFilter }
                                };
                            }
                        }

                        complexFilter = complexFilter ?? new CombinedFilter { ChildFilters = { classificationFilter }, Operator = CombinedFilter.LogicalOperators.And };

                        if (idFilter != null)
                        {
                            (complexFilter as CombinedFilter).ChildFilters.Add(idFilter);
                        }
                    }

                    if (complexFilter != null)
                        mixedContentContext.Content[0].Variations[i].Filter = new KeyValuePair<string, string>(FilterConverter.Types.Complex, JsonConvert.SerializeObject(complexFilter));
                }
            }
        }

        private protected static void AddOrderByExpression(ContentListEntityBase entity, GetAllArgs getAllArgs)
        {
            if (entity.OrderBy == "Manually")
                return;

            var sortExpression = entity.OrderBy == "Custom" ? entity.SortExpression : entity.OrderBy;

            if (string.IsNullOrEmpty(sortExpression))
                return;

            var sortExpressions = sortExpression.Split(",", StringSplitOptions.RemoveEmptyEntries);
            foreach (var expression in sortExpressions)
            {
                var sortExpressionParts = expression.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                if (sortExpressionParts.Length != 2)
                    continue;

                var sortOrder = sortExpressionParts[1].ToUpperInvariant() == "ASC" ? OrderType.Ascending : OrderType.Descending;
                var orderBy = new OrderBy() { Name = sortExpressionParts[0], Type = sortOrder };
                getAllArgs.OrderBy.Add(orderBy);
            }
        }

        private protected static void AddSkipTake(ContentListEntityBase entity, GetAllArgs getAllArgs, ref int currentPage, int pageNumber)
        {
            if (entity.ListSettings != null)
            {
                switch (entity.ListSettings.DisplayMode)
                {
                    case ListDisplayMode.Paging:
                        currentPage = pageNumber;

                        getAllArgs.Take = entity.ListSettings.ItemsPerPage;

                        if (currentPage > 1)
                        {
                            getAllArgs.Skip = entity.ListSettings.ItemsPerPage * (currentPage - 1);
                        }

                        getAllArgs.Count = true;
                        break;
                    case ListDisplayMode.Limit:
                        getAllArgs.Take = entity.ListSettings.LimitItemsCount;
                        break;
                }
            }
        }

        private protected async Task<object> HandleListViewInternal(ContentListEntityBase entity, ReadOnlyCollection<string> urlParameters, HttpContext httpContext, bool? itemsNotSelected = null)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            if (httpContext == null)
                throw new ArgumentNullException(nameof(httpContext));

            if (urlParameters == null)
                urlParameters = new ReadOnlyCollection<string>(Array.Empty<string>());
            var typename = (this.RestService as RestClient).ServiceMetadata.GetSetNameFromType(entity.SelectedItems?.Content[0]?.Type);

            var pageNumber = GetPageValue(entity, urlParameters, httpContext, out IList<string> processedUrlSegments);
            var (classificationFilter, processedClassificationSegments) = await GetClassificationFilter(typename, urlParameters, this.RestService);
            var viewModel = await this.InitializeViewModel(entity, pageNumber, classificationFilter, itemsNotSelected);
            var listViewModel = viewModel as ContentListCommonViewModel;

            if (listViewModel != null)
            {
                listViewModel.ResolvedUrlSegments = processedClassificationSegments;

                if (listViewModel.Pager != null)
                {
                    listViewModel.Pager.ProcessedUrlSegments = processedUrlSegments;
                    listViewModel.Pager.IsPageNumberValid = listViewModel.Pager.IsPageValid(listViewModel.Pager.CurrentPage);
                }
            }

            return viewModel;
        }

        private protected virtual void ModifyParentFilter(ContentListEntityBase entity)
        {
        }

        private protected virtual void AddSelectExpression(ContentListEntityBase entity, GetAllArgs getAllArgs)
        {
            getAllArgs.Fields.Add("*");
        }

        private protected virtual bool HideListView(ContentListEntityBase entityBase, string type)
        {
            return false;
        }

        private protected virtual bool ShowDetailsViewOnChildDetailsView(ContentListEntityBase entityBase)
        {
            return false;
        }

        private protected virtual bool ShowListViewOnEmptyParentFilter(ContentListEntityBase entityBase)
        {
            return false;
        }

        private protected virtual ContentListCommonViewModel PopulateViewModel(ContentListEntityBase entityBase)
        {
            return new ContentListCommonViewModel();
        }

        private protected async Task<object> InitializeViewModel(ContentListEntityBase entity, int pageNumber, CombinedFilter classificationFilter, bool? itemsNotSelected = null)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            bool hasSelectedItemsType;

            if (itemsNotSelected != null)
                hasSelectedItemsType = (bool)!itemsNotSelected;
            else
                hasSelectedItemsType = entity.SelectedItems != null && entity.SelectedItems.Content != null && entity.SelectedItems.Content.Length > 0 && !string.IsNullOrEmpty(entity.SelectedItems.Content[0].Type);

            var variation = hasSelectedItemsType ? entity.SelectedItems.Content[0]?.Variations?.FirstOrDefault() : null;
            var selectedItemsType = hasSelectedItemsType ? entity.SelectedItems.Content[0].Type : string.Empty;
            string filterByParentExpressionSerialized = null;

            var viewModel = this.PopulateViewModel(entity);
            var currentPage = 1;

            if (this.HideListView(entity, selectedItemsType))
                return new ContentListViewModel();

            var getAllArgs = new GetAllArgs();
            if (entity.ContentViewDisplayMode == ContentViewDisplayMode.Detail)
                getAllArgs = this.ConstructGetAllArgs(entity, ref currentPage, pageNumber, filterByParentExpressionSerialized, classificationFilter);
            var contentListModelForDetail = new ContentListModelForDetail(this.RestService, this.RequestContext, this.StylesProvider);
            contentListModelForDetail.GetAllArgs = getAllArgs;
            var detailItemResult = await contentListModelForDetail.HandleDetailItem(entity, this.ShowDetailsViewOnChildDetailsView(entity));
            if (detailItemResult != null)
                return detailItemResult;

            // handle parent filtering
            var hasSelectedDynamicParentFiltering = IsSelectedDynamicParentFiltering(variation);
            if (hasSelectedDynamicParentFiltering)
            {
                var parentType = (this.RestService as RestClient).ServiceMetadata.GetParentType(selectedItemsType);
                var item = this.RequestContext.Model.DetailItem;

                if (parentType != null && item != null && item.ItemType == parentType)
                {
                    var filterByParentExpression = new FilterClause()
                    {
                        FieldName = "ParentId",
                        FieldValue = item.Id,
                        Operator = FilterClause.Operators.Equal,
                    };

                    filterByParentExpressionSerialized = JsonConvert.SerializeObject(filterByParentExpression);
                }
                else if (!this.ShowListViewOnEmptyParentFilter(entity))
                {
                    return viewModel;
                }
            }

            if (hasSelectedItemsType || hasSelectedDynamicParentFiltering)
            {
                getAllArgs = this.ConstructGetAllArgs(entity, ref currentPage, pageNumber, filterByParentExpressionSerialized, classificationFilter);
                var result = await this.RestService.GetItems<SdkItem>(entity.SelectedItems, getAllArgs).ConfigureAwait(false);
                viewModel.Items = result.Items;

                // handle paging and detail page
                if (entity.ListSettings?.DisplayMode == ListDisplayMode.Paging)
                {
                    var pagerTemplate = string.IsNullOrEmpty(entity.PagerTemplate) ? ContentPagerViewModel.PageNumberDefaultTemplate : entity.PagerTemplate;
                    var pagerQueryTemplate = string.IsNullOrEmpty(entity.PagerQueryTemplate) ? ContentPagerViewModel.PageNumberDefaultQueryTemplate : entity.PagerQueryTemplate;
                    viewModel.Pager = new ContentPagerViewModel(currentPage, (int)result.TotalCount, entity.ListSettings.ItemsPerPage, pagerTemplate, pagerQueryTemplate, entity.PagerMode);
                    viewModel.Pager.ViewUrl = this.RequestContext.PageNode?.ViewUrl;
                }

                if (entity.DetailPageMode == DetailPageSelectionMode.ExistingPage)
                {
                    var pageNodes = await this.RestService.GetItems<PageNodeDto>(entity.DetailPage, new GetAllArgs() { Fields = new[] { nameof(PageNodeDto.ViewUrl) } });
                    var items = pageNodes.Items;
                    if (items.Count == 1)
                        viewModel.DetailItemUrl = new Uri(items[0].ViewUrl, UriKind.RelativeOrAbsolute);
                    else
                        viewModel.RenderLinks = false;
                }
            }

            return viewModel;
        }

        private static bool IsSelectedDynamicParentFiltering(ContentVariation variation)
        {
            if (variation == null)
                return false;

            if (variation.DynamicFilterByParent != true)
                return false;

            return true;
        }

        private GetAllArgs ConstructGetAllArgs(ContentListEntityBase entity, ref int currentPage, int pageNumber, string filterByParentExpressionSerialized, CombinedFilter classificationFilter)
        {
            var getAllArgs = new GetAllArgs();

            ContentListModelBase.AddOrderByExpression(entity, getAllArgs);

            ContentListModelBase.AddSkipTake(entity, getAllArgs, ref currentPage, pageNumber);

            this.AddSelectExpression(entity, getAllArgs);

            ContentListModelBase.ChangeLogicalOperator(entity, filterByParentExpressionSerialized, classificationFilter);

            this.ModifyParentFilter(entity);

            return getAllArgs;
        }
    }
}
