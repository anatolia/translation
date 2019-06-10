using Translation.Common.Models.Base;

namespace Translation.Common.Models.Requests.Integration.Token
{
    public class TokenReadListRequest : BaseAuthenticatedRequest
    {
        public TokenReadListRequest(long currentUserId) : base(currentUserId)
        {
        }
    }
}