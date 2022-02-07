namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Common
{
    /// <summary>
    /// Interface for entities that store css paddings.
    /// </summary>
    /// <typeparam name="T">The offset style type.</typeparam>
    public interface IHasPaddings<T>
        where T : OffsetStyleBase
    {
        /// <summary>
        /// Gets or sets the paddings.
        /// </summary>
        T Paddings { get; set; }
    }
}
