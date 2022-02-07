using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Registration;

namespace Progress.Sitefinity.AspNetCore.Widgets.ViewComponents
{
    /// <summary>
    /// The view component for the registration widget.
    /// </summary>
    [SitefinityWidget(Title = "Registration", Order = 2, Section = WidgetSection.Registration, Category = WidgetCategory.LoginAndUsers)]
    [ViewComponent(Name = "SitefinityRegistration")]
    public class RegistrationViewComponent : ViewComponent
    {
        private IRegistrationModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationViewComponent"/> class.
        /// </summary>
        /// <param name="registrationModel">The registration model.</param>
        public RegistrationViewComponent(IRegistrationModel registrationModel)
        {
            this.model = registrationModel;
        }

        /// <summary>
        /// Invokes the registration widget creation.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public virtual async Task<IViewComponentResult> InvokeAsync(IViewComponentContext<RegistrationEntity> context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var viewModel = await this.model.InitializeViewModel(context.Entity);
            return this.View(context.Entity.SfViewName, viewModel);
        }
    }
}
