using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Progress.Sitefinity.AspNetCore.Models;
using Progress.Sitefinity.AspNetCore.Preparations;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Image;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Navigation;
using Progress.Sitefinity.RestSdk;
using Progress.Sitefinity.RestSdk.OData;

namespace Progress.Sitefinity.AspNetCore.Widgets.Preparations
{
    internal class ImagePreparation : IRequestPreparation
    {
        internal const string PreparedData = nameof(ImagePreparation.PreparedData);

        private readonly IImageModelWithPreparation imageModel;

        public ImagePreparation(IImageModel navigationModel)
        {
            this.imageModel = navigationModel as IImageModelWithPreparation;
        }

        public Task Prepare(PageModel pageModel, IRestClient batchClient, HttpContext context)
        {
            if (this.imageModel == null)
                return Task.CompletedTask;

            var viewComponents = pageModel.AllViewComponentsFlat.Where(x => typeof(IViewComponentContext<ImageEntity>).IsAssignableFrom(x.GetType())).ToList();
            var tasks = new List<Task>();
            foreach (IViewComponentContext<ImageEntity> component in viewComponents)
            {
                var resultingTask = this.imageModel.GetImage(component.Entity, batchClient as IODataRestClient).ContinueWith(
                    (itemsTask) =>
                {
                    if (itemsTask.IsFaulted)
                    {
                        component.State.Add(ImagePreparation.PreparedData, itemsTask.Exception.InnerException);
                        return;
                    }

                    component.State.Add(ImagePreparation.PreparedData, itemsTask.Result);
                }, TaskScheduler.Current);

                tasks.Add(resultingTask);
            }

            return Task.WhenAll(tasks);
        }
    }
}
