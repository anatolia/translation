using StandardUtils.Models.Requests;

namespace Translation.Common.Models.Requests.Journal
{
    public class JournalCreateRequest : BaseAuthenticatedRequest
    {
        public string Message { get; }

        public JournalCreateRequest(long currentUserId, string message) : base(currentUserId)
        {
            Message = message;
        }
    }
}