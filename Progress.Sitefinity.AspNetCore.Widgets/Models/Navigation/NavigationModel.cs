using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Progress.Sitefinity.AspNetCore.Areas.Diagnostics.Profiling;
using Progress.Sitefinity.AspNetCore.Web;
using Progress.Sitefinity.AspNetCore.Widgets.ViewComponents.Common;
using Progress.Sitefinity.RestSdk;
using Progress.Sitefinity.RestSdk.Dto;
using Progress.Sitefinity.RestSdk.OData;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Navigation
{
    /// <summary>
    /// The model for the Navigation widget.
    /// </summary>
    public class NavigationModel : INavigationModel, INavigationModelWithPreparation
    {
        private IRequestContext requestContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationModel"/> class.
        /// </summary>
        /// <param name="restService">The HTTP client.</param>
        /// <param name="requestContext">The request context.</param>
        /// <param name="styles">The style classes provider.</param>
        public NavigationModel(IODataRestClient restService, IStyleClassesProvider styles, IRequestContext requestContext)
        {
            this.restService = restService;
            this.styles = styles;
            this.requestContext = requestContext;
        }

        /// <inheritdoc/>
        public virtual async Task<NavigationViewModel> InitializeViewModel(NavigationEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var items = await this.GetItems(entity, this.restService);
            return this.InitializeViewModel(entity, items.Value);
        }

        /// <inheritdoc/>
        public NavigationViewModel InitializeViewModel(NavigationEntity entity, PageViewModel[] items)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var viewModel = new NavigationViewModel();

            var margins = this.styles.GetMarginsClasses(entity);
            viewModel.WrapperCssClass = (entity.WrapperCssClass + " " + margins).Trim();

            viewModel.Nodes = items.ToList();
            viewModel.Attributes = entity.Attributes;

            return viewModel;
        }

        /// <inheritdoc/>
        public Task<ODataWrapper<PageViewModel[]>> GetItems(NavigationEntity entity, IODataRestClient restClient)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            if (restClient == null)
                throw new ArgumentNullException(nameof(restClient));

            var selectedPage = entity.SelectedPage.ItemIdsOrdered != null && entity.SelectedPage.ItemIdsOrdered.Length == 1 ? entity.SelectedPage.ItemIdsOrdered[0] : null;
            Guid selectedPageId;
            if (!Guid.TryParse(selectedPage, out selectedPageId))
            {
                selectedPageId = Guid.Empty;
            }

            var queryParams = new Dictionary<string, string>()
            {
                { "selectionModeString", entity.SelectionMode.ToString() },
                { "levelsToInclude", entity.LevelsToInclude.ToString() },
                { "showParentPage", entity.ShowParentPage.ToString() },
                { "selectedPageId", selectedPageId.ToString() },
                { Constants.QueryParams.PageNodeId, this.requestContext.Model.Id.ToString() },
                { "selectedPages", JsonConvert.SerializeObject(entity.CustomSelectedPages.ItemIdsOrdered) },
            };

            return restClient.ExecuteBoundFunction<ODataWrapper<PageViewModel[]>>(new BoundFunctionArgs()
            {
                Name = $"Default.HierarhicalByLevelsResponse()",
                Type = "pages",
                AdditionalQueryParams = queryParams,
            });
        }

        private IODataRestClient restService;
        private IStyleClassesProvider styles;
    }
}
