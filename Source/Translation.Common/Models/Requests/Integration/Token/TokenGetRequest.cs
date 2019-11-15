using StandardUtils.Models.Requests;

namespace Translation.Common.Models.Requests.Integration.Token
{
    public class TokenGetRequest : BaseAuthenticatedRequest
    {
        public TokenGetRequest(long currentUserId) : base(currentUserId)
        {
        }
    }
}