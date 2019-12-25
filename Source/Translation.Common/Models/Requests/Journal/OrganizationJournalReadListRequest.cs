using StandardUtils.Models.Requests;

namespace Translation.Common.Models.Requests.Journal
{
    public class OrganizationJournalReadListRequest : BaseAuthenticatedPagedRequest
    {
        public OrganizationJournalReadListRequest(long currentUserId) : base(currentUserId)
        {
         
        }
    }
}