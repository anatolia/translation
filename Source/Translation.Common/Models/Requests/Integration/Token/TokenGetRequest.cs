using Translation.Common.Models.Base;

namespace Translation.Common.Models.Requests.Integration.Token
{
    public class TokenGetRequest : BaseAuthenticatedRequest
    {
        public TokenGetRequest(long currentUserId) : base(currentUserId)
        {
        }
    }
}