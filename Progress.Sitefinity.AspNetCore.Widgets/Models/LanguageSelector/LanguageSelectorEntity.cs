using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Progress.Sitefinity.AspNetCore.Models;
using Progress.Sitefinity.AspNetCore.Models.Common;
using Progress.Sitefinity.AspNetCore.ViewComponents.AttributeConfigurator.Attributes;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Common;
using Progress.Sitefinity.Renderer.Designers;
using Progress.Sitefinity.Renderer.Designers.Attributes;
using static Progress.Sitefinity.AspNetCore.Constants;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.LanguageSelector
{
    /// <summary>
    /// Entity class for the Language selector widget.
    /// </summary>
    public class LanguageSelectorEntity : IHasMargins<MarginStyle>
    {
        /// <summary>
        /// Gets or sets the missing translation action.
        /// </summary>
        [ContentSection("Language selector setup")]
        [DisplayName("For languages without translations...")]
        [DataType(KnownFieldTypes.RadioChoice)]
        [Description("Some pages may not be translated to all languages. This setting defines the language selector behavior when a translation is missing.")]
        public MissingTranslationAction LanguageSelectorLinkAction { get; set; }

        /// <summary>
        /// Gets or sets the missing translation action.
        /// </summary>
        [ContentSection("Language selector setup")]
        [DisplayName("Show language names...")]
        [DataType(KnownFieldTypes.RadioChoice)]
        public LanguageSelectorDisplayFormat LanguageSelectorDisplayFormat { get; set; }

        /// <summary>
        /// Gets or sets the view name.
        /// </summary>
        [ContentSection(ContentSectionTitles.DisplaySettings, 0)]
        [ViewSelector]
        [DisplayName("Language selector template")]
        public string SfViewName { get; set; }

        /// <summary>
        /// Gets or sets the margins.
        /// </summary>
        [ContentSection(ContentSectionTitles.DisplaySettings, 1)]
        [DisplayName("Margins")]
        [TableView("Language selector")]
        public MarginStyle Margins { get; set; }

        /// <summary>
        /// Gets or sets the CSS class of the wrapper.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [DisplayName("CSS class")]
        public string CssClass { get; set; }

        /// <summary>
        /// Gets or sets the attributes for navigation widget.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [ContentSection(ContentSectionTitles.Attributes)]
        [DisplayName("Attributes for...")]
        [DataType(customDataType: KnownFieldTypes.Attributes)]
        [LengthDependsOn(null, "", "", ExtraRecords = "[{\"Name\": \"LanguageSelector\", \"Title\": \"Language selector\"}]")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Must be able to set in the model.")]
        public IDictionary<string, IList<AttributeModel>> Attributes { get; set; }
    }
}
