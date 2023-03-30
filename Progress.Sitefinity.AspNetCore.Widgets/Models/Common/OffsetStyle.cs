using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Progress.Sitefinity.AspNetCore.Configuration;
using Progress.Sitefinity.AspNetCore.Models.Common;
using Progress.Sitefinity.Renderer.Designers;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Common
{
    /// <summary>
    /// Styles with available paddings and margins.
    /// </summary>
    public class OffsetStyle : VerticalOffsetStyle
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OffsetStyle"/> class.
        /// </summary>
        public OffsetStyle()
            : base()
        {
            this.Left = OffsetSize.None;
            this.Right = OffsetSize.None;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OffsetStyle"/> class.
        /// </summary>
        /// <param name="size">The offset size.</param>
        public OffsetStyle(OffsetSize size)
            : base(size)
        {
            this.Left = size;
            this.Right = size;
        }

        /// <summary>
        /// Gets or sets the right.
        /// </summary>
        [DisplayName("Right")]
        [DefaultValue("None")]
        [DataType(customDataType: KnownFieldTypes.ChipChoice)]
        public virtual OffsetSize? Right { get; set; }

        /// <summary>
        /// Gets or sets the left.
        /// </summary>
        [DisplayName("Left")]
        [DefaultValue("None")]
        [DataType(customDataType: KnownFieldTypes.ChipChoice)]
        public virtual OffsetSize? Left { get; set; }

        /// <summary>
        /// Gets the classes for the columns.
        /// </summary>
        /// <param name="stylingConfig">The styling config.</param>
        /// <param name="offsetType">The offset type (padding, margin).</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The CSS classes.</returns>
        public override string GetClasses(StylingConfig stylingConfig, string offsetType, OffsetSize defaultValue = OffsetSize.None)
        {
            var classes = new StringBuilder(0);
            VerticalOffsetStyle.AddClassesForDirection(stylingConfig, offsetType, "Top", this.Top, defaultValue, classes);
            VerticalOffsetStyle.AddClassesForDirection(stylingConfig, offsetType, "Right", this.Right, defaultValue, classes);
            VerticalOffsetStyle.AddClassesForDirection(stylingConfig, offsetType, "Bottom", this.Bottom, defaultValue, classes);
            VerticalOffsetStyle.AddClassesForDirection(stylingConfig, offsetType, "Left", this.Left, defaultValue, classes);

            return classes.ToString().TrimEnd();
        }
    }
}
