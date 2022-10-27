using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Progress.Sitefinity.AspNetCore.Configuration;
using Progress.Sitefinity.AspNetCore.RestSdk;
using Progress.Sitefinity.AspNetCore.Web;
using Progress.Sitefinity.AspNetCore.Widgets.ViewComponents.Common;
using Progress.Sitefinity.RestSdk;
using Progress.Sitefinity.RestSdk.Dto;
using Progress.Sitefinity.RestSdk.OData;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Classification
{
    /// <summary>
    /// The model for the Classification widget.
    /// </summary>
    public class ClassificationModel : IClassificationModel
    {
        private readonly IStyleClassesProvider styles;
        private IODataRestClient restService;
        private ISitefinityConfig sfConfig;
        private IRequestContext requestContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClassificationModel"/> class.
        /// </summary>
        /// <param name="restService">The client for Sitefinity web services.</param>
        /// <param name="requestContext">The current request context.</param>
        /// <param name="styles">The styles provider.</param>
        /// <param name="sfConfig">The Sitefinity config.</param>
        public ClassificationModel(
            IODataRestClient restService,
            IRequestContext requestContext,
            IStyleClassesProvider styles,
            ISitefinityConfig sfConfig)
        {
            this.restService = restService;
            this.sfConfig = sfConfig;
            this.requestContext = requestContext;
            this.styles = styles;
        }

        /// <inheritdoc/>
        public virtual async Task<ClassificationViewModel> InitializeViewModel(ClassificationEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var currentSite = this.requestContext.Site;
            var viewModel = new ClassificationViewModel();
            viewModel.ShowItemCount = entity.ShowItemCount;
            viewModel.Attributes = entity.Attributes;
            viewModel.ViewName = entity.SfViewName;
            viewModel.CssClass = entity.CssClass;

            var margins = this.styles.GetMarginsClasses(entity);
            viewModel.CssClass = (entity.CssClass + " " + margins).Trim();

            viewModel.Taxons = await this.GetClassifications(entity);

            return viewModel;
        }

        private async Task<IEnumerable<TaxonDto>> GetClassifications(ClassificationEntity entity)
        {
            if (string.IsNullOrEmpty(entity?.ClassificationSettings?.SelectedTaxonomyId))
            {
                return Enumerable.Empty<TaxonDto>();
            }

            bool showEmpty = entity.ShowEmpty;
            bool showCount = entity.ShowItemCount;
            string orderBy = entity.OrderBy;

            if (orderBy == "Custom")
            {
                orderBy = entity.SortExpression;
            }
            else if (orderBy == "Manually")
            {
                orderBy = "Ordinal";
            }

            var settings = entity.ClassificationSettings;
            Dictionary<string, string> additionalParams = new Dictionary<string, string>
            {
                { "showEmpty", showEmpty.ToString() },
                { "$orderby", orderBy },
                { "@param", $"[{string.Join(',', settings.SelectedTaxaIds.Select(x => $"'{x}'"))}]" },
            };

            var taxa = await this.restService.ExecuteBoundFunction<ODataWrapper<IList<TaxonDto>>>(new BoundFunctionArgs
            {
                Type = entity.ClassificationSettings.SelectedTaxonomyUrl,
                AdditionalQueryParams = additionalParams,
                Name = $"Default.GetTaxons(taxonomyId={settings.SelectedTaxonomyId},selectedTaxaIds=@param,selectionMode='{settings.SelectionMode}',contentType='{settings.ByContentType}')",
            });

            var roots = new List<TaxonDto>();

            foreach (var taxon in taxa.Value)
            {
                taxon.SubTaxa = this.MapTaxonProperties(taxon, entity.ClassificationSettings.SelectedTaxonomyName);
                taxon.UrlName = this.GetTaxaUrl(entity.ClassificationSettings.SelectedTaxonomyName, taxon.UrlName);
                roots.Add(taxon);
            }

            return roots;
        }

        private List<TaxonDto> MapTaxonProperties(TaxonDto taxon, string taxonomyName)
        {
            var children = new List<TaxonDto>();

            foreach (var child in taxon.SubTaxa)
            {
                child.SubTaxa = this.MapTaxonProperties(child, taxonomyName);
                child.UrlName = this.GetTaxaUrl(taxonomyName, child.UrlName);
                children.Add(child);
            }

            return children;
        }

        private string GetTaxaUrl(string taxonomyName, string taxonUrl)
        {
            string pageUrl = this.requestContext.PageNode?.ViewUrl;
            if (taxonUrl.StartsWith('/'))
            {
                taxonUrl = taxonUrl.Substring(1);
            }

            return $"{pageUrl}/-in-{taxonomyName},{taxonUrl.Replace("/", ",", StringComparison.InvariantCulture)}";
        }
    }
}
