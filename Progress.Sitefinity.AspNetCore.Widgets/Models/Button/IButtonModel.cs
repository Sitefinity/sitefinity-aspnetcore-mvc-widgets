using Progress.Sitefinity.AspNetCore.ViewComponents;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Button
{
    /// <summary>
    /// Defines model for the Button (CTA) widget.
    /// </summary>
    public interface IButtonModel
    {
        /// <summary>
        /// Initializes the view model.
        /// </summary>
        /// <param name="entity">The button entity.</param>
        /// <returns>The view model of the widget.</returns>
        public ButtonViewModel InitializeViewModel(ButtonEntity entity);
    }
}
