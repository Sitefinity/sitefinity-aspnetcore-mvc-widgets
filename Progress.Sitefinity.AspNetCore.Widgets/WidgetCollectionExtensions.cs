using Microsoft.Extensions.DependencyInjection;
using Progress.Sitefinity.AspNetCore.Configuration;
using Progress.Sitefinity.AspNetCore.Preparations;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Button;
using Progress.Sitefinity.AspNetCore.Widgets.Models.ChangePassword;
using Progress.Sitefinity.AspNetCore.Widgets.Models.ContentBlock;
using Progress.Sitefinity.AspNetCore.Widgets.Models.ContentList;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Form;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Image;
using Progress.Sitefinity.AspNetCore.Widgets.Models.LoginForm;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Navigation;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Registration;
using Progress.Sitefinity.AspNetCore.Widgets.Models.ResetPassword;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Search;
using Progress.Sitefinity.AspNetCore.Widgets.Models.SearchResults;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Section;
using Progress.Sitefinity.AspNetCore.Widgets.Preparations;
using Progress.Sitefinity.AspNetCore.Widgets.ViewComponents.Common;

namespace Progress.Sitefinity.AspNetCore
{
    /// <summary>
    /// The extensions for the service collection.
    /// </summary>
    public static class WidgetCollectionExtensions
    {
        /// <summary>
        /// Adds widget services to the specified <see cref="IServiceCollection" />.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
        public static void AddViewComponentModels(this IServiceCollection services)
        {
            services.AddTransient<IChangePasswordModel, ChangePasswordModel>();
            services.AddTransient<IContentBlockModel, ContentBlockModel>();
            services.AddTransient<ILoginFormModel, LoginFormModel>();
            services.AddTransient<IResetPasswordModel, ResetPasswordModel>();
            services.AddTransient<IRegistrationModel, RegistrationModel>();
            services.AddTransient<INavigationModel, NavigationModel>();
            services.AddTransient<IImageModel, ImageModel>();
            services.AddTransient<ISectionModel, SectionModel>();
            services.AddTransient<IButtonModel, ButtonModel>();
            services.AddTransient<IContentListModel, ContentListModel>();
            services.AddTransient<ISearchBoxModel, SearchBoxModel>();
            services.AddTransient<IRequestPreparation, NavigationPreparation>();
            services.AddTransient<IRequestPreparation, ImagePreparation>();
            services.AddTransient<IRequestPreparation, ContentListPreparation>();
            services.AddTransient<IFormModel, FormModel>();
            services.AddTransient<ISearchResultsModel, SearchResultsModel>();
            services.AddSingleton<IStyleClassesProvider>(serviceProvider => new StyleGenerator(serviceProvider.GetRequiredService<IWidgetConfig>()));
        }
    }
}
