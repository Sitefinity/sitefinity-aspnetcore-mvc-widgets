using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Progress.Sitefinity.AspNetCore.Widgets.Models.ChangePassword;

namespace Progress.Sitefinity.AspNetCore.Widgets.ViewComponents
{
    /// <summary>
    /// The view component for the change password widget.
    /// </summary>
    [SitefinityWidget(Title = "Change password", Order = 3, Section = WidgetSection.Login, Category = WidgetCategory.LoginAndUsers)]
    [ViewComponent(Name = "SitefinityChangePassword")]
    public class ChangePasswordViewComponent : ViewComponent
    {
        private IChangePasswordModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChangePasswordViewComponent"/> class.
        /// </summary>
        /// <param name="changePasswordModel">The change password model.</param>
        public ChangePasswordViewComponent(IChangePasswordModel changePasswordModel)
        {
            this.model = changePasswordModel;
        }

        /// <summary>
        /// Invokes the change password widget creation.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public virtual async Task<IViewComponentResult> InvokeAsync(IViewComponentContext<ChangePasswordEntity> context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var viewModel = await this.model.InitializeViewModel(context.Entity);

            return this.View(context.Entity.SfViewName, viewModel);
        }
    }
}
