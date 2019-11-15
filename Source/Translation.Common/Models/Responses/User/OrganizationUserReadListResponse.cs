using StandardUtils.Models.Shared;

using Translation.Common.Models.Base;

namespace Translation.Common.Models.Responses.User
{
    public class OrganizationUserReadListResponse : TranslationBaseResponse
    {
        public PagingInfo PagingInfo { get; set; }

        public OrganizationUserReadListResponse()
        {
            PagingInfo = new PagingInfo();
        }
    }
}