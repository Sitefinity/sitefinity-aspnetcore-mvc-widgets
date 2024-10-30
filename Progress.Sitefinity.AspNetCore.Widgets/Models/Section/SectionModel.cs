using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Progress.Sitefinity.AspNetCore.Models;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Common;
using Progress.Sitefinity.AspNetCore.Widgets.ViewComponents.Common;
using Progress.Sitefinity.RestSdk;
using Progress.Sitefinity.RestSdk.Dto;
using Progress.Sitefinity.RestSdk.OData;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Section
{
    /// <summary>
    /// The model for the section widget.
    /// </summary>
    public class SectionModel : ISectionModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SectionModel"/> class.
        /// </summary>
        /// <param name="styles">The style classes provider.</param>
        /// <param name="service">The rest service.</param>
        public SectionModel(IStyleClassesProvider styles, IRestClient service)
        {
            if (service == null)
                throw new ArgumentNullException(nameof(service));

            this.service = service;
            this.styles = styles;
        }

        /// <summary>
        /// Initializes the view model.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>The view model.</returns>
        public virtual async Task<SectionViewModel> InitializeViewModel(SectionEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var viewModel = new SectionViewModel();
            viewModel.TagName = entity.TagName;
            viewModel.ColumnsCount = entity.ColumnsCount;
            viewModel.ColumnProportions = entity.ColumnProportionsInfo;
            viewModel.ColumnNames = PopulateColumnNames(entity);
            viewModel.ColumnTitles = viewModel.ColumnNames.Select(name => ConstructColumnLabel(entity, name)).ToList();
            viewModel.SectionClasses = this.ConstructSectionClass(entity);
            viewModel.ColumnsClasses = this.ConstructColumnClasses(entity);
            viewModel.SectionStyle = entity.SectionBackground != null ? await this.ConstructBackgroundStyle(entity.SectionBackground) : null;
            viewModel.ColumnStyles = ConstructColumnStyles(entity);
            viewModel.ShowVideo = entity.SectionBackground != null && entity.SectionBackground.BackgroundType == Background.Video;
            viewModel.VideoUrl = viewModel.ShowVideo ? await this.GetVideoUrl(entity.SectionBackground) : null;
            viewModel.Attributes = entity.Attributes;
            viewModel.ColumnsAttributes = ConstructColumnsAttributes(entity);
            if (entity.Attributes != null && entity.Attributes.TryGetValue("Section", out IList<AttributeModel> value))
            {
                viewModel.SectionAttributes = value;
            }

            return viewModel;
        }

        private static string ConstructBackgroundBaseStyle(SimpleBackgroundStyle backgroundStyle)
        {
            switch (backgroundStyle.BackgroundType)
            {
                case BackgroundBase.None:
                    return string.Empty;
                case BackgroundBase.Color:
                    return "--sf-background-color: " + backgroundStyle.Color;
                default:
                    return string.Empty;
            }
        }

        private static string ConstructColumnLabel(SectionEntity entity, string columnName)
        {
            if (entity.Labels != null && entity.Labels.ContainsKey(columnName))
            {
                return entity.Labels[columnName].Label;
            }

            return null;
        }

        private static IList<string> PopulateColumnNames(SectionEntity entity)
        {
            var columnNames = new List<string>();
            for (var i = 0; i < entity.ColumnsCount; i++)
            {
                columnNames.Add(SectionModel.ColumnNamePrefix + (i + 1));
            }

            return columnNames;
        }

        /// <summary>
        /// Constructs columns attributes.
        /// </summary>
        /// <returns>The HTML attributes applied to the columns.</returns>
        private static IList<IList<AttributeModel>> ConstructColumnsAttributes(SectionEntity entity)
        {
            var columnsAttributes = new List<IList<AttributeModel>>();
            for (int i = 0; i < entity.ColumnsCount; i++)
            {
                string columnName = SectionModel.ColumnNamePrefix + (i + 1);
                var attributes = new List<AttributeModel>();
                if (entity.Attributes != null && entity.Attributes.ContainsKey(columnName))
                {
                    foreach (var attribute in entity.Attributes[columnName])
                    {
                        attributes.Add(new AttributeModel
                        {
                            Key = attribute.Key,
                            Value = attribute.Value,
                        });
                    }
                }

                columnsAttributes.Add(attributes);
            }

            return columnsAttributes;
        }

        /// <summary>
        /// Constructs column styles.
        /// </summary>
        /// <returns>The CSS classes applied to the columns.</returns>
        private static IList<string> ConstructColumnStyles(SectionEntity entity)
        {
            var columnStyles = new List<string>();
            for (int i = 0; i < entity.ColumnsCount; i++)
            {
                var key = SectionModel.ColumnNamePrefix + (i + 1);
                var columnStyle = entity.ColumnsBackground != null && entity.ColumnsBackground.ContainsKey(key) ? ConstructBackgroundBaseStyle(entity.ColumnsBackground[key]) : string.Empty;
                columnStyles.Add(columnStyle);
            }

            return columnStyles;
        }

        /// <summary>
        /// Constructs section CSS classes.
        /// </summary>
        /// <returns>The CSS classes applied to the section.</returns>
        private string ConstructSectionClass(SectionEntity entity)
        {
            StringBuilder sectionCss = new StringBuilder();

            var paddings = this.styles.GetPaddingsClasses(entity.SectionPadding);
            var imageBgClass = this.styles.StylingConfig.ImageBackgroundClass;
            var videoBgClass = this.styles.StylingConfig.VideoBackgroundClass;

            if (!string.IsNullOrEmpty(paddings))
            {
                sectionCss.Append(paddings + " ");
            }

            var margins = this.styles.GetMarginsClasses(entity.SectionMargin);
            if (!string.IsNullOrEmpty(margins))
            {
                sectionCss.Append(margins + " ");
            }

            if (entity.SectionBackground != null)
            {
                if (entity.SectionBackground.BackgroundType == Background.Image)
                {
                    sectionCss.Append(imageBgClass + " ");
                }

                if (entity.SectionBackground.BackgroundType == Background.Video)
                {
                    sectionCss.Append(videoBgClass + " ");
                }
            }

            var customCss = entity.CustomCssClass != null && entity.CustomCssClass.ContainsKey(SectionModel.SectionName) ? entity.CustomCssClass[SectionModel.SectionName] : null;
            if (customCss != null && !string.IsNullOrEmpty(customCss.Class))
            {
                sectionCss.Append(customCss.Class);
            }

            return sectionCss.ToString().TrimEnd();
        }

        /// <summary>
        /// Constructs column classes.
        /// </summary>
        /// <returns>The CSS classes applied to the columns.</returns>
        private IList<string> ConstructColumnClasses(SectionEntity entity)
        {
            var columnStyles = new List<string>();
            for (int i = 0; i < entity.ColumnsCount; i++)
            {
                columnStyles.Add(this.ConstructColumnClass(entity, SectionModel.ColumnNamePrefix + (i + 1)));
            }

            return columnStyles;
        }

        /// <summary>
        /// Constructs column styles.
        /// </summary>
        /// <param name="entity">The section entity.</param>
        /// <param name="columnName">The name of the column.</param>
        /// <returns>The CSS classes applied to a column.</returns>
        private string ConstructColumnClass(SectionEntity entity, string columnName)
        {
            StringBuilder columnCss = new StringBuilder();
            PaddingStyle columnPaddings = null;
            entity.ColumnsPadding?.TryGetValue(columnName, out columnPaddings);
            var paddings = this.styles.GetPaddingsClasses(columnPaddings);

            if (!string.IsNullOrEmpty(paddings))
            {
                columnCss.Append(paddings + " ");
            }

            var customCss = entity.CustomCssClass != null && entity.CustomCssClass.ContainsKey(columnName) ? entity.CustomCssClass[columnName] : null;
            if (customCss != null && !string.IsNullOrEmpty(customCss.Class))
            {
                columnCss.Append(customCss.Class);
            }

            return columnCss.ToString().TrimEnd();
        }

        /// <summary>
        /// Gets the background style.
        /// </summary>
        /// <returns>The background style.</returns>
        private async Task<string> ConstructBackgroundStyle(BackgroundStyle backgroundStyle)
        {
            switch (backgroundStyle.BackgroundType)
            {
                case Background.None:
                    return string.Empty;
                case Background.Color:
                    return "--sf-background-color: " + backgroundStyle.Color;
                case Background.Image:
                    if (backgroundStyle.ImageItem == null && backgroundStyle.ImageItem.Id != null)
                        return string.Empty;

                    var image = await this.GetItemWithFallback<ImageDto>(backgroundStyle.ImageItem.Id, backgroundStyle.ImageItem.Provider);

                    var imageUrl = image != null ? image.Url : null;
                    var imagePosition = (string)null;

                    switch (backgroundStyle.ImagePosition)
                    {
                        case Position.Fill:
                            imagePosition = "--sf-background-size: 100% auto;";
                            break;
                        case Position.Center:
                            imagePosition = "--sf-background-position: center";
                            break;
                        default:
                            imagePosition = "--sf-background-size: cover;";
                            break;
                    }

                    return $"--sf-background-image: url({imageUrl});{imagePosition}";
                default:
                    return string.Empty;
            }
        }

        /// <summary>
        /// Gets the URL of the video.
        /// </summary>
        /// <param name="backgroundStyle">The background style.</param>
        /// <returns>The media URL.</returns>
        private async Task<string> GetVideoUrl(BackgroundStyle backgroundStyle)
        {
            if (backgroundStyle.BackgroundType == Background.Video && backgroundStyle.VideoItem != null && backgroundStyle.VideoItem.Id != null)
            {
                var video = await this.GetItemWithFallback<VideoDto>(backgroundStyle.VideoItem.Id, backgroundStyle.VideoItem.Provider);
                return video.Url;
            }

            return null;
        }

        private Task<TDO> GetItemWithFallback<TDO>(string id, string provider)
            where TDO : SdkItem
        {
            var args = new BoundFunctionArgs()
            {
                Id = id,
                Name = "Default.GetItemWithFallback()",
                AdditionalQueryParams = new Dictionary<string, string>() { { "sf_fallback_prop_names", "*" }, { "$select", "*" } },
                Provider = provider,
            };

            return (this.service as IODataRestClient).ExecuteBoundFunction<TDO>(args);
        }

        private const string ColumnNamePrefix = "Column";
        private const string SectionName = "Section";
        private IRestClient service;
        private IStyleClassesProvider styles;
    }
}
