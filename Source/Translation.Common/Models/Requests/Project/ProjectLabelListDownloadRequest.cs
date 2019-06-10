using System;

using Translation.Common.Helpers;
using Translation.Common.Models.Base;

namespace Translation.Common.Models.Requests.Project
{
    public sealed class ProjectLabelListDownloadRequest : BaseAuthenticatedRequest
    {
        public Guid ProjectUid { get; }

        public ProjectLabelListDownloadRequest(long currentUserId, Guid projectUid) : base(currentUserId)
        {
            if (projectUid.IsEmptyGuid())
            {
                ThrowArgumentException(nameof(projectUid), projectUid);
            }

            ProjectUid = projectUid;
        }
    }
}