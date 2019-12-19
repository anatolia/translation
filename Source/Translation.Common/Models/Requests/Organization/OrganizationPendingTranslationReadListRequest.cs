using StandardUtils.Models.Requests;

namespace Translation.Common.Models.Requests.Organization
{
    public sealed class OrganizationPendingTranslationReadListRequest : BaseAuthenticatedPagedRequest
    {
        public OrganizationPendingTranslationReadListRequest(long currentUserId) : base(currentUserId)
        {

        }
    }
}