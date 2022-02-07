using System.ComponentModel;
using Progress.Sitefinity.AspNetCore.FormWidgets.Entities.Common;
using Progress.Sitefinity.Renderer.Contracts.Forms;
using Progress.Sitefinity.Renderer.Designers;
using Progress.Sitefinity.Renderer.Designers.Attributes;

namespace Progress.Sitefinity.AspNetCore.FormWidgets.Entities.TextField
{
    /// <summary>
    /// Entity for holding the text field specific settings.
    /// </summary>
    public class TextFieldEntity : TextEntityBase, ITextFieldContract
    {
        /// <summary>
        /// Gets or sets the type of the input element.
        /// </summary>
        [ContentSection(Constants.ContentSectionTitles.LabelsAndContent, 3)]
        [DisplayName("Field type")]
        public TextType InputType { get; set; }

        /// <inheritdoc/>
        [Category(PropertyCategory.Advanced)]
        [ContentSection("AdvancedMain", 3)]
        [DisplayName("Regular expression validation pattern")]
        public string RegularExpression { get; set; }

        /// <inheritdoc/>
        [Category(PropertyCategory.Advanced)]
        [ContentSection("AdvancedMain", 4)]
        [DisplayName("Regular expression error message")]
        public string RegularExpressionViolationMessage { get; set; }
    }
}
