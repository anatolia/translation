using System;

using Translation.Client.Web.Models.Base;

namespace Translation.Client.Web.Models.LabelTranslation
{
    public class LabelTranslationRevisionReadListModel : BaseModel
    {
        public string LabelTranslationName { get; set; }
        public Guid LabelTranslationUid { get; set; }

        public LabelTranslationRevisionReadListModel()
        {
            Title = "label_translation_revision_list_title";
        }
    }
}