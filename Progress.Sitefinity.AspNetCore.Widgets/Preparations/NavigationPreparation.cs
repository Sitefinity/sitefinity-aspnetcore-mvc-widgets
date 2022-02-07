using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Progress.Sitefinity.AspNetCore.Models;
using Progress.Sitefinity.AspNetCore.Preparations;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Navigation;
using Progress.Sitefinity.RestSdk;
using Progress.Sitefinity.RestSdk.OData;

namespace Progress.Sitefinity.AspNetCore.Widgets.Preparations
{
    internal class NavigationPreparation : IRequestPreparation
    {
        internal const string PreparedData = nameof(NavigationPreparation.PreparedData);

        private readonly INavigationModelWithPreparation navigationModel;

        public NavigationPreparation(INavigationModel navigationModel)
        {
            this.navigationModel = navigationModel as INavigationModelWithPreparation;
        }

        public Task Prepare(PageModel pageModel, IRestClient batchClient, HttpContext context)
        {
            if (this.navigationModel == null)
                return Task.CompletedTask;

            var viewComponents = pageModel.AllViewComponentsFlat.Where(x => typeof(IViewComponentContext<NavigationEntity>).IsAssignableFrom(x.GetType())).ToList();
            var tasks = new List<Task>();
            foreach (IViewComponentContext<NavigationEntity> component in viewComponents)
            {
                var resultingTask = this.navigationModel.GetItems(component.Entity, batchClient as IODataRestClient).ContinueWith(
                    (itemsTask) =>
                {
                    component.State.Add(NavigationPreparation.PreparedData, itemsTask.Result.Value);
                }, TaskScheduler.Current);

                tasks.Add(resultingTask);
            }

            return Task.WhenAll(tasks);
        }
    }
}
