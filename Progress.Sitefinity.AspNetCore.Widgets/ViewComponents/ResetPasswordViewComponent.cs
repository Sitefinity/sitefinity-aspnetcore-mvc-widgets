using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Progress.Sitefinity.AspNetCore.Widgets.Models.ResetPassword;

namespace Progress.Sitefinity.AspNetCore.Widgets.ViewComponents
{
    /// <summary>
    /// The view component for the reset password widget.
    /// </summary>
    [SitefinityWidget(Title = "Reset password", Order = 4, Section = WidgetSection.Login, Category = WidgetCategory.LoginAndUsers)]
    [ViewComponent(Name = "SitefinityResetPassword")]
    public class ResetPasswordViewComponent : ViewComponent
    {
        private IResetPasswordModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResetPasswordViewComponent"/> class.
        /// </summary>
        /// <param name="resetPasswordModel">The reset password model.</param>
        public ResetPasswordViewComponent(IResetPasswordModel resetPasswordModel)
        {
            this.model = resetPasswordModel;
        }

        /// <summary>
        /// Invokes the login form widget creation.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public virtual async Task<IViewComponentResult> InvokeAsync(IViewComponentContext<ResetPasswordEntity> context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var viewModel = await this.model.InitializeViewModel(context.Entity);
            return this.View(context.Entity.SfViewName, viewModel);
        }
    }
}
