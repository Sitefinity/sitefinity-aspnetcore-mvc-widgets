using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        [Description("Display modal")]
        modal,

        /// <summary>
        /// Inline.
        /// </summary>
        [Description("Display inline")]
        inline,
    }
}
