namespace Progress.Sitefinity.AspNetCore.Widgets.Models.ContentList
{
    /// <summary>
    /// The content view display mode.
    /// </summary>
    /// <remarks>
    /// Each option describes a different option for rendernig the content list.
    /// </remarks>
    public enum ContentViewDisplayMode
    {
        /// <summary>
        /// Automatically determines whether to render the items in list mode or detail mode.
        /// </summary>
        Automatic,

        /// <summary>
        /// Always displays items in list mode only(not handling additional parameters).
        /// </summary>
        Master,

        /// <summary>
        /// Always displays items in detail mode based on the selection of the first item from the manually selected items.
        /// </summary>
        Detail,
    }
}
