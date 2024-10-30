using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Progress.Sitefinity.Renderer.Designers.Attributes;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.NativeChat
{
    /// <summary>
    /// The ChatWindowMode enum.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "Should be lowercase.")]
    public enum ChatWindowMode
    {
        /// <summary>
        /// Modal.
        /// </summary>
        [EnumDisplayName("Display modal")]
        [Description("Display modal")]
        modal,

        /// <summary>
        /// Inline.
        /// </summary>
        [EnumDisplayName("Display inline")]
        [Description("Display inline")]
        inline,
    }
}
