using System;
using Progress.Sitefinity.AspNetCore.Widgets.ViewComponents.Common;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Button
{
    /// <summary>
    /// The model for the Button(CTA) widget.
    /// </summary>
    public class ButtonModel : IButtonModel
    {
        private readonly IStyleClassesProvider styles;

        /// <summary>
        /// Initializes a new instance of the <see cref="ButtonModel"/> class.
        /// </summary>
        /// <param name="styles">The widget config.</param>
        public ButtonModel(IStyleClassesProvider styles)
        {
            this.styles = styles;
        }

        /// <inheritdoc/>
        public virtual ButtonViewModel InitializeViewModel(ButtonEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var viewModel = new ButtonViewModel();
            viewModel.PrimaryActionLabel = entity.PrimaryActionLabel;
            viewModel.SecondaryActionLabel = entity.SecondaryActionLabel;
            viewModel.PrimaryActionLink = entity.PrimaryActionLink;
            viewModel.SecondaryActionLink = entity.SecondaryActionLink;

            var margins = this.styles.GetMarginsClasses(entity);
            var alignment = this.styles.GetAlignmenClasses(entity);
            viewModel.CssClass = (entity.CssClass + " " + margins + " " + alignment).Trim();
            var primary = "Primary";
            var secondary = "Secondary";

            viewModel.PrimaryButtonCssClass = this.GetButtonCss(primary, entity);
            viewModel.SecondaryButtonCssClass = this.GetButtonCss(secondary, entity);
            viewModel.Attributes = entity.Attributes;

            return viewModel;
        }

        private string GetButtonCss(string buttonKey, ButtonEntity entity)
        {
            string returnValue = null;
            if (entity.Style != null && entity.Style.ContainsKey(buttonKey))
            {
                var displayStyle = entity.Style[buttonKey].DisplayStyle;
                var resolvedStyle = this.styles.GetConfiguredButtonClasses(displayStyle);
                if (!string.IsNullOrEmpty(resolvedStyle))
                {
                    returnValue = resolvedStyle;
                }
                else
                {
                    // For backward compatibility because previously the style value was stored in the DB
                    returnValue = displayStyle;
                }
            }
            else
            {
                returnValue = this.styles.GetConfiguredButtonClasses(buttonKey);
            }

            return returnValue ?? string.Empty;
        }
    }
}
