﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Progress.Sitefinity.AspNetCore.Widgets.Models.ContentBlock;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Navigation;
using Progress.Sitefinity.AspNetCore.Widgets.Preparations;

namespace Progress.Sitefinity.AspNetCore.Widgets.ViewComponents
{
    /// <summary>
    /// The view component for the Navigation widget.
    /// </summary>
    [SitefinityWidget(Title = "Navigation", Category = WidgetCategory.Content, Section = WidgetSection.NavigationAndSearch, Order = 0, EmptyIconText = "No pages have been published", EmptyIconAction = EmptyLinkAction.None, EmptyIcon = "file-text-o", IconName = "navigation")]
    [ViewComponent(Name = "SitefinityNavigation")]
    public class NavigationViewComponent : ViewComponent
    {
        private INavigationModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationViewComponent"/> class.
        /// </summary>
        /// <param name="navigationModel">The navigation model.</param>
        public NavigationViewComponent(INavigationModel navigationModel)
        {
            this.model = navigationModel;
        }

        /// <summary>
        /// Invokes the Navigation widget creation.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public virtual async Task<IViewComponentResult> InvokeAsync(IViewComponentContext<NavigationEntity> context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var viewModel = await this.model.InitializeViewModel(context.Entity);
            return this.View(context.Entity.SfViewName, viewModel);
        }
    }
}
