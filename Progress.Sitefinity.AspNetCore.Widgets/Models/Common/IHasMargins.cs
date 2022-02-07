namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Common
{
    /// <summary>
    /// Interface for entities that store css margins.
    /// </summary>
    /// <typeparam name="T">The offset style type.</typeparam>
    public interface IHasMargins<T>
        where T : OffsetStyleBase
    {
        /// <summary>
        /// Gets or sets the margins.
        /// </summary>
        T Margins { get; set; }
    }
}
