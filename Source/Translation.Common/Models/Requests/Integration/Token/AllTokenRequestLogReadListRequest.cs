using Translation.Common.Models.Base;

namespace Translation.Common.Models.Requests.Integration.Token
{
    public class AllTokenRequestLogReadListRequest : BaseAuthenticatedPagedRequest
    {
        public AllTokenRequestLogReadListRequest(long currentUserId) : base(currentUserId)
        {
        }
    }
}