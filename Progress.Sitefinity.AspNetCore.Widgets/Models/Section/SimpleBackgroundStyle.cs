﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Progress.Sitefinity.AspNetCore.ViewComponents.AttributeConfigurator.Attributes;
using Progress.Sitefinity.Renderer.Designers;
using Progress.Sitefinity.Renderer.Designers.Attributes;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Section
{
    /// <summary>
    /// Provides basic style information for the background.
    /// </summary>
    public class SimpleBackgroundStyle
    {
        /// <summary>
        /// Gets or sets the background type.
        /// </summary>
        [DisplayName("Type")]
        [DataType(customDataType: KnownFieldTypes.ChipChoice)]
        public BackgroundBase BackgroundType { get; set; }

        /// <summary>
        /// Gets or sets the color style.
        /// </summary>
        [DisplayName("Value")]
        [ColorPalette("Default")]
        [ConditionalVisibility("{\"conditions\":[{\"fieldName\":\"BackgroundType\",\"operator\":\"Equals\",\"value\":\"Color\"}]}")]
        public string Color { get; set; }
    }

    /// <summary>
    /// Enum with basic backgrounds.
    /// </summary>
    public enum BackgroundBase
    {
        /// <summary>
        /// No backgorund.
        /// </summary>
        None,

        /// <summary>
        /// Color.
        /// </summary>
        Color,
    }
}
