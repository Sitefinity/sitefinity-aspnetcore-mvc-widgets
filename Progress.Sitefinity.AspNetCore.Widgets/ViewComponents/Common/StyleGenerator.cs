using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Progress.Sitefinity.AspNetCore.Configuration;
using Progress.Sitefinity.AspNetCore.Http;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Button;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Common;

namespace Progress.Sitefinity.AspNetCore.Widgets.ViewComponents.Common
{
    internal class StyleGenerator : IStyleClassesProvider
    {
        private IHttpContextAccessor httpContextAccessor;
        private StylingConfig stylingConfig;

        public StyleGenerator(IWidgetConfig widgetConfig, IHttpContextAccessor httpContextAccessor)
        {
            if (widgetConfig == null)
                throw new ArgumentNullException(nameof(widgetConfig));

            if (httpContextAccessor == null)
                throw new ArgumentNullException(nameof(httpContextAccessor));

            this.httpContextAccessor = httpContextAccessor;
            this.stylingConfig = widgetConfig.Styling;
        }

        public StylingConfig StylingConfig
        {
            get
            {
                var config = this.httpContextAccessor.HttpContext.GetStylingConfigForCurrentPackage();
                if (config != null)
                    return config;

                return this.stylingConfig;
            }
        }

        public OffsetSize DefaultMargin
        {
            get
            {
                if (Enum.TryParse(this.StylingConfig.DefaultMargin, out OffsetSize margin))
                    return margin;

                return OffsetSize.None;
            }
        }

        public OffsetSize DefaultPadding
        {
            get
            {
                if (Enum.TryParse(this.StylingConfig.DefaultPadding, out OffsetSize margin))
                    return margin;

                return OffsetSize.None;
            }
        }

        public string GetMarginsClasses<T>(IHasMargins<T> entity)
            where T : OffsetStyleBase
        {
            return this.GetMarginsClasses(entity.Margins);
        }

        public string GetMarginsClasses(OffsetStyleBase margins)
        {
            return margins != null ? margins.GetClasses(this.StylingConfig, "Margin", this.DefaultMargin) : OffsetStyle.GetDefaultClasses(this.StylingConfig, "Margin", this.DefaultMargin);
        }

        public string GetPaddingsClasses<T>(IHasPaddings<T> entity)
            where T : OffsetStyleBase
        {
            return this.GetPaddingsClasses(entity.Paddings);
        }

        public string GetPaddingsClasses(OffsetStyleBase margins)
        {
            return margins != null ? margins.GetClasses(this.StylingConfig, "Padding", this.DefaultPadding) : OffsetStyle.GetDefaultClasses(this.StylingConfig, "Padding", this.DefaultPadding);
        }

        public string GetAlignmenClasses(IHasPosition entity)
        {
            return entity.Position != null && entity.Position.Any() ? GetAlignmentClasses(this.StylingConfig, entity.Position.First().Value.Alignment) : GetAlignmentClasses(this.StylingConfig, Alignment.Left);
        }

        /// <inheritdoc/>
        public string GetConfiguredButtonClasses(string buttonType)
        {
            var displayStyle = this.StylingConfig.ButtonClasses.FirstOrDefault(c => c.Key == buttonType);
            if (displayStyle != null)
            {
                return displayStyle.Value;
            }

            return null;
        }

        /// <inheritdoc/>
        public string GetDefaultButtonClass()
        {
            if (this.StylingConfig.ButtonClasses.Count > 0)
                return this.StylingConfig.ButtonClasses[0].Value;

            return null;
        }

        private static string GetAlignmentClasses(StylingConfig stylingConfig, Alignment position)
        {
            string alignment;
            if (stylingConfig.AlignmentClasses.TryGetValue(position.ToString(), out alignment))
            {
                return alignment;
            }

            return string.Empty;
        }
    }
}
