using Progress.Sitefinity.AspNetCore.Widgets.Models.ContentList;

namespace Progress.Sitefinity.AspNetCore.Widgets.Preparations
{
    internal class ContentListPreparation : ContentListBasePreparation
    {
        public ContentListPreparation(IContentListModel contentListModel)
            : base(contentListModel)
        {
        }

        protected override string ContentListType => "SitefinityContentList";
    }
}
