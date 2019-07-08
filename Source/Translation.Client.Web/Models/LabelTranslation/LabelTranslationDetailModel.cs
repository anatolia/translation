using System;

using Translation.Client.Web.Models.Base;

namespace Translation.Client.Web.Models.LabelTranslation
{
    public sealed class LabelTranslationDetailModel : BaseModel
    {
        public Guid OrganizationUid { get; set; }
        public Guid LabelTranslationUid { get; set; }

        public string LabelKey { get; set; }
        public string LanguageName { get; set; }
        public string LanguageIconUrl { get; set; }

        public string Translation { get; set; }

        public LabelTranslationDetailModel()
        {
            Title = "label_translation_detail_title";
        }

        public override void SetInputModelValues()
        {
        }
    }
}
