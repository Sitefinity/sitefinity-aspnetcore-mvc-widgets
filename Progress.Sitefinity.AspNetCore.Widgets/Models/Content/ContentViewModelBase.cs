namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Content
{
    /// <summary>
    /// Base content view model.
    /// </summary>
    /// <typeparam name="T">The content item type.</typeparam>
    public class ContentViewModelBase<T>
    {
        /// <summary>
        /// Gets or sets the styles.
        /// </summary>
        public string CssClass { get; set; }

        /// <summary>
        /// Gets or sets the item.
        /// </summary>
        public T Item { get; set; }
    }
}
