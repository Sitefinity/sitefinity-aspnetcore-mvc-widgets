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
    internal class ContentListModelForDetail
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContentListModelForDetail"/> class.
        /// </summary>
        /// <param name="restService">The HTTP client.</param>
        /// <param name="requestContext">The request context.</param>
        public ContentListModelForDetail(IODataRestClient restService, IRequestContext requestContext)
        {
            this.restService = restService;
            this.requestContext = requestContext;
        }

        public async Task<ContentDetailViewModel> HandleDetailItem(ContentListEntity entity, IQueryCollection query)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var detailItem = await this.TryHandleDetailItem(entity, query);
            if (detailItem != null)
            {
                detailItem.CssClass = entity.CssClasses?.FirstOrDefault(x => x.FieldName == "Details view")?.CssClass;
                return detailItem;
            }

            return null;
        }

        private async Task<ContentDetailViewModel> TryHandleDetailItem(ContentListEntity entity, IQueryCollection query)
        {
            if (entity.SelectedItems != null)
            {
                var content = entity.SelectedItems.Content;
                if (content != null && content.Length > 0)
                {
                    var selectedItemsType = entity.SelectedItems.Content[0].Type;
                    var detailItem = this.requestContext.Model.DetailItem;
                    if (detailItem != null &&
                        detailItem.ItemType == selectedItemsType &&
                        entity.ContentViewDisplayMode == ContentViewDisplayMode.Automatic &&
                        entity.DetailPageMode == DetailPageSelectionMode.SamePage)
                    {
                        var item = await this.LoadItem(detailItem.Id, detailItem.ProviderName, detailItem.ItemType, query);
                        return new ContentDetailViewModel(item);
                    }

                    if (detailItem != null && !string.IsNullOrEmpty(detailItem.ErrorMessage))
                    {
                        throw new DetailItemException(detailItem.ErrorMessage);
                    }

                    if (entity.ContentViewDisplayMode == ContentViewDisplayMode.Detail)
                    {
                        var item = await this.LoadItem(entity.SelectedItems.ItemIdsOrdered[0], entity.SelectedItems.Content[0].Variations[0].Source, selectedItemsType, query);
                        return new ContentDetailViewModel(item);
                    }

                    var handled = await this.HandleShowDetailsViewOnChildDetailsView(entity, selectedItemsType, detailItem);
                    if (handled != null)
                    {
                        return handled;
                    }
                }
            }

            return null;
        }

        private Task<SdkItem> LoadItem(string itemId, string itemProvider, string itemType, IQueryCollection query)
        {
            if (query.ContainsKey("sf-content-action"))
            {
                var queryParams = query.ToDictionary(x => x.Key, y => HttpUtility.UrlEncode(y.Value.ToString()));
                return this.restService.ExecuteBoundFunction<SdkItem>(new BoundFunctionArgs()
                {
                    Id = itemId,
                    Name = "Default.GetItemWithStatus()",
                    AdditionalQueryParams = queryParams,
                    Type = itemType,
                    Provider = itemProvider,
                });
            }
            else
            {
                return this.restService.GetItem<SdkItem>(new GetItemArgs()
                {
                    Id = itemId,
                    Type = itemType,
                    Provider = itemProvider,
                    Fields = new[] { "*" },
                });
            }
        }

        private async Task<ContentDetailViewModel> HandleShowDetailsViewOnChildDetailsView(ContentListEntity entity, string selectedItemsType, ResolvedDetailItem detailItem)
        {
            if (entity.ShowDetailsViewOnChildDetailsView && detailItem != null && entity.ContentViewDisplayMode == ContentViewDisplayMode.Master)
            {
                var childTypes = (this.restService as RestClient).ServiceMetadata.GetChildTypes(selectedItemsType).SelectMany((x, i) => x.Select(y => new KeyValuePair<int, string>(i + 1, y)));
                var childTypeInDetails = childTypes.FirstOrDefault(x => x.Value == detailItem.ItemType);
                if (!childTypeInDetails.Equals(default(KeyValuePair<int, string>)))
                {
                    var childItem = await this.restService.GetItem<SdkItem>(new GetItemArgs()
                    {
                        Id = detailItem.Id,
                        Type = detailItem.ItemType,
                        Provider = detailItem.ProviderName,
                        Fields = new[] { "ItemDefaultUrl" },
                    });

                    var parentsTitles = childItem.GetValue<string>("ItemDefaultUrl").Split('/', StringSplitOptions.RemoveEmptyEntries);
                    var parentTitle = parentsTitles.Reverse().ElementAt(childTypeInDetails.Key);

                    if (selectedItemsType == RestClientContentTypes.Blogs)
                    {
                        parentTitle = parentsTitles[0];
                    }

                    var parentItems = await this.restService.GetItems<SdkItem>(new GetAllArgs()
                    {
                        Type = selectedItemsType,
                        Provider = detailItem.ProviderName,
                        Fields = new[] { "*" },
                        Filter = new FilterClause()
                        {
                            FieldName = "UrlName",
                            Operator = FilterClause.Operators.Equal,
                            FieldValue = parentTitle,
                        },
                    });

                    if (parentItems.Items.Count == 1)
                    {
                        return new ContentDetailViewModel(parentItems.Items.First());
                    }
                }
            }

            return null;
        }

        private IODataRestClient restService;
        private IRequestContext requestContext;
    }
}
