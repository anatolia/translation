using StandardUtils.Models.Requests;

namespace Translation.Common.Models.Responses.Admin
{
    public class SuperAdminUserReadListRequest : BaseAuthenticatedPagedRequest
    {
        public SuperAdminUserReadListRequest(long currentUserId) : base(currentUserId)
        {
        }
    }
}