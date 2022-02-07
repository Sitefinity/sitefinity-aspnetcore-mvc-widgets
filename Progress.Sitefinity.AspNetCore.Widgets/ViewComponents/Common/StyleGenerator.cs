using System;
using System.Linq;
using Progress.Sitefinity.AspNetCore.Configuration;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Button;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Common;

namespace Progress.Sitefinity.AspNetCore.Widgets.ViewComponents.Common
{
    internal class StyleGenerator : IStyleClassesProvider
    {
        public StyleGenerator(IWidgetConfig widgetConfig)
        {
            if (widgetConfig == null)
                throw new ArgumentNullException(nameof(widgetConfig));

            this.StylingConfig = widgetConfig.Styling;

            OffsetSize margin, padding;
            if (Enum.TryParse(this.StylingConfig.DefaultMargin, out margin))
            {
                this.DefaultMargin = margin;
            }

            if (Enum.TryParse(this.StylingConfig.DefaultPadding, out padding))
            {
                this.DefaultPadding = padding;
            }
        }

        public StylingConfig StylingConfig { get; private set; }

        public OffsetSize DefaultMargin { get; private set; } = OffsetSize.None;

        public OffsetSize DefaultPadding { get; private set; } = OffsetSize.None;

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
            ChoiceConfig displayStyle = this.StylingConfig.ButtonClasses.FirstOrDefault(c => c.Key == buttonType);
            if (displayStyle == null)
            {
                displayStyle = GetDafultBtnClasses().FirstOrDefault(c => c.Key == buttonType);
            }

            if (displayStyle != null)
            {
                return displayStyle.Value;
            }

            return string.Empty;
        }

        private static ChoiceConfig[] GetDafultBtnClasses()
        {
            var buttonClasses = new ChoiceConfig[]
            {
                new ChoiceConfig()
                {
                    Key = "Primary",
                    Value = "btn btn-primary",
                    Title = "Primary action",
                },
                new ChoiceConfig()
                {
                    Key = "Secondary",
                    Value = "btn btn-secondary",
                    Title = "Secondary action",
                },
                new ChoiceConfig()
                {
                    Key = "Link",
                    Value = "Link",
                    Title = string.Empty,
                },
            };

            return buttonClasses;
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
