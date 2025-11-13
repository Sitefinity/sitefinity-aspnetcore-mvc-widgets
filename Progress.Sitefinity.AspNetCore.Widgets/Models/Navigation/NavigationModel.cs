using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
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
    public class NavigationModel : INavigationModel
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
            var value = items.Value;
            value = AddManualSelectionItems(entity, value);

            return this.InitializeViewModel(entity, value);
        }

        /// <summary>
        /// Initializes the view model with a preloaded state.
        /// </summary>
        /// <param name="entity">The navigation entity.</param>
        /// <param name="items">The page items.</param>
        /// <returns>The view model of the widget.</returns>
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

        /// <summary>
        /// Loads the items for the provided entity using hte provided <see cref="IODataRestClient" /> instance.
        /// </summary>
        /// <param name="entity">The entity class.</param>
        /// <param name="restClient">The rest client.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
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

        private static PageViewModel[] AddManualSelectionItems(NavigationEntity entity, PageViewModel[] value)
        {
            if (entity.CustomSelectedPages.ManualSelectionItems?.Length > 0)
            {
                var valueList = value.ToList();
                foreach (var manualSelectionItem in entity.CustomSelectedPages.ManualSelectionItems.OrderBy(m => m.Index))
                {
                    var externalUrl = manualSelectionItem.Item as ExternalUrlEntity;
                    if (externalUrl != null)
                    {
                        var externalUrlNode = new PageViewModel()
                        {
                            ChildNodes = new List<PageViewModel>(),
                            HasChildOpen = false,
                            IsCurrentlyOpened = false,
                            Key = externalUrl.Title,
                            LinkTarget = externalUrl.OpenInNewWindow ? "_blank" : "_self",
                            Title = externalUrl.Title,
                            Url = externalUrl.Url
                        };

                        if (manualSelectionItem.Index <= valueList.Count)
                        {
                            valueList.Insert(manualSelectionItem.Index, externalUrlNode);
                        }
                        else
                        {
                            valueList.Add(externalUrlNode);
                        }
                    }
                }

                value = valueList.ToArray();
            }

            return value;
        }

        private IODataRestClient restService;
        private IStyleClassesProvider styles;
    }
}
