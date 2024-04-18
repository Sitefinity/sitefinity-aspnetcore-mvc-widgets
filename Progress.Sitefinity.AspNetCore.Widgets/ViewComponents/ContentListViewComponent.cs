using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Progress.Sitefinity.AspNetCore.Web;
using Progress.Sitefinity.AspNetCore.Widgets.Models.ContentList;
using Progress.Sitefinity.AspNetCore.Widgets.Preparations;
using Progress.Sitefinity.RestSdk;
using Progress.Sitefinity.RestSdk.Client;

namespace Progress.Sitefinity.AspNetCore.Widgets.ViewComponents
{
    /// <summary>
    /// The view component for the Content list widget.
    /// </summary>
    [SitefinityWidget(Title = ContentListViewComponent.Title, EmptyIconText = "Select content", EmptyIconAction = EmptyLinkAction.Edit, EmptyIcon = "plus-circle", Section = WidgetSection.ContentLists, Order = 1)]
    [ViewComponent(Name = "SitefinityContentList")]
    public class ContentListViewComponent : ViewComponent
    {
        /// <summary>
        /// The default title of the <see cref="ContentListViewComponent" /> - Content list.
        /// </summary>
        public const string Title = "Content list";

        private IContentListModel model;
        private IRestClient restClient;
        private IRequestContext requestContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentListViewComponent"/> class.
        /// </summary>
        /// <param name="contentListModel">The content list model.</param>
        /// <param name="restClient">The rest client.</param>
        /// <param name="requestContext">The request context.</param>
        public ContentListViewComponent(IContentListModel contentListModel, IRestClient restClient, IRequestContext requestContext)
        {
            this.model = contentListModel;
            this.restClient = restClient;
            this.requestContext = requestContext;
        }

        /// <summary>
        /// Invokes the Content list widget creation.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public virtual async Task<IViewComponentResult> InvokeAsync(IViewComponentContext<ContentListEntity> context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (context.Entity.SelectedItems != null && context.Entity.SelectedItems.Content.Length > 0 && context.Title == ContentListViewComponent.Title)
            {
                var type = context.Entity.SelectedItems.Content[0].Type;
                if (!string.IsNullOrEmpty(type))
                {
                    var moduleDisplayName = (this.restClient as RestClient).ServiceMetadata.GetModuleDisplayName(type);
                    context.SetTitle($"{ContentListViewComponent.Title} - {moduleDisplayName}");
                }
            }

            object viewModel;
            if (context.State.TryGetValue(ContentListPreparation.PreparedData, out object value))
            {
                viewModel = value;
            }
            else
            {
                viewModel = await this.model.HandleListView(context.Entity, this.requestContext.Model.UrlParameters, this.HttpContext);
            }

            if (viewModel is ContentListViewModelBase)
            {
                (viewModel as ContentListViewModelBase).Attributes = context.Entity.Attributes;
            }

            if (viewModel is ContentListViewModel)
            {
                if ((viewModel as ContentListViewModel).Items.Count == 0 &&
                    context.Entity.SelectedItems != null &&
                    context.Entity.SelectedItems.Content.Length > 0 &&
                    !string.IsNullOrEmpty(context.Entity.SelectedItems.Content[0].Type))
                {
                    context.SetHideEmptyVisual(true);
                }

                return this.View(context.Entity.SfViewName, viewModel);
            }
            else
            {
                return this.View(context.Entity.SfDetailViewName, viewModel);
            }
        }
    }
}
