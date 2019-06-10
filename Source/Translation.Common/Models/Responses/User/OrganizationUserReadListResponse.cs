using Translation.Common.Models.Base;
using Translation.Common.Models.Shared;

namespace Translation.Common.Models.Responses.User
{
    public class OrganizationUserReadListResponse : BaseResponse
    {
        public PagingInfo PagingInfo { get; set; }

        public OrganizationUserReadListResponse()
        {
            PagingInfo = new PagingInfo();
        }
    }
}