using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using Progress.Sitefinity.AspNetCore.Configuration;
using Progress.Sitefinity.AspNetCore.RestSdk;
using Progress.Sitefinity.AspNetCore.Web;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Common;
using Progress.Sitefinity.AspNetCore.Widgets.Models.ContentPager;
using Progress.Sitefinity.Clients.LayoutService.Dto;
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
    /// The model for the Content list widget.
    /// </summary>
    public class ContentListModel : IContentListModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContentListModel"/> class.
        /// </summary>
        /// <param name="restService">The HTTP client.</param>
        /// <param name="requestContext">The request context.</param>
        public ContentListModel(IODataRestClient restService, IRequestContext requestContext)
        {
            this.restService = restService;
            this.requestContext = requestContext;
        }

        /// <inheritdoc/>
        public virtual async Task<object> HandleListView(ContentListEntity entity, ReadOnlyCollection<string> urlParameters, HttpContext httpContext)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            if (httpContext == null)
                throw new ArgumentNullException(nameof(httpContext));

            if (urlParameters == null)
                urlParameters = new ReadOnlyCollection<string>(Array.Empty<string>());

            var pageNumber = GetPageValue(entity, urlParameters, httpContext, out IList<string> processedUrlSegments);
            var viewModel = await this.InitializeViewModel(entity, httpContext.Request.Query, pageNumber);
            var listViewModel = viewModel as ContentListViewModel;

            if (listViewModel != null && listViewModel.Pager != null)
            {
                listViewModel.Pager.ProcessedUrlSegments = processedUrlSegments;
                listViewModel.Pager.IsPageNumberValid = listViewModel.Pager.IsPageValid(listViewModel.Pager.CurrentPage);
            }

            return viewModel;
        }

        private static int GetPageValue(ContentListEntity entity, ReadOnlyCollection<string> urlParameters, HttpContext httpContext, out IList<string> processedUrlSegments)
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

        private static void ChangeLogicalOperator(MixedContentContext mixedContentContext, LogicalOperator logicalOperator, string additionalFilter, string dynamicFilterByParent)
        {
            if (mixedContentContext != null && mixedContentContext.Content != null && mixedContentContext.Content.Length > 0 && mixedContentContext.Content[0].Variations != null)
            {
                for (var i = 0; i < mixedContentContext.Content[0].Variations.Length; i++)
                {
                    var variation = mixedContentContext.Content[0].Variations[i];
                    object complexFilter = null;
                    if (variation.Filter.Key == FilterConverter.Types.Complex && !string.IsNullOrEmpty(variation.Filter.Value))
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

                    if (!string.IsNullOrEmpty(additionalFilter))
                    {
                        var deserializedFilter = FilterConverter.From(new KeyValuePair<string, string>("Complex", additionalFilter));
                        if (complexFilter != null)
                        {
                            complexFilter = new CombinedFilter()
                            {
                                Operator = (complexFilter as CombinedFilter).Operator,
                                ChildFilters = new[] { complexFilter, deserializedFilter },
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
                        if (complexFilter != null)
                        {
                            complexFilter = new CombinedFilter()
                            {
                                Operator = logicalOperator == LogicalOperator.AND ? CombinedFilter.LogicalOperators.And : CombinedFilter.LogicalOperators.Or,
                                ChildFilters = new[] { complexFilter, deserializedDynamicFilter },
                            };
                        }
                        else
                        {
                            complexFilter = deserializedDynamicFilter;
                        }
                    }

                    if (complexFilter != null)
                        mixedContentContext.Content[0].Variations[i].Filter = new KeyValuePair<string, string>(FilterConverter.Types.Complex, JsonConvert.SerializeObject(complexFilter));
                }
            }
        }

        private static OrderBy GetOrderByExpression(ContentListEntity entity)
        {
            if (entity.OrderBy == "Manually")
                return null;

            var sortExpression = entity.OrderBy == "Custom" ? entity.SortExpression : entity.OrderBy;

            if (string.IsNullOrEmpty(sortExpression))
                return null;

            var sortExpressionParts = sortExpression.Split(" ");
            if (sortExpressionParts.Length != 2)
                return null;

            var sortOrder = sortExpressionParts[1].ToUpperInvariant() == "ASC" ? OrderType.Ascending : OrderType.Descending;
            var orderBy = new OrderBy() { Name = sortExpressionParts[0], Type = sortOrder };

            return orderBy;
        }

        private static bool IsSelectedDynamicParentFiltering(ContentVariation variation)
        {
            if (variation == null)
                return false;

            if (variation.DynamicFilterByParent != true)
                return false;

            return true;
        }

        private async Task<object> InitializeViewModel(ContentListEntity entity, IQueryCollection query, int pageNumber)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var hasSelectedItemsType = entity.SelectedItems != null && entity.SelectedItems.Content != null && entity.SelectedItems.Content.Length > 0 && !string.IsNullOrEmpty(entity.SelectedItems.Content[0].Type);
            var variation = hasSelectedItemsType ? entity.SelectedItems.Content[0].Variations.FirstOrDefault() : null;
            var selectedItemsType = hasSelectedItemsType ? entity.SelectedItems.Content[0].Type : string.Empty;
            string filterByParentExpressionSerialized = null;

            if (this.HideListView(entity, selectedItemsType))
                return new ContentListViewModel();

            var detailItemResult = await new ContentListModelForDetail(this.restService, this.requestContext).HandleDetailItem(entity, query);
            if (detailItemResult != null)
                return detailItemResult;

            var viewModel = new ContentListViewModel();
            viewModel.RenderLinks = !(entity.ContentViewDisplayMode == ContentViewDisplayMode.Master && entity.DetailPageMode == DetailPageSelectionMode.SamePage);
            viewModel.ListFieldMapping = entity.ListFieldMapping;
            viewModel.CssClasses = entity.CssClasses;
            viewModel.DetailItemUrl = new Uri(this.requestContext.PageNode.ViewUrl, UriKind.RelativeOrAbsolute);

            var showPager = false;
            var currentPage = 1;

            var hasSelectedDynamicParentFiltering = IsSelectedDynamicParentFiltering(variation);
            if (hasSelectedDynamicParentFiltering)
            {
                var parentType = (this.restService as RestClient).ServiceMetadata.GetParentType(selectedItemsType);
                var item = this.requestContext.Model.DetailItem;

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
                else if (!entity.ShowListViewOnEmptyParentFilter)
                {
                    return viewModel;
                }
            }

            if (hasSelectedItemsType || hasSelectedDynamicParentFiltering)
            {
                var getAllArgs = new GetAllArgs();

                var orderBy = ContentListModel.GetOrderByExpression(entity);
                if (orderBy != null)
                {
                    getAllArgs.OrderBy.Add(orderBy);
                }

                if (entity.ListSettings != null)
                {
                    switch (entity.ListSettings.DisplayMode)
                    {
                        case ListDisplayMode.Paging:
                            showPager = true;
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

                getAllArgs.Fields.Add("*");

                ChangeLogicalOperator(entity.SelectedItems, entity.SelectionGroupLogicalOperator, entity.FilterExpression, filterByParentExpressionSerialized);
                var result = await this.restService.GetItems<SdkItem>(entity.SelectedItems, getAllArgs).ConfigureAwait(false);
                viewModel.Items = result.Items;

                if (showPager)
                {
                    var pagerTemplate = string.IsNullOrEmpty(entity.PagerTemplate) ? ContentPagerViewModel.PageNumberDefaultTemplate : entity.PagerTemplate;
                    var pagerQueryTemplate = string.IsNullOrEmpty(entity.PagerQueryTemplate) ? ContentPagerViewModel.PageNumberDefaultQueryTemplate : entity.PagerQueryTemplate;
                    viewModel.Pager = new ContentPagerViewModel(currentPage, (int)result.TotalCount, entity.ListSettings.ItemsPerPage, pagerTemplate, pagerQueryTemplate, entity.PagerMode);
                }

                if (entity.DetailPageMode == DetailPageSelectionMode.ExistingPage)
                {
                    var pageNodes = await this.restService.GetItems<PageNodeDto>(entity.DetailPage, new GetAllArgs() { Fields = new[] { nameof(PageNodeDto.ViewUrl) } });
                    var items = pageNodes.Items;
                    if (items.Count == 1)
                        viewModel.DetailItemUrl = new Uri(items[0].ViewUrl, UriKind.RelativeOrAbsolute);
                }
            }

            return viewModel;
        }

        private bool HideListView(ContentListEntity entity, string type)
        {
            if (!string.IsNullOrEmpty(type) && !entity.ShowListViewOnChildDetailsView)
            {
                // get details item
                var detailItem = this.requestContext.Model.DetailItem;
                if (detailItem != null)
                {
                    // check whether the item is from a child type
                    var childTypes = (this.restService as RestClient).ServiceMetadata.GetChildTypes(type).SelectMany(x => x);

                    return childTypes.Any(x => x == detailItem.ItemType);
                }
            }

            return false;
        }

        private IODataRestClient restService;
        private IRequestContext requestContext;
    }
}
