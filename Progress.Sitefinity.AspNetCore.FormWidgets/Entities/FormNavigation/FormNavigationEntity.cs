using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Progress.Sitefinity.AspNetCore.ViewComponents.AttributeConfigurator.Attributes;
using Progress.Sitefinity.Renderer.Designers;
using Progress.Sitefinity.Renderer.Designers.Attributes;

namespace Progress.Sitefinity.AspNetCore.FormWidgets.Entities.FormNavigation
{
    /// <summary>
    /// Entity for the form navigation widget.
    /// </summary>
    public class FormNavigationEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormNavigationEntity"/> class.
        /// </summary>
        public FormNavigationEntity()
        {
            this.NavigationSteps = new List<string>();
        }

        /// <summary>
        /// Gets or sets the navigation steps of the widget.
        /// </summary>
        [DisplayName("Navigation steps")]
        [DataType(customDataType: "formNavigationSteps")]
        public IEnumerable<string> NavigationSteps { get; set; }

        /// <summary>
        /// Gets or sets the view name.
        /// </summary>
        [ViewSelector]
        [ContentSection(Constants.ContentSectionTitles.DisplaySettings)]
        [DisplayName("Template")]
        public string SfViewName { get; set; }

        /// <summary>
        /// Gets or sets the CSS class.
        /// </summary>
        [ContentSection(Constants.ContentSectionTitles.DisplaySettings)]
        [DisplayName("CSS class")]
        public string CssClass { get; set; }
    }
}
