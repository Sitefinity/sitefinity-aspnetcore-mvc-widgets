using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Progress.Sitefinity.AspNetCore.Widgets.Models.LoginForm;

namespace Progress.Sitefinity.AspNetCore.Widgets.ViewComponents
{
    /// <summary>
    /// The view component for the login form widget.
    /// </summary>
    [SitefinityWidget(Title = "Login form", Order = 2, Section = WidgetSection.Login, Category = WidgetCategory.LoginAndUsers)]
    [ViewComponent(Name = "SitefinityLoginForm")]
    public class LoginFormViewComponent : ViewComponent
    {
        private ILoginFormModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginFormViewComponent"/> class.
        /// </summary>
        /// <param name="loginFormModel">The login form model.</param>
        public LoginFormViewComponent(ILoginFormModel loginFormModel)
        {
            this.model = loginFormModel;
        }

        /// <summary>
        /// Invokes the login form widget creation.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public virtual async Task<IViewComponentResult> InvokeAsync(IViewComponentContext<LoginFormEntity> context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var viewModel = await this.model.InitializeViewModel(context.Entity);
            return this.View(context.Entity.SfViewName, viewModel);
        }
    }
}
