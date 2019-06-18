using Translation.Common.Models.Base;
using Translation.Common.Models.DataTransferObjects;

namespace Translation.Common.Models.Responses.Label
{
    public class LabelCreateListResponse : BaseResponse
    {
        public int AddedLabelCount { get; set; }
        public int CanNotAddedLabelCount { get; set; }

        public int AddedLabelTranslationCount { get; set; }
        public int CanNotAddedLabelTranslationCount { get; set; }
        public int UpdatedLabelTranslationCount { get; set; }
    }
}