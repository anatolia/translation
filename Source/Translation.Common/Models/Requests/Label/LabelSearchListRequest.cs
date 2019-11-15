using StandardUtils.Models.Requests;

namespace Translation.Common.Models.Requests.Label
{
    public sealed class LabelSearchListRequest : BaseAuthenticatedPagedRequest
    {
        public string SearchTerm { get;}

        public LabelSearchListRequest(long currentUserId, string searchTerm) : base(currentUserId)
        {
            SearchTerm = searchTerm;
        }

    }
}