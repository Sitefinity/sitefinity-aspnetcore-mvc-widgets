using System.Collections.Generic;
using Progress.Sitefinity.AspNetCore.Configuration;
using Progress.Sitefinity.AspNetCore.FormWidgets.Entities;

namespace Progress.Sitefinity.AspNetCore.FormWidgets.ViewComponents.Common
{
    /// <summary>
    /// The styles generator for forms widgets.
    /// </summary>
    public class FormWidgetsStyleGenerator
    {
        private IWidgetConfig widgetConfiguration;
        private Dictionary<string, string> defaultFieldSizeClasses = new Dictionary<string, string>()
        {
            { "WidthNONE", string.Empty },
            { "WidthS", "w-50" },
            { "WidthM", "w-75" },
            { "WidthL", "w-100" },
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="FormWidgetsStyleGenerator"/> class.
        /// </summary>
        /// <param name="widgetConfig">The widget config.</param>
        public FormWidgetsStyleGenerator(IWidgetConfig widgetConfig)
        {
            this.widgetConfiguration = widgetConfig;
        }

        /// <summary>
        /// Gets the field size css.
        /// </summary>
        /// <param name="fieldSize">The field size.</param>
        /// <returns>The css.</returns>
        public string GetFieldSizeCss(FieldSize fieldSize)
        {
            var key = $"Width{fieldSize.ToString()}";

            if (this.widgetConfiguration.Styling.FieldSizeClasses.ContainsKey(key))
                return this.widgetConfiguration.Styling.FieldSizeClasses[key];
            else if (this.defaultFieldSizeClasses.ContainsKey(key))
                return this.defaultFieldSizeClasses[key];

            return string.Empty;
        }
    }
}
