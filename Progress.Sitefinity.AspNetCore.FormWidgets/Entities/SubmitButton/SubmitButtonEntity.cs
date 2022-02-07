using System.ComponentModel;
using Progress.Sitefinity.AspNetCore.ViewComponents.AttributeConfigurator.Attributes;
using Progress.Sitefinity.Renderer.Designers;
using Progress.Sitefinity.Renderer.Designers.Attributes;

namespace Progress.Sitefinity.AspNetCore.FormWidgets.Entities.SubmitButton
{
    /// <summary>
    /// Entity for holding the submit button specific settings.
    /// </summary>
    public class SubmitButtonEntity
    {
        /// <summary>
        /// Gets or sets the label of the form widget.
        /// </summary>
        [DefaultValue("Submit")]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndContent)]
        public string Label { get; set; }

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
        [Category(PropertyCategory.Advanced)]
        [DisplayName("CSS class")]
        public string CssClass { get; set; }
    }
}
