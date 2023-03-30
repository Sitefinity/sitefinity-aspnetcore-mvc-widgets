using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        [Description("File picker")]
        FilePicker = 0x01,

        /// <summary>
        /// LocationPicker.
        /// </summary>
        [Description("Location picker")]
        LocationPicker = 0x02,
    }
}
