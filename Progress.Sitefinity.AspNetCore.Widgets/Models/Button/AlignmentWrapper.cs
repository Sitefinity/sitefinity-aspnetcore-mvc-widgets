using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Progress.Sitefinity.Renderer.Designers;
using Progress.Sitefinity.Renderer.Designers.Attributes;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Button
{
    /// <summary>
    /// Defines a wrapper around Alignment class.
    /// </summary>
    public class AlignmentWrapper
    {
        /// <summary>
        /// Gets or sets the alignment.
        /// </summary>
        [DefaultValue("Left")]
        [DataType(customDataType: KnownFieldTypes.ChipChoice)]
        public Alignment Alignment { get; set; }
    }

    /// <summary>
    /// Enum with available alignments.
    /// </summary>
    public enum Alignment
    {
        /// <summary>
        /// Left.
        /// </summary>
        [Icon("align-left")]
        Left,

        /// <summary>
        /// Center.
        /// </summary>
        [Icon("align-center")]
        Center,

        /// <summary>
        /// Right.
        /// </summary>
        [Icon("align-right")]
        Right,

        /// <summary>
        /// Justify.
        /// </summary>
        [Icon("align-justify")]
        Justify,
    }
}
