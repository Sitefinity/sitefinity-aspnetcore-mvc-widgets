using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Newtonsoft.Json;
using Progress.Sitefinity.AspNetCore.Models;
using Progress.Sitefinity.AspNetCore.Web;
using Progress.Sitefinity.AspNetCore.Widgets.ViewComponents.Common;
using Progress.Sitefinity.RestSdk;
using Progress.Sitefinity.RestSdk.Clients.Pages.Dto;
using Progress.Sitefinity.RestSdk.Dto;
using Progress.Sitefinity.RestSdk.OData;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Breadcrumb
{
    /// <summary>
    /// The model for the Breadcrumb widget.
    /// </summary>
    public class BreadcrumbModel : IBreadcrumbModel
    {
        private IODataRestClient restClient;
        private IRequestContext requestContext;
        private IStyleClassesProvider styles;
        private IHttpContextAccessor httpContextAccessor;
        private IRenderContext renderContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="BreadcrumbModel"/> class.
        /// </summary>
        /// <param name="restClient">The rest client.</param>
        /// <param name="requestContext">The request context.</param>
        /// <param name="renderContext">The render context.</param>
        /// <param name="styles">The styles.</param>
        /// <param name="httpContextAccessor">The http context accessor.</param>
        public BreadcrumbModel(IODataRestClient restClient, IRequestContext requestContext, IRenderContext renderContext, IStyleClassesProvider styles, IHttpContextAccessor httpContextAccessor)
        {
            this.restClient = restClient;
            this.requestContext = requestContext;
            this.renderContext = renderContext;
            this.styles = styles;
            this.httpContextAccessor = httpContextAccessor;
        }

        /// <inheritdoc/>
        public virtual async Task<BreadcrumbViewModel> InitializeViewModel(BreadcrumbEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var viewModel = new BreadcrumbViewModel();
            if (this.requestContext.PageNode != null)
            {
                var currentPageId = this.requestContext.PageNode.Id;
                var args = new BoundFunctionArgs()
                {
                    Type = RestClientContentTypes.Pages,
                    Name = "Default.GetBreadcrumb()",
                };

                if (entity.BreadcrumbIncludeOption == BreadcrumbIncludeOption.SpecificPagePath && entity.SelectedPage.ItemIdsOrdered.Length > 0)
                    args.AdditionalQueryParams.Add("startingPageId", entity.SelectedPage.ItemIdsOrdered[0]);

                args.AdditionalQueryParams.Add("currentPageId", currentPageId);
                args.AdditionalQueryParams.Add("currentPageUrl", HttpUtility.UrlEncode(this.httpContextAccessor.HttpContext.Request.GetEncodedUrl()));
                args.AdditionalQueryParams.Add("addStartingPageAtEnd", entity.AddCurrentPageLinkAtTheEnd.ToString());
                args.AdditionalQueryParams.Add("addHomePageAtBeginning", entity.AddHomePageLinkAtBeginning.ToString());
                args.AdditionalQueryParams.Add("includeGroupPages", entity.IncludeGroupPages.ToString());

                if (this.requestContext.Model.DetailItem != null && entity.AllowVirtualNodes)
                {
                    var stringifiedItem = JsonConvert.SerializeObject(this.requestContext.Model.DetailItem);
                    args.AdditionalQueryParams.Add("detailItemInfo", stringifiedItem);
                }

                var result = await this.restClient.ExecuteBoundFunction<ODataWrapper<PageNodeDto[]>>(args);

                viewModel.Pages.AddRange(result.Value);
            }
            else if (this.renderContext.IsEdit)
            {
                viewModel.PageMissingMessage = "Breadcrumb is visible when you are on a particular page.";
            }

            var margins = this.styles.GetMarginsClasses(entity);
            viewModel.CssClass = (entity.WrapperCssClass + " " + margins).Trim();
            viewModel.Attributes = entity.Attributes;

            return viewModel;
        }
    }
}
