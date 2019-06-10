using System;

using Translation.Common.Helpers;
using Translation.Common.Models.Base;

namespace Translation.Common.Models.Requests.Project
{
    public sealed class ProjectCreateRequest : BaseAuthenticatedRequest
    {
        public Guid OrganizationUid { get; }
        public string ProjectName { get; }
        public string Url { get; }
        public string Description { get; }

        public ProjectCreateRequest(long currentUserId, Guid organizationUid, string projectName,
                                    string url, string description) : base(currentUserId)
        {
            if (organizationUid.IsEmptyGuid())
            {
                ThrowArgumentException(nameof(organizationUid), organizationUid);
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
            ProjectName = projectName;
            Url = url;
            Description = description;
        }
    }
}