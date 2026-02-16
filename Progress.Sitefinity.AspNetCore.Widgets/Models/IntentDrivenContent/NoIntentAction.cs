namespace Progress.Sitefinity.AspNetCore.Widgets.Models.IntentDrivenContent
{
    /// <summary>
    /// Specifies the action to take when no intent is detected in the content.
    /// </summary>
    public enum NoIntentAction
    {
        /// <summary>
        /// No action will be taken.
        /// </summary>
        None,

        /// <summary>
        /// Content will be generated using a predefined query.
        /// </summary>
        GenerateWithPredefinedQuery,
    }
}
