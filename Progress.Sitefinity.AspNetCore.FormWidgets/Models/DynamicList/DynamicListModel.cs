using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Progress.Sitefinity.AspNetCore.FormWidgets.Entities.Dropdown;
using Progress.Sitefinity.AspNetCore.FormWidgets.Models.Common;
using Progress.Sitefinity.AspNetCore.FormWidgets.ViewComponents;
using Progress.Sitefinity.AspNetCore.FormWidgets.ViewComponents.Common;
using Progress.Sitefinity.AspNetCore.FormWidgets.ViewModels.DynamicList;
using Progress.Sitefinity.AspNetCore.RestSdk;
using Progress.Sitefinity.Renderer.Contracts.Forms;
using Progress.Sitefinity.RestSdk;
using Progress.Sitefinity.RestSdk.Client;
using Progress.Sitefinity.RestSdk.Dto;
using Progress.Sitefinity.RestSdk.OData;

namespace Progress.Sitefinity.AspNetCore.FormWidgets.Models.DynamicList
{
    /// <summary>
    /// The model object that holds the logic for the <see cref="DropdownViewComponent" /> widget.
    /// </summary>
    public class DynamicListModel : IDynamicListModel
    {
        private FormWidgetsStyleGenerator formWidgetsStyleGenerator;
        private IRestClient restClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicListModel"/> class.
        /// </summary>
        /// <param name="formWidgetsStyleGenerator">The style generator for forms widgets.</param>
        /// <param name="restClient">The rest client.</param>
        public DynamicListModel(FormWidgetsStyleGenerator formWidgetsStyleGenerator, IRestClient restClient)
        {
            this.formWidgetsStyleGenerator = formWidgetsStyleGenerator;
            this.restClient = restClient;
        }

        /// <inheritdoc/>
        public virtual async Task<DynamicListViewModel> InitializeViewModel(DynamicListEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var viewModel = new DynamicListViewModel();
            viewModel.Label = entity.Label;
            viewModel.InstructionalText = entity.InstructionalText;
            viewModel.Choices = await this.GetChoiceItems(entity);
            viewModel.Required = entity.Required;
            viewModel.FieldName = entity.SfFieldName;
            viewModel.CssClass = entity.CssClass;
            viewModel.ViolationRestrictionsMessages = JObject.FromObject(new
            {
                required = BuildValidationMessage(entity.Label, entity.RequiredErrorMessage, "{0} field is required"),
            }).ToString();

            viewModel.CssClass = $"{entity.CssClass} {this.formWidgetsStyleGenerator.GetFieldSizeCss(entity.FieldSize)}";
            viewModel.ColumnsNumber = entity.ColumnsNumber;
            return viewModel;
        }

        private static IList<ChoiceOption> GetChoices(IEnumerable<SdkItem> items, string defaultFieldName, DynamicListEntity entity)
        {
            if (items == null)
                return new List<ChoiceOption>();

            var returnVal = new List<ChoiceOption>();
            var taxa = items as IList<TaxonDto>;
            if (entity.ClassificationSettings.SelectionMode == TaxonSelectionMode.All && taxa != null)
            {
                GetAllTaxa(taxa, defaultFieldName, entity, ref returnVal);
            }
            else
            {
                returnVal = items.Select(x =>
                {
                    return GetOption(defaultFieldName, entity, x);
                }).ToList();
            }

            if (entity.SfViewName == "Dropdown" && returnVal.Count > 0)
                returnVal.Insert(0, new ChoiceOption() { Name = "Select", Selected = true });

            return returnVal;
        }

        private static ChoiceOption GetOption(string defaultFieldName, DynamicListEntity entity, SdkItem item)
        {
            var option = new ChoiceOption()
            {
                Name = item.GetValue<string>(defaultFieldName),
            };

            if (!string.IsNullOrEmpty(entity.ValueFieldName) && item.TryGetValue(entity.ValueFieldName, out string value))
            {
                option.Value = value;
            }
            else
            {
                option.Value = option.Name;
            }

            return option;
        }

        private static void GetAllTaxa(IList<TaxonDto> taxa, string defaultFieldName, DynamicListEntity entity, ref List<ChoiceOption> taxaChoices)
        {
            foreach (var item in taxa)
            {
                taxaChoices.Add(GetOption(defaultFieldName, entity, item));
                GetAllTaxa(item.SubTaxa, defaultFieldName, entity, ref taxaChoices);
            }
        }

        private static IList<OrderBy> GetOrderByExpressionForContent(DynamicListEntity entity)
        {
            var result = new List<OrderBy>();

            if (entity.OrderByContent != "Manually")
            {
                var sortExpression = entity.OrderByContent == "Custom" ? entity.SortExpression : entity.OrderByContent;

                if (!string.IsNullOrEmpty(sortExpression))
                {
                    var sortExpressions = sortExpression.Split(",", StringSplitOptions.RemoveEmptyEntries);
                    foreach (var expression in sortExpressions)
                    {
                        var sortExpressionParts = expression.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                        if (sortExpressionParts.Length != 2)
                            continue;

                        var sortOrder = sortExpressionParts[1].ToUpperInvariant() == "ASC" ? OrderType.Ascending : OrderType.Descending;
                        var orderBy = new OrderBy() { Name = sortExpressionParts[0], Type = sortOrder };
                        result.Add(orderBy);
                    }
                }
            }

            return result;
        }

        private static List<TaxonDto> MapTaxonProperties(TaxonDto taxon)
        {
            var children = new List<TaxonDto>();

            foreach (var child in taxon.SubTaxa)
            {
                child.SubTaxa = MapTaxonProperties(child);
                children.Add(child);
            }

            return children;
        }

        private static string BuildValidationMessage(string textFieldLabel, string actualMessage, string defaultMessage)
        {
            actualMessage = string.IsNullOrEmpty(actualMessage) ? defaultMessage : actualMessage;
            string result = string.Format(CultureInfo.InvariantCulture, actualMessage, textFieldLabel);

            return result;
        }

        private async Task<IList<ChoiceOption>> GetChoiceItems(DynamicListEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            IEnumerable<SdkItem> items = null;
            string defaultFieldName = null;
            if (entity.ListType == Selection.Classification)
            {
                items = await this.GetClassifications(entity);
                defaultFieldName = nameof(TaxonDto.Title);
            }
            else if (entity.ListType == Selection.Content)
            {
                var result = await this.GetContent(entity);
                items = result.Item1;
                defaultFieldName = result.Item2;
            }

            return GetChoices(items, defaultFieldName, entity);
        }

        private async Task<Tuple<IEnumerable<SdkItem>, string>> GetContent(DynamicListEntity entity)
        {
            if (entity.SelectedContent != null &&
                entity.SelectedContent.Content != null &&
                entity.SelectedContent.Content.Length > 0 &&
                entity.SelectedContent.Content[0].Type != null)
            {
                var itemType = entity.SelectedContent.Content[0].Type;
                var defaultFieldName = (this.restClient as RestClient).ServiceMetadata.GetDefaultFieldName(itemType);
                var getAllArgs = new GetAllArgs
                {
                    OrderBy = GetOrderByExpressionForContent(entity)
                };

                var items = (await this.restClient.GetItems<SdkItem>(entity.SelectedContent, getAllArgs)).Items;

                return new Tuple<IEnumerable<SdkItem>, string>(items, defaultFieldName);
            }

            return new Tuple<IEnumerable<SdkItem>, string>(Enumerable.Empty<SdkItem>(), null);
        }

        private async Task<IEnumerable<TaxonDto>> GetClassifications(DynamicListEntity entity)
        {
            var settings = entity.ClassificationSettings;
            if (string.IsNullOrEmpty(settings.SelectedTaxonomyId))
            {
                return Enumerable.Empty<TaxonDto>();
            }

            string orderBy = entity.OrderBy;

            if (orderBy == "Custom")
                orderBy = entity.SortExpression;
            else if (orderBy == "Manually")
                orderBy = "Ordinal";

            Dictionary<string, string> additionalParams = new Dictionary<string, string>
            {
                { "showEmpty", bool.TrueString },
                { "$orderby", orderBy },
                { "@param", $"[{string.Join(',', settings.SelectedTaxaIds.Select(x => $"'{x}'"))}]" },
            };

            var taxonomy = await this.restClient.GetItem<TaxonomyDto>(x => x.Id == settings.SelectedTaxonomyId);
            if (taxonomy == null)
                throw new ArgumentException($"The selected taxonomy with id {taxonomy.Id} does not exist");

            var taxa = await (this.restClient as IODataRestClient).ExecuteBoundFunction<ODataWrapper<IList<TaxonDto>>>(new BoundFunctionArgs
            {
                Type = taxonomy.TaxaUrl,
                AdditionalQueryParams = additionalParams,
                Name = $"Default.GetTaxons(taxonomyId={settings.SelectedTaxonomyId},selectedTaxaIds=@param,selectionMode='{settings.SelectionMode}',contentType='{settings.ByContentType}')",
            });

            var roots = new List<TaxonDto>();

            foreach (var taxon in taxa.Value)
            {
                taxon.SubTaxa = MapTaxonProperties(taxon);
                roots.Add(taxon);
            }

            return roots;
        }
    }
}
