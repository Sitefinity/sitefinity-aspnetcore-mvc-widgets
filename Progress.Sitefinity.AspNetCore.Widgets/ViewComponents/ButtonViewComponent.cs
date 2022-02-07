using System;
using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Button;

namespace Progress.Sitefinity.AspNetCore.Widgets.ViewComponents
{
    /// <summary>
    /// The view component for the Button (CTA) widget.
    /// </summary>
    [SitefinityWidget(Title = "Call to action", EmptyIconText = "Create call to action", Order = 3, Section = WidgetSection.Basic)]
    [ViewComponent(Name = "SitefinityButton")]
    public class ButtonViewComponent : ViewComponent
    {
        private IButtonModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="ButtonViewComponent"/> class.
        /// </summary>
        /// <param name="buttonModel">The button model.</param>
        public ButtonViewComponent(IButtonModel buttonModel)
        {
            this.model = buttonModel;
        }

        /// <summary>
        /// Invokes the Button widget creation.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>A <see cref="IViewComponentResult"/> representing the result of the operation.</returns>
        public virtual IViewComponentResult Invoke(IViewComponentContext<ButtonEntity> context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var viewModel = this.model.InitializeViewModel(context.Entity);

            return this.View(viewModel);
        }
    }
}
