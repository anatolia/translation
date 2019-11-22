using System.Collections.Generic;
using Translation.Common.Models.Base;

namespace Translation.Common.Models.Responses.Label
{
    public class LabelCreateListResponse : TranslationBaseResponse
    {
        public int AddedLabelCount { get; set; }
        public int CanNotAddedLabelCount { get; set; }
        public List<string> CanNotAddedLabels { get; set; }
        public int TotalLabelCount { get; set; }

        public int AddedLabelTranslationCount { get; set; }
        public int CanNotAddedLabelTranslationCount { get; set; }
        public int UpdatedLabelTranslationCount { get; set; }

        public LabelCreateListResponse()
        {
            CanNotAddedLabels = new List<string>();
        }
    }
}