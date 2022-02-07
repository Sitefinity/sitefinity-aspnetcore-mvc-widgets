using System.Collections.Generic;
using Progress.Sitefinity.AspNetCore.Models;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Navigation
{
    /// <summary>
    /// The view model for the navigation widget.
    /// </summary>
    public class NavigationViewModel
    {
        private IList<PageViewModel> nodes;

        /// <summary>
        /// Gets or sets the list of site map nodes that will be displayed in the navigation widget.
        /// </summary>
        /// <value>
        /// The nodes.
        /// </value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "DTO")]
        public IList<PageViewModel> Nodes
        {
            get
            {
                return this.nodes ?? (this.nodes = new List<PageViewModel>());
            }

            set
            {
                this.nodes = value;
            }
        }

        /// <summary>
        /// Gets or sets the styles.
        /// </summary>
        public string WrapperCssClass { get; set; }

        /// <summary>
        /// Gets or sets the attributes for navigation widget.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Must be able to set in property editor.")]
        public IDictionary<string, IList<AttributeModel>> Attributes { get; set; }
    }
}
