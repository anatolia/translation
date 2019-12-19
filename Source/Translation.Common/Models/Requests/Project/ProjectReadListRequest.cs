using StandardUtils.Models.Requests;

namespace Translation.Common.Models.Requests.Project
{
    public sealed class ProjectReadListRequest : BaseAuthenticatedPagedRequest
    {
        public ProjectReadListRequest(long currentUserId) : base(currentUserId)
        {
        }
    }
}