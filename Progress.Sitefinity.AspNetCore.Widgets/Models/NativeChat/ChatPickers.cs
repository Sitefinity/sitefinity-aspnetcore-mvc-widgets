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
    /// The ChatPickers enum.
    /// </summary>
    [Flags]
    public enum ChatPickers
    {
        /// <summary>
        /// FilePicker.
        /// </summary>
        [EnumDisplayName("File picker")]
        [Description("File picker")]
        FilePicker = 0x01,

        /// <summary>
        /// LocationPicker.
        /// </summary>
        [EnumDisplayName("Location picker")]
        [Description("Location picker")]
        LocationPicker = 0x02,
    }
}
