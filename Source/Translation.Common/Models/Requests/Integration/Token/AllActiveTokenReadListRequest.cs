using StandardUtils.Models.Requests;

namespace Translation.Common.Models.Requests.Integration.Token
{
    public class AllActiveTokenReadListRequest : BaseAuthenticatedPagedRequest
    {
        public AllActiveTokenReadListRequest(long currentUserId) : base(currentUserId)
        {
        }
    }
}