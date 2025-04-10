using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Progress.Sitefinity.AspNetCore.Configuration;
using Progress.Sitefinity.AspNetCore.Preparations;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Progress.Sitefinity.AspNetCore.Widgets.Attributes;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Breadcrumb;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Button;
using Progress.Sitefinity.AspNetCore.Widgets.Models.ChangePassword;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Classification;
using Progress.Sitefinity.AspNetCore.Widgets.Models.ContentBlock;
using Progress.Sitefinity.AspNetCore.Widgets.Models.ContentList;
using Progress.Sitefinity.AspNetCore.Widgets.Models.DocumentList;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Facets;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Form;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Image;
using Progress.Sitefinity.AspNetCore.Widgets.Models.LoginForm;
using Progress.Sitefinity.AspNetCore.Widgets.Models.NativeChat;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Navigation;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Profile;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Registration;
using Progress.Sitefinity.AspNetCore.Widgets.Models.ResetPassword;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Search;
using Progress.Sitefinity.AspNetCore.Widgets.Models.SearchResults;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Section;
using Progress.Sitefinity.AspNetCore.Widgets.Models.SitefinityAssistant;
using Progress.Sitefinity.AspNetCore.Widgets.Preparations;
using Progress.Sitefinity.AspNetCore.Widgets.ViewComponents.Common;
using Progress.Sitefinity.Renderer.Designers;

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
            services.AddTransient<IProfileModel, ProfileModel>();
            services.AddTransient<INavigationModel, NavigationModel>();
            services.AddTransient<IImageModel, ImageModel>();
            services.AddTransient<ISectionModel, SectionModel>();
            services.AddTransient<IButtonModel, ButtonModel>();
            services.AddTransient<IContentListModel, ContentListModel>();
            services.AddTransient<IClassificationModel, ClassificationModel>();
            services.AddTransient<ISearchBoxModel, SearchBoxModel>();
            services.AddTransient<IRequestPreparation, ContentListPreparation>();
            services.AddTransient<IRequestPreparation, DocumentListPreparation>();
            services.AddTransient<INativeChatModel, NativeChatModel>();
            services.AddTransient<ISitefinityAssistantModel, SitefinityAssistantModel>();
            services.AddTransient<IDocumentListModel, DocumentListModel>();
            services.AddTransient<IContentListModelBase, ContentListModel>();
            services.AddTransient<IContentListModelBase, DocumentListModel>();

            services.AddTransient<IFormModel, FormModel>();
            services.AddTransient<ISearchResultsModel, SearchResultsModel>();
            services.AddTransient<IFacetsModel, FacetsModel>();
            services.AddTransient<IBreadcrumbModel, BreadcrumbModel>();
            services.AddSingleton<IStyleClassesProvider>(serviceProvider =>
            {
                var httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
                return new StyleGenerator(serviceProvider.GetRequiredService<IWidgetConfig>(), httpContextAccessor);
            });
            services.AddSingleton<INativeChatClient, NativeChatClient>();
            services.AddSingleton<ISitefinityAssistantClient, SitefinityAssistantClient>();
            services.AddSingleton<ISitefinityAssistantCDN, SitefinityAssistantCDN>();
            services.AddSingleton<IExternalChoicesProvider>(provider => provider.GetRequiredService<INativeChatClient>());
            services.AddSingleton<IExternalChoicesProvider>(provider => provider.GetRequiredService<ISitefinityAssistantClient>());
            services.AddSingleton<IPropertyConfigurator, ExternalPropertyConfigurator>();
        }
    }
}
