using Progress.Sitefinity.AspNetCore.Widgets.Models.DocumentList;

namespace Progress.Sitefinity.AspNetCore.Widgets.Preparations
{
    internal class DocumentListPreparation : ContentListBasePreparation
    {
        public DocumentListPreparation(IDocumentListModel documentListModel)
            : base(documentListModel)
        {
        }

        protected override string ContentListType => "SitefinityDocumentList";
    }
}
