using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Progress.Sitefinity.AspNetCore.Models.Common;
using Progress.Sitefinity.AspNetCore.ViewComponents.AttributeConfigurator.Attributes;
using Progress.Sitefinity.Renderer.Designers;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Common
{
    /// <summary>
    /// Styles with available vertical margins.
    /// </summary>
    public class MarginVerticalStyle : VerticalOffsetStyle
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MarginVerticalStyle"/> class.
        /// </summary>
        public MarginVerticalStyle()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MarginVerticalStyle"/> class.
        /// </summary>
        /// <param name="size">The offset size.</param>
        public MarginVerticalStyle(OffsetSize size)
            : base(size)
        {
        }

        /// <summary>
        /// Gets or sets the top.
        /// </summary>
        [DisplayName("Top")]
        [ConfigurationDefaultValue("Widgets:Styling:DefaultMargin")]
        [DataType(customDataType: KnownFieldTypes.ChipChoice)]
        public override OffsetSize? Top { get; set; }

        /// <summary>
        /// Gets or sets the bottom.
        /// </summary>
        [DisplayName("Bottom")]
        [ConfigurationDefaultValue("Widgets:Styling:DefaultMargin")]
        [DataType(customDataType: KnownFieldTypes.ChipChoice)]
        public override OffsetSize? Bottom { get; set; }
    }
}
