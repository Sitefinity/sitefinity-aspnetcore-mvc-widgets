using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using Progress.Sitefinity.Renderer.Designers.Attributes;

namespace Progress.Sitefinity.AspNetCore.Widgets.Attributes
{
    /// <summary>
    /// The ExternalDataChoiceAttribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DataContract(Name = "Choice")]
    internal class ExternalDataChoiceAttribute : ChoiceAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExternalDataChoiceAttribute"/> class.
        /// </summary>
        public ExternalDataChoiceAttribute(string choicedProviderName)
            : base(null)
        {
            this.ChoicesProviderName = choicedProviderName;
        }

        public string ChoicesProviderName { get; }
    }
}
