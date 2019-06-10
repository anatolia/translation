using System;

using Translation.Common.Helpers;
using Translation.Common.Models.Base;

namespace Translation.Common.Models.Requests.Project
{
    public sealed class ProjectUploadFromExcelRequest : BaseAuthenticatedRequest
    {
        public Guid ProjectUid { get; }

        public ProjectUploadFromExcelRequest(long currentUserId, Guid projectUid) : base(currentUserId)
        {
            if (projectUid.IsEmptyGuid())
            {
                ThrowArgumentException(nameof(projectUid), projectUid);
            }

            ProjectUid = projectUid;
        }
    }
}