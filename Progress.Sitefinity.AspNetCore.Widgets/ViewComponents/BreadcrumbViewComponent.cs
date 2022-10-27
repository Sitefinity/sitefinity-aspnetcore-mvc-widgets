using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Progress.Sitefinity.AspNetCore.Web;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Breadcrumb;
using Progress.Sitefinity.RestSdk;
using Progress.Sitefinity.RestSdk.Clients.Pages.Dto;
using Progress.Sitefinity.RestSdk.Dto;
using Progress.Sitefinity.RestSdk.OData;

namespace Progress.Sitefinity.AspNetCore.Widgets.ViewComponents
{
    /// <summary>
    /// Language selector widget.
    /// </summary>
    [SitefinityWidget(Title = "Breadcrumb", Category = WidgetCategory.NavigationAndSearch, Section = WidgetSection.MainNavigation)]
    [ViewComponent(Name = "SitefinityBreadcrumb")]
    public class BreadcrumbViewComponent : ViewComponent
    {
        private IBreadcrumbModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="BreadcrumbViewComponent"/> class.
        /// </summary>
        /// <param name="model">The breadcrumb model.</param>
        public BreadcrumbViewComponent(IBreadcrumbModel model)
        {
            this.model = model;
        }

        /// <summary>
        /// Invokes the breadcrumb logic.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>A <see cref="IViewComponentResult"/> representing the result of the operation.</returns>
        public async Task<IViewComponentResult> InvokeAsync(IViewComponentContext<BreadcrumbEntity> context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var viewModel = await this.model.InitializeViewModel(context.Entity);

            return this.View(context.Entity.SfViewName, viewModel);
        }
    }
}
