using Translation.Common.Models.Base;

namespace Translation.Common.Models.Requests.Journal
{
    public class AllJournalReadListRequest : BaseAuthenticatedPagedRequest
    {
        public AllJournalReadListRequest(long currentUserId) : base(currentUserId)
        {

        }
    }
}