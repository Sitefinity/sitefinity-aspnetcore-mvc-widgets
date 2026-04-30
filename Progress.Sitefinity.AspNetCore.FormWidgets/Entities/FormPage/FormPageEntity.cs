using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Progress.Sitefinity.AspNetCore.ViewComponents.AttributeConfigurator.Attributes;
using Progress.Sitefinity.Renderer.Designers;
using Progress.Sitefinity.Renderer.Designers.Attributes;

namespace Progress.Sitefinity.AspNetCore.FormWidgets.Entities.FormPage
{
    /// <summary>
    /// Entity for the form page widget.
    /// </summary>
    public class FormPageEntity
    {
        /// <summary>
        /// Gets or sets the label of the form widget.
        /// </summary>
        [DisplayName("Page label")]
        [DefaultValue("Step")]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndContent)]
        public string PageLabel { get; set; }

        /// <summary>
        /// Gets or sets the button label of the form widget.
        /// </summary>
        [DisplayName("Button label")]
        [DefaultValue("Next")]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndContent)]
        public string ButtonLabel { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether users are allowed to step backward.
        /// </summary>
        [DisplayName("Allow users to step backward")]
        [DefaultValue(true)]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndContent)]
        [Choice("[{\"Title\":\"Yes\",\"Name\":\"Yes\",\"Value\":true,\"Icon\":null},{\"Title\":\"No\",\"Name\":\"No\",\"Value\":false,\"Icon\":null}]")]
        [DataType(customDataType: KnownFieldTypes.ChipChoice)]
        public bool AllowStepBackward { get; set; }

        /// <summary>
        /// Gets or sets the button label of the form widget.
        /// </summary>
        [DisplayName("Back link label")]
        [DefaultValue("Back")]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndContent)]
        [ConditionalVisibility("{\"conditions\":[{\"fieldName\":\"AllowStepBackward\",\"operator\":\"Equals\",\"value\":true}]}")]
        public string BackLinkLabel { get; set; }

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
