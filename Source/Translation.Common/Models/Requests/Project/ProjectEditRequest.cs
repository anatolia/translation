using System;

using Translation.Common.Helpers;
using Translation.Common.Models.Base;

namespace Translation.Common.Models.Requests.Project
{
    public sealed class ProjectEditRequest : BaseAuthenticatedRequest
    {
        public Guid OrganizationUid { get; }
        public Guid ProjectUid { get; }
        public string ProjectName { get; }
        public string Url { get; }
        public string Description { get; }

        public ProjectEditRequest(long currentUserId, Guid organizationUid, Guid projectUid,
                                  string projectName, string url, string description) : base(currentUserId)
        {
            if (organizationUid.IsEmptyGuid())
            {
                ThrowArgumentException(nameof(organizationUid), organizationUid);
            }

            if (projectUid.IsEmptyGuid())
            {
                ThrowArgumentException(nameof(projectUid), projectUid);
            }

            if (projectName.IsEmpty())
            {
                ThrowArgumentException(nameof(projectName), projectName);
            }

            if (url.IsNotEmpty()
                && url.IsNotUrl())
            {
                ThrowArgumentException(nameof(url), url);
            }

            OrganizationUid = organizationUid;
            ProjectUid = projectUid;
            ProjectName = projectName;
            Url = url;
            Description = description;
        }
    }
}