using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Progress.Sitefinity.AspNetCore.Configuration;
using Progress.Sitefinity.AspNetCore.Widgets.ViewComponents.Common;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.IntentDrivenContent
{
    /// <summary>
    /// The model for the IntentDrivenContent widget.
    /// </summary>
    public class IntentDrivenContentModel : IIntentDrivenContentModel
    {
        private readonly IStyleClassesProvider styles;
        private readonly ISitefinityConfig config;

        /// <summary>
        /// Initializes a new instance of the <see cref="IntentDrivenContentModel"/> class.
        /// </summary>
        /// <param name="sitefinityConfig">The current Sitefinity configuration.</param>
        /// <param name="styles">The provider for style classes used to generate CSS classes for the widget.</param>
        public IntentDrivenContentModel(ISitefinityConfig sitefinityConfig, IStyleClassesProvider styles)
        {
            this.styles = styles;
            this.config = sitefinityConfig;
        }

        /// <inheritdoc/>
        public virtual Task<IntentDrivenContentViewModel> InitializeViewModel(IntentDrivenContentEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var viewModel = new IntentDrivenContentViewModel();
            viewModel.DefaultQuery = entity.NoProvidedIntent == NoIntentAction.GenerateWithPredefinedQuery ? entity.DefaultQuery : null;
            var relativeServicePath = this.config.WebServicePath.StartsWith('/') ? this.config.WebServicePath : "/" + this.config.WebServicePath;
            viewModel.ServiceUrl = $"{relativeServicePath}/DynamicExperience/Content";

            viewModel.SectionNames = new List<string>();
            viewModel.SectionsConfiguration = new List<SectionViewModel>();

            var margins = this.styles.GetMarginsClasses(entity);
            viewModel.CssClass = (entity.ContentCssClass + " " + margins).Trim();

            if (entity.SectionsConfiguration == null || !entity.SectionsConfiguration.Any())
            {
                return Task.FromResult(viewModel);
            }

            foreach (var section in entity.SectionsConfiguration)
            {
                var sectionName = this.GetEnumSectionName(section.SectionType);
                viewModel.SectionNames.Add(sectionName);

                viewModel.SectionsConfiguration.Add(new SectionViewModel
                {
                    SectionType = section.SectionType,
                    CssClass = this.GetSectionCssClass(entity, section.SectionType)
                });
            }

            return Task.FromResult(viewModel);
        }

        private string GetSectionCssClass(IntentDrivenContentEntity entity, SectionType sectionType)
        {
            return sectionType switch
            {
                SectionType.TitleAndSummary => entity.PageTitleAndSummaryCssClass,
                SectionType.RichText => entity.RichTextCssClass,
                SectionType.FAQ => entity.FaqCssClass,
                SectionType.Hero => entity.HeroCssClass,
                SectionType.ContentList => entity.ContentItemsListCssClass,
                SectionType.ContentListCards => entity.ContentItemsCardsCssClass,
                SectionType.Error => null,
                _ => null
            };
        }

        private string GetEnumSectionName(SectionType sectionType)
        {
            var type = typeof(SectionType);
            var memInfo = type.GetMember(sectionType.ToString());
            var attributes = memInfo.FirstOrDefault()?.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
            var sectionName = ((System.ComponentModel.DescriptionAttribute)attributes.FirstOrDefault())?.Description;
            if (!string.IsNullOrEmpty(sectionName) && sectionName.Contains("_", StringComparison.InvariantCultureIgnoreCase))
            {
                var parts = sectionName.Split('_');

                return parts[0];
            }

            return sectionName;
        }
    }
}
