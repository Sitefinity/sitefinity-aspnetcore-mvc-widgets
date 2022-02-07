using System.Collections.Generic;
using System.Runtime.Serialization;
using Progress.Sitefinity.AspNetCore.Models;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Navigation
{
    /// <summary>
    /// This class represents the model of the Nodes that will be rendered inside the Navigation templates.
    /// </summary>
    public class PageViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PageViewModel"/> class.
        /// </summary>
        public PageViewModel()
        {
            this.ChildNodes = new List<PageViewModel>();
        }

        /// <summary>
        /// Gets or sets the node key.
        /// </summary>
        /// <value>
        /// The the key.
        /// </value>
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the node title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the node URL.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1056:URI-like properties should not be strings", Justification = "DTO")]
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the link target.
        /// </summary>
        /// <value>
        /// The link target.
        /// </value>
        public string LinkTarget { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this node represents currently opened page.
        /// </summary>
        /// <value>
        ///   <c>true</c> if page node is currently opened; otherwise, <c>false</c>.
        /// </value>
        public bool IsCurrentlyOpened { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the currently opened page is a descendent of this node.
        /// </summary>
        /// <value>
        ///   <c>true</c> if currently opened page is descendent of this node; otherwise, <c>false</c>.
        /// </value>
        public bool HasChildOpen { get; set; }

        /// <summary>
        /// Gets or sets the child nodes.
        /// </summary>
        /// <value>
        /// The child nodes.
        /// </value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "DTO")]
        public IList<PageViewModel> ChildNodes { get; set; }

        /// <summary>
        /// Gets or sets the original site map node.
        /// </summary>
        /// <value>
        /// The original site map node.
        /// </value>
        [DataMember]
        public SiteMapPage PageSiteMapNode { get; set; }
    }
}
