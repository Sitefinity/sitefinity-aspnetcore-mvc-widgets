using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Http;
using Progress.Sitefinity.AspNetCore.RestSdk;
using Progress.Sitefinity.AspNetCore.Web;
using Progress.Sitefinity.AspNetCore.Widgets.Models.DocumentList;
using Progress.Sitefinity.AspNetCore.Widgets.ViewComponents.Common;
using Progress.Sitefinity.Clients.LayoutService.Dto;
using Progress.Sitefinity.RestSdk;
using Progress.Sitefinity.RestSdk.Client;
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
        public GetAllArgs GetAllArgs { get; set; } = new GetAllArgs();

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentListModelForDetail"/> class.
        /// </summary>
        /// <param name="restService">The HTTP client.</param>
        /// <param name="requestContext">The request context.</param>
        /// <param name="styles">The style classes provider.</param>
        public ContentListModelForDetail(IODataRestClient restService, IRequestContext requestContext, IStyleClassesProvider styles)
        {
            this.restService = restService;
            this.requestContext = requestContext;
            this.styles = styles;
        }

        public async Task<ContentDetailViewModel> HandleDetailItem(ContentListEntityBase entity, bool showDetailsViewOnChildDetailsView)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var detailItem = await this.TryHandleDetailItem(entity, showDetailsViewOnChildDetailsView);
            if (detailItem != null)
            {
                detailItem.CssClass = entity.CssClasses?.FirstOrDefault(x => x.FieldName == "Details view")?.CssClass;

                var documentEntity = entity as DocumentListEntity;
                if (documentEntity != null)
                {
                    var margins = this.styles.GetMarginsClasses(documentEntity);
                    detailItem.WrapperCssClass = margins.Trim();
                    detailItem.DownloadLinkLabel = documentEntity.DownloadLinkLabel;
                }

                return detailItem;
            }

            return null;
        }

        private static IList<string> ExtractFieldsFromExpression(string selectExpression)
        {
            var fields = new List<string>();
            if (!string.IsNullOrEmpty(selectExpression))
            {
                var split = selectExpression.Split(';', StringSplitOptions.RemoveEmptyEntries);
                foreach (var field in split)
                {
                    fields.Add(field.Trim());
                }
            }

            return fields.Count > 0 ? fields : new List<string> { "*" };
        }

        private async Task<ContentDetailViewModel> TryHandleDetailItem(ContentListEntityBase entity, bool showDetailsViewOnChildDetailsView)
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
                        var item = this.requestContext.ResolvedDetailItem;
                        return new ContentDetailViewModel(item);
                    }

                    if (detailItem != null && !string.IsNullOrEmpty(detailItem.ErrorMessage))
                    {
                        throw new DetailItemException(detailItem.ErrorMessage);
                    }

                    if (entity.ContentViewDisplayMode == ContentViewDisplayMode.Detail)
                    {
                        var item = new SdkItem();
                        if (entity.SelectedItems.ItemIdsOrdered.Length == 0)
                        {
                            this.GetAllArgs.Take = 1;
                            this.GetAllArgs.Skip = 0;
                            this.GetAllArgs.Fields = ExtractFieldsFromExpression((entity as ContentListEntity)?.DetailItemSelectExpression);
                            var detailItemCollection = await this.restService.GetItems<SdkItem>(entity.SelectedItems, this.GetAllArgs);
                            if (detailItemCollection.Items.Count == 0)
                                return null;
                            item = detailItemCollection.Items[0];
                        }
                        else
                        {
                            item = await this.restService.GetItem<SdkItem>(new GetItemArgs()
                            {
                                Id = entity.SelectedItems.ItemIdsOrdered[0],
                                Type = selectedItemsType,
                                Provider = entity.SelectedItems.Content[0].Variations[0].Source,
                                Fields = ExtractFieldsFromExpression((entity as ContentListEntity)?.DetailItemSelectExpression),
                            });
                        }

                        return new ContentDetailViewModel(item);
                    }

                    var handled = await this.HandleShowDetailsViewOnChildDetailsView(entity, selectedItemsType, detailItem, showDetailsViewOnChildDetailsView);
                    if (handled != null)
                    {
                        return handled;
                    }
                }
            }

            return null;
        }

        private async Task<ContentDetailViewModel> HandleShowDetailsViewOnChildDetailsView(ContentListEntityBase entity, string selectedItemsType, ResolvedDetailItem detailItem, bool showDetailsViewOnChildDetailsView)
        {
            if (showDetailsViewOnChildDetailsView && detailItem != null && entity.ContentViewDisplayMode == ContentViewDisplayMode.Master)
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
                        Fields = ExtractFieldsFromExpression((entity as ContentListEntity)?.DetailItemSelectExpression),
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
        private IStyleClassesProvider styles;
    }
}
