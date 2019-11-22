using StandardUtils.Models.Requests;

namespace Translation.Common.Models.Requests.Journal
{
    public class AllJournalReadListRequest : BaseAuthenticatedPagedRequest
    {
        public AllJournalReadListRequest(long currentUserId) : base(currentUserId)
        {
            PagingInfo.IsAscending = false;
        }
    }
}