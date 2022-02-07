using Progress.Sitefinity.AspNetCore.Configuration;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Common;

namespace Progress.Sitefinity.AspNetCore.Widgets.ViewComponents.Common
{
    /// <summary>
    /// Provides html class generation for widgets.
    /// </summary>
    public interface IStyleClassesProvider
    {
        /// <summary>
        /// Gets the styling config.
        /// </summary>
        StylingConfig StylingConfig { get; }

        /// <summary>
        /// Gets the default margin.
        /// </summary>
        OffsetSize DefaultMargin { get; }

        /// <summary>
        /// Gets the default padding.
        /// </summary>
        OffsetSize DefaultPadding { get; }

        /// <summary>
        /// Generates margins classes for the html output.
        /// </summary>
        /// <typeparam name="T">The offset style type.</typeparam>
        /// <param name="entity">The widget entitiy for which we want to generate margins.</param>
        /// <returns>The html style string.</returns>
        string GetMarginsClasses<T>(IHasMargins<T> entity)
            where T : OffsetStyleBase;

        /// <summary>
        /// Generates margins classes for the html output.
        /// </summary>
        /// <param name="margins">The margins.</param>
        /// <returns>The html style string.</returns>
        string GetMarginsClasses(OffsetStyleBase margins);

        /// <summary>
        /// Generates paddings classes for the html output.
        /// </summary>
        /// <typeparam name="T">The offset style type.</typeparam>
        /// <param name="entity">The widget entitiy for which we want to generate paddings.</param>
        /// <returns>The html style string.</returns>
        string GetPaddingsClasses<T>(IHasPaddings<T> entity)
            where T : OffsetStyleBase;

        /// <summary>
        /// Generates padding classes for the html output.
        /// </summary>
        /// <param name="paddings">The paddings.</param>
        /// <returns>The html style string.</returns>
        string GetPaddingsClasses(OffsetStyleBase paddings);

        /// <summary>
        /// Generates position for the html output.
        /// </summary>
        /// <param name="entity">The widget entitiy for which we want to generate position.</param>
        /// <returns>The html style string.</returns>
        string GetAlignmenClasses(IHasPosition entity);

        /// <summary>
        /// Gets the default classes for the button.
        /// </summary>
        /// <param name="buttonType">The button type.</param>
        /// <returns>The CSS classes.</returns>
        public string GetConfiguredButtonClasses(string buttonType);
    }
}
