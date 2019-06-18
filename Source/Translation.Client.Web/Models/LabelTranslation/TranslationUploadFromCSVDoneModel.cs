using System;

using Translation.Client.Web.Models.Base;

namespace Translation.Client.Web.Models.LabelTranslation
{
    public class TranslationUploadFromCSVDoneModel : BaseModel
    {
        public Guid LabelUid { get; set; }
        public string LabelKey { get; set; }

        public int AddedTranslationCount { get; set; }
        public int CanNotAddedTranslationCount { get; set; }

        public TranslationUploadFromCSVDoneModel()
        {
            Title = "translation_upload_done_title";
        }
    }
}