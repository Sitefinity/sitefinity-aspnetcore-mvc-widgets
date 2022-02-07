using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Progress.Sitefinity.AspNetCore.FormWidgets.Entities.FileField;
using Progress.Sitefinity.AspNetCore.FormWidgets.Models.FileField;
using Progress.Sitefinity.AspNetCore.FormWidgets.ViewComponents;
using Progress.Sitefinity.AspNetCore.FormWidgets.ViewModels.FileField;

namespace Progress.Sitefinity.AspNetCore.FormWidgets.Models.TextField
{
    /// <summary>
    /// The model object that holds the logic for the <see cref="TextFieldViewComponent" /> widget.
    /// </summary>
    public class FileFieldModel : IFileFieldModel
    {
        private static readonly Dictionary<string, string[]> PredefinedAcceptValues = new Dictionary<string, string[]>()
        {
            { "Audio", new string[] { ".mp3", ".ogg", ".wav", ".wma" } },
            { "Video", new string[] { ".avi", ".mpg", ".mpeg", ".mov", ".mp4", ".wmv" } },
            { "Image", new string[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp" } },
            { "Document", new string[] { ".pdf", ".doc", ".docx", ".ppt", ".pptx", ".pps", ".ppsx", ".xls", ".xlsx" } },
        };

        /// <inheritdoc/>
        public Task<FileFieldViewModel> InitializeViewModel(FileFieldEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var allowedFileTypes = GetAcceptedFileTypes(entity);
            var viewModel = new FileFieldViewModel()
            {
                CssClass = entity.CssClass,
                FieldName = entity.SfFieldName,
                Label = entity.Label,
                RequiredViolationMessage = BuildValidationMessage(entity.Label, entity.RequiredErrorMessage, "{0} field is required"),
                FileSizeViolationMessage = BuildValidationMessage(entity.Label, entity.FileSizeViolationMessage, "The size of the selected file is too large"),
                FileTypeViolationMessage = BuildValidationMessage(entity.Label, entity.FileTypeViolationMessage, "File type is not allowed to upload"),
                InstructionalText = entity.InstructionalText,
                AllowMultipleFiles = entity.AllowMultipleFiles,
                Required = entity.Required,
                MinFileSizeInMb = entity.Range?.Min,
                MaxFileSizeInMb = entity.Range?.Max,
                AllowedFileTypes = allowedFileTypes,
                ViolationRestrictionsJson = JObject.FromObject(new
                {
                    maxSize = entity.Range?.Max,
                    minSize = entity.Range?.Min,
                    required = entity.Required,
                    allowMultiple = entity.AllowMultipleFiles,
                    allowedFileTypes = allowedFileTypes,
                }).ToString(),
            };

            if (entity.Required)
                viewModel.ValidationAttributes = "required";

            return Task.FromResult(viewModel);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1308:Normalize strings to uppercase", Justification = "can be lowercase as well")]
        private static string[] GetAcceptedFileTypes(FileFieldEntity entity)
        {
            var parsedArray = new HashSet<string>();
            var fileTypes = entity.FileTypes;
            if (fileTypes == null || string.IsNullOrEmpty(fileTypes.Type))
                return null;

            var types = fileTypes.Type.Split(',').Select(x => x.Trim());
            foreach (var type in types)
            {
                if (PredefinedAcceptValues.TryGetValue(type, out string[] values))
                {
                    foreach (var value in values)
                    {
                        parsedArray.Add(value);
                    }
                }

                if (type == "Other")
                {
                    var fileTypesSplit = fileTypes.Other
                        .Split(',')
                        .Select(t => t.Trim().ToLowerInvariant())
                        .Select(t => t.StartsWith(".", StringComparison.OrdinalIgnoreCase) ? t : "." + t)
                        .ToArray();

                    foreach (var value in fileTypesSplit)
                    {
                        parsedArray.Add(value);
                    }
                }
            }

            return parsedArray.ToArray();
        }

        private static string BuildValidationMessage(string textFieldLabel, string actualMessage, string defaultMessage)
        {
            actualMessage = string.IsNullOrEmpty(actualMessage) ? defaultMessage : actualMessage;
            string result = string.Format(CultureInfo.InvariantCulture, actualMessage, textFieldLabel);

            return result;
        }
    }
}
