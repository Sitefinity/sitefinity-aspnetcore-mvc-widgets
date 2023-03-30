using Microsoft.Extensions.DependencyInjection;
using Progress.Sitefinity.AspNetCore.FormWidgets.Models.ContentBlock;
using Progress.Sitefinity.AspNetCore.FormWidgets.Models.Dropdown;
using Progress.Sitefinity.AspNetCore.FormWidgets.Models.DynamicList;
using Progress.Sitefinity.AspNetCore.FormWidgets.Models.FileField;
using Progress.Sitefinity.AspNetCore.FormWidgets.Models.FormSection;
using Progress.Sitefinity.AspNetCore.FormWidgets.Models.MultipleChoice;
using Progress.Sitefinity.AspNetCore.FormWidgets.Models.Paragraph;
using Progress.Sitefinity.AspNetCore.FormWidgets.Models.SubmitButton;
using Progress.Sitefinity.AspNetCore.FormWidgets.Models.TextField;
using Progress.Sitefinity.AspNetCore.FormWidgets.ViewComponents.Common;

namespace Progress.Sitefinity.AspNetCore.FormWidgets
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
        public static void AddFormViewComponentModels(this IServiceCollection services)
        {
            services.AddTransient<ITextFieldModel, TextFieldModel>();
            services.AddTransient<ISubmitButtonModel, SubmitButtonModel>();
            services.AddTransient<IFormSectionModel, FormSectionModel>();
            services.AddTransient<IParagraphModel, ParagraphModel>();
            services.AddTransient<IMultipleChoiceModel, MultipleChoiceModel>();
            services.AddTransient<IDropdownModel, DropdownModel>();
            services.AddTransient<IFileFieldModel, FileFieldModel>();
            services.AddTransient<IContentBlockModel, ContentBlockModel>();
            services.AddTransient<IDynamicListModel, DynamicListModel>();

            services.AddSingleton<FormWidgetsStyleGenerator>();
        }
    }
}
