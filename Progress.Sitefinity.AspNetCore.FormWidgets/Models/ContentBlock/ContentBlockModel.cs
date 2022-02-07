using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Progress.Sitefinity.RestSdk;
using Progress.Sitefinity.RestSdk.OData;

namespace Progress.Sitefinity.AspNetCore.FormWidgets.Models.ContentBlock
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
        public ContentBlockModel(IRestClient restService)
        {
            this.restService = restService;
        }

        /// <summary>
        /// Initializes the view model.
        /// </summary>
        /// <param name="entity">The entity of the content block.</param>
        /// <returns>The view model of the widget.</returns>
        public virtual ContentBlockViewModel InitializeViewModel(ContentBlockEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var viewModel = new ContentBlockViewModel
            {
                Content = entity.Content,
                TagName = entity.TagName,
                WrapperCssClass = entity.WrapperCssClass,
            };

            return viewModel;
        }

        private IRestClient restService;
    }
}
