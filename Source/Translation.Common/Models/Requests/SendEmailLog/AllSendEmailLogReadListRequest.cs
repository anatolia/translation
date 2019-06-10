using Translation.Common.Models.Base;

namespace Translation.Common.Models.Requests.SendEmailLog
{
    public class AllSendEmailLogReadListRequest : BaseAuthenticatedPagedRequest
    {
        public AllSendEmailLogReadListRequest(long currentUserId) : base(currentUserId)
        {
        }
    }
}