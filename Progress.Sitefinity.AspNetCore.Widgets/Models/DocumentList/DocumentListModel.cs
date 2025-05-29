using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Progress.Sitefinity.AspNetCore.Web;
using Progress.Sitefinity.AspNetCore.Widgets.Models.ContentList;
using Progress.Sitefinity.AspNetCore.Widgets.ViewComponents.Common;
using Progress.Sitefinity.Renderer.Entities.Content;
using Progress.Sitefinity.RestSdk.Filters;
using Progress.Sitefinity.RestSdk.OData;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.DocumentList
{
    /// <summary>
    /// The model for the Document list widget.
    /// </summary>
    public class DocumentListModel : ContentListModelBase, IDocumentListModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentListModel"/> class.
        /// </summary>
        /// <param name="restService">The rest service.</param>
        /// <param name="requestContext">The request context.</param>
        /// <param name="styles">The style classes provider.</param>
        public DocumentListModel(IODataRestClient restService, IRequestContext requestContext, IStyleClassesProvider styles)
            : base(restService, requestContext, styles)
        {
            this.styles = styles;
        }

        /// <inheritdoc />
        public override async Task<object> HandleListView(ContentListEntityBase entity, ReadOnlyCollection<string> urlParameters, HttpContext httpContext)
        {
            var hasSelectedItems = entity?.SelectedItems?.Content?.Length > 0 &&
                !string.IsNullOrEmpty(entity.SelectedItems.Content[0].Type) &&
                entity.SelectedItems.ItemIdsOrdered != null;

            return await this.HandleListViewInternal(entity, urlParameters, httpContext, !hasSelectedItems);
        }

        private protected override ContentListCommonViewModel PopulateViewModel(ContentListEntityBase entityBase)
        {
            var viewModel = new DocumentListViewModel();
            viewModel.CssClasses = entityBase.CssClasses;
            viewModel.DetailItemUrl = new Uri(this.RequestContext.PageNode?.ViewUrl ?? string.Empty, UriKind.RelativeOrAbsolute);
            viewModel.RenderLinks = !(entityBase.ContentViewDisplayMode == ContentViewDisplayMode.Master && entityBase.DetailPageMode == DetailPageSelectionMode.SamePage);
            viewModel.Type = entityBase.SelectedItems?.Content?[0].Type;

            var documentEntity = entityBase as DocumentListEntity;
            if (documentEntity != null)
            {
                var margins = this.styles.GetMarginsClasses(documentEntity);
                viewModel.WrapperCssClass = margins.Trim();
                viewModel.DownloadLinkLabel = documentEntity.DownloadLinkLabel;
                viewModel.SizeColumnLabel = documentEntity.SizeColumnLabel;
                viewModel.TitleColumnLabel = documentEntity.TitleColumnLabel;
                viewModel.TypeColumnLabel = documentEntity.TypeColumnLabel;
            }

            return viewModel;
        }

        private protected override void ModifyParentFilter(ContentListEntityBase entity)
        {
            MixedContentContext mixedContentContext = entity.SelectedItems;

            if (mixedContentContext?.Content != null && mixedContentContext.Content.Length > 0 && mixedContentContext.Content[0].Variations != null)
            {
                for (var i = 0; i < mixedContentContext.Content[0].Variations.Length; i++)
                {
                    var variation = mixedContentContext.Content[0].Variations[i];
                    bool isComplexFilter = variation.Filter.Key == FilterConverter.Types.Complex;
                    CombinedFilter complexFilter = null;

                    if (!string.IsNullOrEmpty(variation.Filter.Value))
                    {
                        complexFilter = FilterConverter.From(new KeyValuePair<string, string>(variation.Filter.Key, variation.Filter.Value)) as CombinedFilter;
                        if (complexFilter != null)
                        {
                            var parentIdFilter = complexFilter.ChildFilters.FirstOrDefault(x => (x as FilterClause)?.FieldName == "ParentId");
                            var hasOtherFiltersThanParentId = complexFilter.ChildFilters.Any(x => (x as FilterClause)?.FieldName != "ParentId");
                            if (parentIdFilter == null && !hasOtherFiltersThanParentId)
                            {
                                var combinedParentFilter = complexFilter.ChildFilters.FirstOrDefault(x => (x is CombinedFilter)) as CombinedFilter;
                                if (combinedParentFilter != null)
                                {
                                    var hasParentIdFilter = combinedParentFilter.ChildFilters.Any(x => (x as FilterClause)?.FieldName == "ParentId");
                                    if (hasParentIdFilter)
                                    {
                                        parentIdFilter = combinedParentFilter.ChildFilters.FirstOrDefault(y => (y as FilterClause)?.FieldName == "ParentId");
                                    }
                                    else
                                    {
                                        var innerCombinedParentFilter = combinedParentFilter.ChildFilters.FirstOrDefault(x => (x is CombinedFilter)) as CombinedFilter;
                                        parentIdFilter = innerCombinedParentFilter.ChildFilters.FirstOrDefault(y => (y as FilterClause)?.FieldName == "ParentId");
                                    }

                                    var newChildFilters = complexFilter.ChildFilters.Where(x => !(x is CombinedFilter).Equals(combinedParentFilter)).ToList();
                                    complexFilter.ChildFilters = newChildFilters;
                                }
                            }
                            else
                            {
                                complexFilter.ChildFilters = complexFilter.ChildFilters.Where(x => (x as FilterClause)?.FieldName != "ParentId").ToList();
                            }

                            if (parentIdFilter != null)
                            {
                                var parentId = (parentIdFilter as FilterClause).FieldValue as JArray;
                                var libraryIdFilter = new CombinedFilter
                                {
                                    Operator = CombinedFilter.LogicalOperators.And,
                                    ChildFilters =
                                {
                                    parentIdFilter,
                                    new FilterClause
                                    {
                                        FieldName = "FolderId",
                                        Operator = FilterClause.Operators.Equal,
                                        FieldValue = null,
                                    },
                                },
                                };

                                var newParentIdFilter = new CombinedFilter
                                {
                                    Operator = CombinedFilter.LogicalOperators.Or,
                                    ChildFilters =
                                {
                                    libraryIdFilter,
                                },
                                };

                                // we add the folder filters one by one because using any+or with a colleciton of ids pointing to the FolterId property
                                // causes OData to throw an exception that these operators could be used only on a collection property
                                // TODO: needs investigation on the EDM generation
                                foreach (var folderId in parentId)
                                {
                                    newParentIdFilter.ChildFilters.Add(
                                        new FilterClause()
                                        {
                                            FieldName = "FolderId",
                                            Operator = FilterClause.Operators.Equal,
                                            FieldValue = folderId,
                                        });
                                }

                                complexFilter.ChildFilters.Add(newParentIdFilter);
                            }
                        }

                        if (isComplexFilter)
                        {
                            mixedContentContext.Content[0].Variations[i].Filter = new KeyValuePair<string, string>(FilterConverter.Types.Complex, JsonConvert.SerializeObject(complexFilter));
                        }
                    }
                }
            }
        }

        private IStyleClassesProvider styles;
    }
}
