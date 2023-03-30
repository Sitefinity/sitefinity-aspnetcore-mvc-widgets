using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Progress.Sitefinity.AspNetCore.Models.Common;
using Progress.Sitefinity.AspNetCore.ViewComponents.AttributeConfigurator.Attributes;
using Progress.Sitefinity.Renderer.Designers;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Common
{
    /// <summary>
    /// Styles with available paddings.
    /// </summary>
    public class PaddingStyle : OffsetStyle
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PaddingStyle"/> class.
        /// </summary>
        public PaddingStyle()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PaddingStyle"/> class.
        /// </summary>
        /// <param name="size">The offset size.</param>
        public PaddingStyle(OffsetSize size)
            : base(size)
        {
        }

        /// <summary>
        /// Gets or sets the top.
        /// </summary>
        [DisplayName("Top")]
        [ConfigurationDefaultValue("Widgets:Styling:DefaultPadding")]
        [DataType(customDataType: KnownFieldTypes.ChipChoice)]
        public override OffsetSize? Top { get; set; }

        /// <summary>
        /// Gets or sets the right.
        /// </summary>
        [DisplayName("Right")]
        [ConfigurationDefaultValue("Widgets:Styling:DefaultPadding")]
        [DataType(customDataType: KnownFieldTypes.ChipChoice)]
        public override OffsetSize? Right { get; set; }

        /// <summary>
        /// Gets or sets the bottom.
        /// </summary>
        [DisplayName("Bottom")]
        [ConfigurationDefaultValue("Widgets:Styling:DefaultPadding")]
        [DataType(customDataType: KnownFieldTypes.ChipChoice)]
        public override OffsetSize? Bottom { get; set; }

        /// <summary>
        /// Gets or sets the left.
        /// </summary>
        [DisplayName("Left")]
        [ConfigurationDefaultValue("Widgets:Styling:DefaultPadding")]
        [DataType(customDataType: KnownFieldTypes.ChipChoice)]
        public override OffsetSize? Left { get; set; }
    }
}
