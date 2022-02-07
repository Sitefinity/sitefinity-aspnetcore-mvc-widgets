using System.ComponentModel;
using System.Linq;
using Progress.Sitefinity.AspNetCore.Configuration;
using Progress.Sitefinity.AspNetCore.ViewComponents.AttributeConfigurator.Attributes;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Button
{
    /// <summary>
    /// Specifies the styling opitions for the buttons.
    /// </summary>
    public class ButtonStyle
    {
        /// <summary>
        /// Gets or sets the alignment.
        /// </summary>
        [DisplayName("Display style for")]
        [DefaultValue("Primary")]
        [ConfigurationChoice("Widgets:Styling:ButtonClasses")]
        public string DisplayStyle { get; set; }
    }
}
