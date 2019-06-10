using Translation.Common.Models.Base;

namespace Translation.Common.Models.Requests.User.LoginLog
{
    public class AllLoginLogReadListRequest : BaseAuthenticatedPagedRequest
    {
        public AllLoginLogReadListRequest(long currentUserId) : base(currentUserId)
        {

        }
    }
}