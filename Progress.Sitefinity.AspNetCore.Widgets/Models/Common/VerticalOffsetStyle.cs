using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Progress.Sitefinity.AspNetCore.Configuration;
using Progress.Sitefinity.AspNetCore.Models.Common;
using Progress.Sitefinity.Renderer.Designers;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Common
{
    /// <summary>
    /// Styles with available vertical paddings and margins.
    /// </summary>
    public class VerticalOffsetStyle : OffsetStyleBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VerticalOffsetStyle"/> class.
        /// </summary>
        public VerticalOffsetStyle()
        {
            this.Top = OffsetSize.None;
            this.Bottom = OffsetSize.None;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VerticalOffsetStyle"/> class.
        /// </summary>
        /// <param name="size">The offset size.</param>
        public VerticalOffsetStyle(OffsetSize size)
        {
            this.Top = size;
            this.Bottom = size;
        }

        /// <summary>
        /// Gets or sets the top.
        /// </summary>
        [DisplayName("Top")]
        [DefaultValue("None")]
        [DataType(customDataType: KnownFieldTypes.ChipChoice)]
        public virtual OffsetSize? Top { get; set; }

        /// <summary>
        /// Gets or sets the bottom.
        /// </summary>
        [DisplayName("Bottom")]
        [DefaultValue("None")]
        [DataType(customDataType: KnownFieldTypes.ChipChoice)]
        public virtual OffsetSize? Bottom { get; set; }

        /// <inheritdoc/>
        public override string GetClasses(StylingConfig stylingConfig, string offsetType, OffsetSize defaultValue = OffsetSize.None)
        {
            var classes = new StringBuilder(0);
            AddClassesForDirection(stylingConfig, offsetType, "Top", this.Top, defaultValue, classes);
            AddClassesForDirection(stylingConfig, offsetType, "Bottom", this.Bottom, defaultValue, classes);

            return classes.ToString().TrimEnd();
        }
    }
}
