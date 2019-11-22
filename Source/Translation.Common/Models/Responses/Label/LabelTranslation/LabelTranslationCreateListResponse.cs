using Translation.Common.Models.Base;

namespace Translation.Common.Models.Responses.Label.LabelTranslation
{
    public class LabelTranslationCreateListResponse : TranslationBaseResponse
    {
        public int AddedTranslationCount { get; set; }
        public int CanNotAddedTranslationCount { get; set; }
        public int UpdatedTranslationCount { get; set; }
    }
}