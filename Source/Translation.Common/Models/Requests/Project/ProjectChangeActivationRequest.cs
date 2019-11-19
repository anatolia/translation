using System;

using StandardUtils.Helpers;
using StandardUtils.Models.Requests;

namespace Translation.Common.Models.Requests.Project
{
    public class ProjectChangeActivationRequest : BaseAuthenticatedRequest
    {
        public Guid OrganizationUid { get; }
        public Guid ProjectUid { get; }

        public ProjectChangeActivationRequest(long currentUserId, Guid organizationUid, Guid projectUid) : base(currentUserId)
        {
            if (organizationUid.IsEmptyGuid())
            {
                ThrowArgumentException(nameof(organizationUid), organizationUid);
            }

            if (projectUid.IsEmptyGuid())
            {
                ThrowArgumentException(nameof(projectUid), projectUid);
            }

            OrganizationUid = organizationUid;
            ProjectUid = projectUid;
        }
    }
}