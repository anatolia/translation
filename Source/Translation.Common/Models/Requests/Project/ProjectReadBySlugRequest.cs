using StandardUtils.Helpers;
using StandardUtils.Models.Requests;

namespace Translation.Common.Models.Requests.Project
{
    public sealed class ProjectReadBySlugRequest : BaseAuthenticatedRequest
    {
        public string ProjectSlug { get; }

        public ProjectReadBySlugRequest(long currentUserId, string projectSlug) : base(currentUserId)
        {
            if (projectSlug.IsEmpty())
            {
                ThrowArgumentException(nameof(projectSlug), projectSlug);
            }

            ProjectSlug = projectSlug;
        }
    }
}