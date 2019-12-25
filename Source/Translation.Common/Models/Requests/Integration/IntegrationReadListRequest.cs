using StandardUtils.Models.Requests;

namespace Translation.Common.Models.Requests.Integration
{
    public class IntegrationReadListRequest : BaseAuthenticatedPagedRequest
    {
        public IntegrationReadListRequest(long currentUserId) : base(currentUserId)
        {

        }
    }
}