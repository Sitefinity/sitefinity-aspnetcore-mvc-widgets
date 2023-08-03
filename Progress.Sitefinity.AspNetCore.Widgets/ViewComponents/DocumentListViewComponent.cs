using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Progress.Sitefinity.AspNetCore.Web;
using Progress.Sitefinity.AspNetCore.Widgets.Models.ContentList;
using Progress.Sitefinity.AspNetCore.Widgets.Models.DocumentList;
using Progress.Sitefinity.AspNetCore.Widgets.Preparations;
using Progress.Sitefinity.RestSdk;

namespace Progress.Sitefinity.AspNetCore.Widgets.ViewComponents
{
    /// <summary>
    /// The view component for the Document list widget.
    /// </summary>
    [SitefinityWidget(EmptyIcon = "plus-circle", EmptyIconText = "Select documents", Title = "Document list", Order = 2, Section = WidgetSection.ContentLists)]
    [ViewComponent(Name = "SitefinityDocumentList")]
    public class DocumentListViewComponent : ViewComponent
    {
        private IDocumentListModel model;
        private IRestClient restClient;
        private IRequestContext requestContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentListViewComponent"/> class.
        /// </summary>
        /// <param name="documentListModel">The document list model.</param>
        /// <param name="restClient">The rest client.</param>
        /// <param name="requestContext">The request context.</param>
        public DocumentListViewComponent(IDocumentListModel documentListModel, IRestClient restClient, IRequestContext requestContext)
        {
            this.model = documentListModel;
            this.restClient = restClient;
            this.requestContext = requestContext;
        }

        /// <summary>
        /// Invokes the Document list widget creation.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public virtual async Task<IViewComponentResult> InvokeAsync(IViewComponentContext<DocumentListEntity> context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

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

            if (viewModel is ContentListCommonViewModel)
            {
                if ((viewModel as ContentListCommonViewModel).Items.Count == 0 &&
                    context.Entity.SelectedItems != null &&
                    context.Entity.SelectedItems.Content.Length > 0 &&
                    !string.IsNullOrEmpty(context.Entity.SelectedItems.Content[0].Type) &&
                    context.Entity.SelectedItems.ItemIdsOrdered != null)
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
