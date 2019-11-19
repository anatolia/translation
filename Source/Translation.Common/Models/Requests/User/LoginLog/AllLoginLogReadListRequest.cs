using StandardUtils.Models.Requests;

namespace Translation.Common.Models.Requests.User.LoginLog
{
    public class AllLoginLogReadListRequest : BaseAuthenticatedPagedRequest
    {
        public AllLoginLogReadListRequest(long currentUserId) : base(currentUserId)
        {

        }
    }
}