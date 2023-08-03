using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Progress.Sitefinity.AspNetCore.Web;
using Progress.Sitefinity.AspNetCore.Widgets.ViewComponents.Common;
using Progress.Sitefinity.RestSdk;
using Progress.Sitefinity.RestSdk.Client;
using Progress.Sitefinity.RestSdk.OData;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.ContentList
{
    /// <summary>
    /// The model for the Content list widget.
    /// </summary>
    public class ContentListModel : ContentListModelBase, IContentListModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContentListModel"/> class.
        /// </summary>
        /// <param name="restService">The HTTP client.</param>
        /// <param name="requestContext">The request context.</param>
        /// <param name="styles">The style classes provider.</param>
        public ContentListModel(IODataRestClient restService, IRequestContext requestContext, IStyleClassesProvider styles)
            : base(restService, requestContext, styles)
        {
        }

        /// <inheritdoc/>
        public override async Task<object> HandleListView(ContentListEntityBase entity, ReadOnlyCollection<string> urlParameters, HttpContext httpContext)
        {
            return await this.HandleListViewInternal(entity, urlParameters, httpContext);
        }

        private protected override void AddSelectExpression(ContentListEntityBase entity, GetAllArgs getAllArgs)
        {
            string selectExpression = (entity as ContentListEntity).SelectExpression;

            if (string.IsNullOrEmpty(selectExpression))
            {
                getAllArgs.Fields.Add("*");
            }
            else
            {
                var split = selectExpression.Split(';', StringSplitOptions.RemoveEmptyEntries);
                foreach (var field in split)
                {
                    getAllArgs.Fields.Add(field.Trim());
                }
            }
        }

        private protected override bool HideListView(ContentListEntityBase entityBase, string type)
        {
            var entity = entityBase as ContentListEntity;
            if (!string.IsNullOrEmpty(type) && !entity.ShowListViewOnChildDetailsView)
            {
                // get details item
                var detailItem = this.RequestContext.Model.DetailItem;
                if (detailItem != null)
                {
                    // check whether the item is from a child type
                    var childTypes = (this.RestService as RestClient).ServiceMetadata.GetChildTypes(type).SelectMany(x => x);

                    return childTypes.Any(x => x == detailItem.ItemType);
                }
            }

            return false;
        }

        private protected override ContentListViewModel PopulateViewModel(ContentListEntityBase entityBase)
        {
            var viewModel = new ContentListViewModel();
            var entity = entityBase as ContentListEntity;
            viewModel.RenderLinks = !(entity.ContentViewDisplayMode == ContentViewDisplayMode.Master && entity.DetailPageMode == DetailPageSelectionMode.SamePage);
            viewModel.ListFieldMapping = entity.ListFieldMapping;
            viewModel.CssClasses = entity.CssClasses;
            viewModel.DetailItemUrl = new Uri(this.RequestContext.PageNode?.ViewUrl ?? string.Empty, UriKind.RelativeOrAbsolute);

            return viewModel;
        }

        private protected override bool ShowDetailsViewOnChildDetailsView(ContentListEntityBase entityBase)
        {
            return (entityBase as ContentListEntity).ShowDetailsViewOnChildDetailsView;
        }

        private protected override bool ShowListViewOnEmptyParentFilter(ContentListEntityBase entityBase)
        {
            return (entityBase as ContentListEntity).ShowListViewOnEmptyParentFilter;
        }
    }
}
