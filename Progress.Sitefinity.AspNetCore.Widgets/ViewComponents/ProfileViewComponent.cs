using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Profile;

namespace Progress.Sitefinity.AspNetCore.Widgets.ViewComponents
{
    /// <summary>
    /// The view component for the Profile widget.
    /// </summary>
    [SitefinityWidget(Title = "Profile", Order = 5, Section = WidgetSection.LoginAndUsers, Category = WidgetCategory.Content, IconName = "profile", NotPersonalizable = true)]
    [ViewComponent(Name = "SitefinityProfile")]
    public class ProfileViewComponent : ViewComponent
    {
        private IProfileModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileViewComponent"/> class.
        /// </summary>
        /// <param name="profileModel">The reset password model.</param>
        public ProfileViewComponent(IProfileModel profileModel)
        {
            this.model = profileModel;
        }

        /// <summary>
        /// Invokes the Profile widget creation.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public virtual async Task<IViewComponentResult> InvokeAsync(IViewComponentContext<ProfileEntity> context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var viewModel = await this.model.InitializeViewModel(context.Entity);
            if (!string.IsNullOrEmpty(viewModel.Warning))
            {
                context.SetWarning(viewModel.Warning);
            }

            return this.View(context.Entity.SfViewName, viewModel);
        }
    }
}
