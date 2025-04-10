using System.Linq;
using System.Threading.Tasks;
using Progress.Sitefinity.AspNetCore.RestSdk;
using Progress.Sitefinity.Renderer.Entities.Content;
using Progress.Sitefinity.RestSdk;
using Progress.Sitefinity.RestSdk.Dto;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Common
{
    internal static class Extensions
    {
        public static async Task<string> GetSingleSelectedImageUrlAsync(this IRestClient restClient, MixedContentContext image)
        {
            if (image.ItemIdsOrdered != null && image.ItemIdsOrdered.Length == 1)
            {
                var getAllArgsDictionary = image.Content.ToDictionary(x => x.Type, y => new GetAllArgs());

                var images = await restClient.GetItems<ImageDto>(image, getAllArgsDictionary);
                if (images.Items.Count == 1 && images.Items[0].Id == image.ItemIdsOrdered[0])
                    return images.Items[0].Url;
            }

            return null;
        }
    }
}
