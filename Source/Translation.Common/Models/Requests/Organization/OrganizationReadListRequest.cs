using StandardUtils.Models.Requests;

namespace Translation.Common.Models.Requests.Organization
{
    public sealed class OrganizationReadListRequest : BaseAuthenticatedPagedRequest
    {
        public OrganizationReadListRequest(long currentUserId) : base(currentUserId)
        {
        }
    }
}