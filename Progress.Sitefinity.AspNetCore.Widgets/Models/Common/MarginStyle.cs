using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Progress.Sitefinity.AspNetCore.ViewComponents.AttributeConfigurator.Attributes;
using Progress.Sitefinity.Renderer.Designers;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Common
{
    /// <summary>
    /// Styles with available margins.
    /// </summary>
    public class MarginStyle : OffsetStyle
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MarginStyle"/> class.
        /// </summary>
        public MarginStyle()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MarginStyle"/> class.
        /// </summary>
        /// <param name="size">The offset size.</param>
        public MarginStyle(OffsetSize size)
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
        /// Gets or sets the right.
        /// </summary>
        [DisplayName("Right")]
        [ConfigurationDefaultValue("Widgets:Styling:DefaultMargin")]
        [DataType(customDataType: KnownFieldTypes.ChipChoice)]
        public override OffsetSize? Right { get; set; }

        /// <summary>
        /// Gets or sets the bottom.
        /// </summary>
        [DisplayName("Bottom")]
        [ConfigurationDefaultValue("Widgets:Styling:DefaultMargin")]
        [DataType(customDataType: KnownFieldTypes.ChipChoice)]
        public override OffsetSize? Bottom { get; set; }

        /// <summary>
        /// Gets or sets the left.
        /// </summary>
        [DisplayName("Left")]
        [ConfigurationDefaultValue("Widgets:Styling:DefaultMargin")]
        [DataType(customDataType: KnownFieldTypes.ChipChoice)]
        public override OffsetSize? Left { get; set; }
    }
}
