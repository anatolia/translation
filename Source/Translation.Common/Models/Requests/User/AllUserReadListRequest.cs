using Translation.Common.Models.Base;

namespace Translation.Common.Models.Requests.User
{
    public sealed class AllUserReadListRequest : BaseAuthenticatedPagedRequest
    {
        public AllUserReadListRequest(long currentUserId) : base(currentUserId)
        {

        }
    }
}