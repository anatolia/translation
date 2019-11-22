using System;

using StandardUtils.Models.Requests;

namespace Translation.Common.Models.Requests.Project
{
    public class ProjectRestoreRequest : BaseAuthenticatedRequest
    {
        public Guid ProjectUid { get; set; }
        public int Revision { get; set; }

        public ProjectRestoreRequest(long currentUserId, Guid projectUid, int revision) : base(currentUserId)
        {
            ProjectUid = projectUid;
            Revision = revision;
        }
    }
}