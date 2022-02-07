using System;
using System.Collections.Generic;
using System.Text;
using Progress.Sitefinity.AspNetCore.Configuration;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Common
{
    /// <summary>
    /// Base class for offset styles.
    /// </summary>
    public abstract class OffsetStyleBase
    {
        /// <summary>
        /// Gets the default classes for the columns.
        /// </summary>
        /// <param name="stylingConfig">The styling config.</param>
        /// <param name="offsetType">The offset type (padding, margin).</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The CSS classes.</returns>
        internal static string GetDefaultClasses(StylingConfig stylingConfig, string offsetType, OffsetSize defaultValue = OffsetSize.None)
        {
            var classes = new StringBuilder(0);
            var topClass = GetPredefinedClasses(stylingConfig, defaultValue, "Top", offsetType);
            if (!string.IsNullOrEmpty(topClass))
            {
                classes.Append(topClass + " ");
            }

            var rightClass = GetPredefinedClasses(stylingConfig, defaultValue, "Right", offsetType);
            if (!string.IsNullOrEmpty(rightClass))
            {
                classes.Append(rightClass + " ");
            }

            var bottomClass = GetPredefinedClasses(stylingConfig, defaultValue, "Bottom", offsetType);
            if (!string.IsNullOrEmpty(bottomClass))
            {
                classes.Append(bottomClass + " ");
            }

            var leftClass = GetPredefinedClasses(stylingConfig, defaultValue, "Left", offsetType);
            if (!string.IsNullOrEmpty(leftClass))
            {
                classes.Append(leftClass + " ");
            }

            return classes.ToString().TrimEnd();
        }

        /// <summary>
        /// Gets the classes for the columns.
        /// </summary>
        /// <param name="stylingConfig">The styling config.</param>
        /// <param name="offsetType">The offset type (padding, margin).</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The CSS classes.</returns>
        protected internal abstract string GetClasses(StylingConfig stylingConfig, string offsetType, OffsetSize defaultValue = OffsetSize.None);

        /// <summary>
        /// Adds classes for direction.
        /// </summary>
        /// <param name="stylingConfig">The styling config.</param>
        /// <param name="offsetType">The offsetTyple ("margin", "padding").</param>
        /// <param name="direction">the direction ("top", "left"..).</param>
        /// <param name="currentSize">The cirrent offset size.</param>
        /// <param name="defaultValue">The default offset size.</param>
        /// <param name="classes">The string builder.</param>
        protected static void AddClassesForDirection(StylingConfig stylingConfig, string offsetType, string direction, OffsetSize? currentSize, OffsetSize defaultValue, StringBuilder classes)
        {
            if (classes == null)
                throw new ArgumentNullException(nameof(classes));

            if (stylingConfig == null)
                throw new ArgumentNullException(nameof(stylingConfig));

            var size = currentSize != null ? currentSize : defaultValue;

            var className = GetPredefinedClasses(stylingConfig, size, direction, offsetType);
            if (!string.IsNullOrEmpty(className))
            {
                classes.Append(className + " ");
            }
        }

        /// <summary>
        /// Gets a predifined class name.
        /// </summary>
        /// <param name="stylingConfig">The styling config.</param>
        /// <param name="title">The user friendly title for the CSS class.</param>
        /// <param name="direction">The offset direction (left, top, bottom, right).</param>
        /// <param name="offsetType">The offset type(margin, padding).</param>
        /// <returns>The CSS class name.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1308:Normalize strings to uppercase", Justification = "Need this for the css style name.")]
        private static string GetPredefinedClasses(StylingConfig stylingConfig, OffsetSize? title, string direction, string offsetType)
        {
            if (title == null)
                return string.Empty;

            var key = $"{offsetType}{direction}{title.ToString().ToUpperInvariant()}";

            if (stylingConfig.OffsetClasses.ContainsKey(key))
                return stylingConfig.OffsetClasses[key];
            else if (defaultOffsetClasses.ContainsKey(key))
                return defaultOffsetClasses[key];

            return string.Empty;
        }

        private static IDictionary<string, string> defaultOffsetClasses = new Dictionary<string, string>()
        {
            { "PaddingTopS", "pt-3" },
            { "PaddingTopM", "pt-4" },
            { "PaddingTopL", "pt-5" },
            { "PaddingLeftS", "ps-3" },
            { "PaddingLeftM", "ps-4" },
            { "PaddingLeftL", "ps-5" },
            { "PaddingBottomS", "pb-3" },
            { "PaddingBottomM", "pb-4" },
            { "PaddingBottomL", "pb-5" },
            { "PaddingRightS", "pe-3" },
            { "PaddingRightM", "pe-4" },
            { "PaddingRightL", "pe-5" },
            { "MarginTopS", "mt-3" },
            { "MarginTopM", "mt-4" },
            { "MarginTopL", "mt-5" },
            { "MarginLeftS", "ms-3" },
            { "MarginLeftM", "ms-4" },
            { "MarginLeftL", "ms-5" },
            { "MarginBottomS", "mb-3" },
            { "MarginBottomM", "mb-4" },
            { "MarginBottomL", "mb-5" },
            { "MarginRightS", "me-3" },
            { "MarginRightM", "me-4" },
            { "MarginRightL", "me-5" },
        };
    }
}
