using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Progress.Sitefinity.AspNetCore.Models;
using Progress.Sitefinity.AspNetCore.Preparations;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Progress.Sitefinity.AspNetCore.Web;
using Progress.Sitefinity.AspNetCore.Widgets.Models.ContentList;
using Progress.Sitefinity.RestSdk;

namespace Progress.Sitefinity.AspNetCore.Widgets.Preparations
{
    internal class ContentListPreparation : IRequestPreparation
    {
        public ContentListPreparation(IContentListModel contentListModel)
        {
            this.contentListModel = contentListModel;
        }

        public Task Prepare(PageModel pageModel, IRestClient batchClient, HttpContext httpContext)
        {
            var context = httpContext.RequestServices.GetService<IRequestContext>();
            var contentListWidgets = pageModel.AllViewComponentsFlat
                .Where(x => typeof(IViewComponentContext<ContentListEntity>).IsAssignableFrom(x.GetType()))
                .Select(context => context as IViewComponentContext<ContentListEntity>)
                .ToList();

            if (!contentListWidgets.Any())
                return Task.CompletedTask;

            return this.PreparePager(pageModel, httpContext, contentListWidgets);
        }

        private Task PreparePager(PageModel pageModel, HttpContext httpContext, IList<IViewComponentContext<ContentListEntity>> components)
        {
            var tasks = new List<Task>();
            var allTasksResolved = true;
            var resolvedSegments = new List<string>();
            foreach (var component in components)
            {
                var resultingTask = this.contentListModel.HandleListView(component.Entity, pageModel.UrlParameters, httpContext).ContinueWith(
                    (itemsTask) =>
                    {
                        if (itemsTask.IsFaulted)
                        {
                            allTasksResolved = false;
                            return;
                        }

                        var listViewModel = itemsTask.Result as ContentListViewModel;
                        if (listViewModel != null)
                        {
                            if (listViewModel.Pager != null)
                            {
                                var processedUrlSegments = listViewModel.Pager.ProcessedUrlSegments;
                                var isPageNumberValid = listViewModel.Pager.IsPageNumberValid;

                                if (isPageNumberValid)
                                {
                                    component.State.Add(ContentListPreparation.PreparedData, listViewModel);
                                    resolvedSegments.AddRange(processedUrlSegments);
                                }
                                else
                                {
                                    allTasksResolved = false;
                                }
                            }

                            pageModel.MarkUrlParametersResolved(listViewModel.ResolvedUrlSegments);
                        }
                    }, TaskScheduler.Current);

                tasks.Add(resultingTask);
            }

            Task.WaitAll(tasks.ToArray());

            resolvedSegments = resolvedSegments.Distinct().ToList();
            if (pageModel.UrlParameters.Count != resolvedSegments.Count || !Enumerable.SequenceEqual(pageModel.UrlParameters.OrderBy(x => x), resolvedSegments.OrderBy(x => x)))
                allTasksResolved = false;

            if (allTasksResolved)
            {
                pageModel.MarkUrlParametersResolved();
            }
            else
            {
                pageModel.MarkUrlParametersResolved(resolvedSegments);
            }

            return Task.WhenAll(tasks);
        }

        internal const string PreparedData = nameof(ContentListPreparation.PreparedData);
        private readonly IContentListModel contentListModel;
    }
}
