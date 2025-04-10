using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Progress.Sitefinity.AspNetCore.Models;
using Progress.Sitefinity.AspNetCore.Models.Common;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Common;
using Progress.Sitefinity.Renderer.Designers;
using Progress.Sitefinity.Renderer.Designers.Attributes;
using Progress.Sitefinity.Renderer.Models;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Button
{
    /// <summary>
    /// The entity for the Button(CTA) widget. Contains all of the data persited in the database.
    /// </summary>
    [SectionsOrder(PrimaryAction, SecondaryAction, Constants.ContentSectionTitles.DisplaySettings)]
    public class ButtonEntity : IHasMargins<MarginStyle>, IHasPosition
    {
        /// <summary>
        /// Gets or sets the action label for the primary action.
        /// </summary>
        [DisplayName("Action label")]
        [ContentSection(PrimaryAction, 1)]
        [DescriptionExtended(InstructionalNotes = "Example: Learn more")]
        public string PrimaryActionLabel { get; set; }

        /// <summary>
        /// Gets or sets the action link for the primary action.
        /// </summary>
        [DisplayName("Action link")]
        [ContentSection(PrimaryAction, 2)]
        [DataType(customDataType: "linkSelector")]
        public LinkModel PrimaryActionLink { get; set; }

        /// <summary>
        /// Gets or sets the action label for the secondary action.
        /// </summary>
        [DisplayName("Action label")]
        [ContentSection(SecondaryAction, 1)]
        [DescriptionExtended(InstructionalNotes = "Example: Learn more")]
        public string SecondaryActionLabel { get; set; }

        /// <summary>
        /// Gets or sets the action link for the secondary action.
        /// </summary>
        [DisplayName("Action link")]
        [ContentSection(SecondaryAction, 2)]
        [DataType(customDataType: "linkSelector")]
        public LinkModel SecondaryActionLink { get; set; }

        /// <summary>
        /// Gets or sets the custom CSS for the columns and for the section.
        /// </summary>
        [ContentSection(Constants.ContentSectionTitles.DisplaySettings)]
        [LengthDependsOn(null, "", "", ExtraRecords = "[{\"Name\": \"Primary\", \"Title\": \"Primary\"}, {\"Name\": \"Secondary\", \"Title\": \"Secondary\"}]")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Must be able to set in property editor.")]
        public IDictionary<string, ButtonStyle> Style { get; set; }

        /// <inheritdoc/>
        [ContentSection(Constants.ContentSectionTitles.DisplaySettings)]
        [LengthDependsOn(null, "", "", ExtraRecords = "[{\"Name\": \"CTA\", \"Title\": \"CTA\"}]")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Must be able to set in property editor.")]
        public IDictionary<string, AlignmentWrapper> Position { get; set; }

        /// <inheritdoc/>
        [ContentSection(Constants.ContentSectionTitles.DisplaySettings, 1)]
        [DisplayName("Margins")]
        [TableView("CTA")]
        public MarginStyle Margins { get; set; }

        /// <summary>
        /// Gets or sets the CSS class of the button.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [DisplayName("CSS class")]
        public string CssClass { get; set; }

        /// <summary>
        /// Gets or sets the attributes for the button.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [ContentSection(Constants.ContentSectionTitles.Attributes)]
        [DisplayName("Attributes for...")]
        [DataType(customDataType: KnownFieldTypes.Attributes)]
        [LengthDependsOn(null, "", " ", ExtraRecords = "[{\"Name\": \"Wrapper\", \"Title\": \"Wrapper\"},{\"Name\": \"Primary\", \"Title\": \"Primary\"},{\"Name\": \"Secondary\", \"Title\": \"Secondary\"}]")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Must be able to set in property editor.")]
        public IDictionary<string, IList<AttributeModel>> Attributes { get; set; }

        private const string PrimaryAction = "Primary action";
        private const string SecondaryAction = "Secondary action";
    }
}
