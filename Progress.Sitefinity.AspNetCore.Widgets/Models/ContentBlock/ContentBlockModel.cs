using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Progress.Sitefinity.AspNetCore.Models;
using Progress.Sitefinity.AspNetCore.Widgets.ViewComponents.Common;
using Progress.Sitefinity.RestSdk;
using Progress.Sitefinity.RestSdk.OData;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.ContentBlock
{
    /// <summary>
    /// The model for the Content block widget.
    /// </summary>
    public class ContentBlockModel : IContentBlockModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContentBlockModel"/> class.
        /// </summary>
        /// <param name="restService">The HTTP client.</param>
        /// <param name="styles">The style classes provider.</param>
        public ContentBlockModel(IRestClient restService, IStyleClassesProvider styles)
        {
            this.restService = restService;
            this.styles = styles;
        }

        /// <summary>
        /// Initializes the view model.
        /// </summary>
        /// <param name="entity">The entity of the content block.</param>
        /// <returns>The view model of the widget.</returns>
        public virtual async Task<ContentBlockViewModel> InitializeViewModel(ContentBlockEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var viewModel = new ContentBlockViewModel
            {
                Content = entity.Content,
                TagName = entity.TagName,
                Attributes = GetAttributes(entity),
            };

            var margins = this.styles.GetMarginsClasses(entity);
            viewModel.WrapperCssClass = (entity.WrapperCssClass + " " + margins).Trim();

            if (entity.SharedContentID != Guid.Empty)
            {
                try
                {
                    var item = await (this.restService as IODataRestClient).ExecuteBoundFunction<ContentItem>(
                        new BoundFunctionArgs()
                        {
                            Type = RestClientContentTypes.GenericContent,
                            Name = $"Default.GetItemById(itemId={entity.SharedContentID.ToString()})",
                            AdditionalQueryParams = new Dictionary<string, string>()
                            {
                                { QueryParamNames.FallbackPropNames, "Title,Content" },
                            },
                        });

                    viewModel.Content = item.Content;
                }
                catch (HttpRequestException)
                {
                    entity.SharedContentID = Guid.Empty;
                }
            }

            return viewModel;
        }

        private static IList<AttributeModel> GetAttributes(ContentBlockEntity entity)
        {
            var attributes = new List<AttributeModel>();
            if (entity.Attributes != null && entity.Attributes.ContainsKey("ContentBlock"))
            {
                foreach (var attribute in entity.Attributes["ContentBlock"])
                {
                    attributes.Add(attribute);
                }
            }

            return attributes;
        }

        private IRestClient restService;
        private IStyleClassesProvider styles;
    }
}
