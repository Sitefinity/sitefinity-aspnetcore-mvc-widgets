using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.ContentList
{
    /// <summary>
    /// Defines field mapping for the frontend views.
    /// </summary>
    public class FieldMapping
    {
        /// <summary>
        /// Gets or sets the friendly name used to access field.
        /// <remarks>This name is ment to be displayed in the widget editor and in the widget templates.</remarks>
        /// </summary>
        public string FriendlyName { get; set; }

        /// <summary>
        /// Gets or sets the name of the field. It`s the same as in the content type.
        /// </summary>
        public string Name { get; set; }
    }
}
