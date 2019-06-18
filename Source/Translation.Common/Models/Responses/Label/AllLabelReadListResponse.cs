using System.Collections.Generic;

using Translation.Common.Models.Base;
using Translation.Common.Models.DataTransferObjects;

namespace Translation.Common.Models.Responses.Label
{
    public class AllLabelReadListResponse : BaseResponse
    {
        public List<LabelFatDto> Labels { get; set; }

        public AllLabelReadListResponse()
        {
            Labels = new List<LabelFatDto>();
        }
    }
}