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
    internal abstract class ContentListBasePreparation : IRequestPreparation
    {
        internal const string PreparedData = nameof(ContentListBasePreparation.PreparedData);

        protected abstract string ContentListType { get; }

        private IContentListModelBase contentListModelBase;

        public ContentListBasePreparation(IContentListModelBase contentListModelBase)
        {
            this.contentListModelBase = contentListModelBase;
        }

        public Task Prepare(PageModel pageModel, IRestClient batchClient, HttpContext httpContext)
        {
            var context = httpContext.RequestServices.GetService<IRequestContext>();
            var contentListWidgets = pageModel.AllViewComponentsFlat
                .Where(x => typeof(IViewComponentContext<ContentListEntityBase>).IsAssignableFrom(x.GetType()) && (x.Name == this.ContentListType))
                .Select(context => context as IViewComponentContext<ContentListEntityBase>)
                .ToList();

            if (!contentListWidgets.Any())
                return Task.CompletedTask;

            return this.PreparePagerAsync(pageModel, httpContext, contentListWidgets);
        }

        private async Task PreparePagerAsync(PageModel pageModel, HttpContext httpContext, IList<IViewComponentContext<ContentListEntityBase>> components)
        {
            var tasks = components.Select(component => this.PrepareComponentAsync(component, pageModel, httpContext)).ToArray();
            var taskResults = await Task.WhenAll(tasks);

            var allTasksResolved = true;
            var allResolvedSegments = new List<string>();
            foreach (var taskResult in taskResults)
            {
                if (taskResult.IsFaulted || !taskResult.IsPageValid)
                {
                    allTasksResolved = false;
                }

                if (taskResult.MarkAsBadRequest)
                {
                    pageModel.MarkAsBadRequest();
                }

                if (taskResult.ResolvedUrlSegments != null)
                {
                    allResolvedSegments.AddRange(taskResult.ResolvedUrlSegments);
                }

                if (taskResult.State != null && taskResult.State.Any())
                {
                    foreach (var kvp in taskResult.State)
                    {
                        taskResult.Component.State[kvp.Key] = kvp.Value;
                    }
                }
            }

            allResolvedSegments = allResolvedSegments.Distinct().ToList();
            lock (pageModel)
            {
                var allParametersResolved = false;
                if (allTasksResolved)
                {
                    if (pageModel.UrlParameters.Count == allResolvedSegments.Count || Enumerable.SequenceEqual(pageModel.UrlParameters.OrderBy(x => x), allResolvedSegments.OrderBy(x => x)))
                    {
                        pageModel.MarkUrlParametersResolved();
                        allParametersResolved = true;
                    }
                }

                if (!allParametersResolved)
                {
                    pageModel.MarkUrlParametersResolved(allResolvedSegments);
                }
            }
        }

        private async Task<ComponentPreparationResult> PrepareComponentAsync(IViewComponentContext<ContentListEntityBase> component, PageModel pageModel, HttpContext httpContext)
        {
            try
            {
                var items = await this.contentListModelBase.HandleListView(component.Entity, pageModel.UrlParameters, httpContext);
                var listViewModel = items as ContentListCommonViewModel;
                if (listViewModel == null)
                    return new ComponentPreparationResult();

                var result = new ComponentPreparationResult()
                {
                    ResolvedUrlSegments = listViewModel.ResolvedUrlSegments,
                    Component = component
                };

                if (listViewModel.Pager != null)
                {
                    if (listViewModel.Pager.IsPageValid())
                    {
                        result.State.Add(ContentListPreparation.PreparedData, listViewModel);
                        foreach (var pagerSegment in listViewModel.Pager.ProcessedUrlSegments)
                        {
                            result.ResolvedUrlSegments.Add(pagerSegment);
                        }
                    }
                    else
                    {
                        result.IsPageValid = false;

                        // bad request if the page number is invalid (string, zero, negative) otherwise fallback to first page
                        if (component.Entity.PagerMode == PagerMode.QueryParameter && listViewModel.Pager.CurrentPage <= 0)
                            result.MarkAsBadRequest = true;
                    }
                }
                else
                {
                    result.State.Add(ContentListPreparation.PreparedData, listViewModel);
                }

                return result;
            }
            catch
            {
                return new ComponentPreparationResult() { IsFaulted = true, IsPageValid = false };
            }
        }

        private sealed class ComponentPreparationResult
        {
            public bool IsFaulted { get; set; }

            public bool IsPageValid { get; set; } = true;

            public bool MarkAsBadRequest { get; set; }

            public IList<string> ResolvedUrlSegments { get; set; }

            public Dictionary<string, object> State { get; set; } = new Dictionary<string, object>();

            public IViewComponentContext<ContentListEntityBase> Component { get; set; }
        }
    }
}
