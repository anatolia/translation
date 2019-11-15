using System;

using StandardUtils.Helpers;
using StandardUtils.Models.Requests;

namespace Translation.Common.Models.Requests.Project
{
    public sealed class ProjectPendingTranslationReadListRequest : BaseAuthenticatedPagedRequest
    {
        public Guid ProjectUid { get; }

        public ProjectPendingTranslationReadListRequest(long currentUserId, Guid projectUid) : base(currentUserId)
        {
            if (projectUid.IsEmptyGuid())
            {
                ThrowArgumentException(nameof(projectUid), projectUid);
            }

            ProjectUid = projectUid;
        }
    }
}