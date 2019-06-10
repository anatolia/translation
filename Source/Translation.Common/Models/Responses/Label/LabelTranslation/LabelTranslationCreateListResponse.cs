using Translation.Common.Models.Base;
using Translation.Common.Models.DataTransferObjects;

namespace Translation.Common.Models.Responses.Label.LabelTranslation
{
    public class LabelTranslationCreateListResponse : BaseResponse
    {
        public int AddedTranslationCount { get; set; }
        public int CanNotAddedTranslationCount { get; set; }
    }
}