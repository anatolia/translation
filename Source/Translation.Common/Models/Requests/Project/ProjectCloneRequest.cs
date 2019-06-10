using System;

using Translation.Common.Helpers;
using Translation.Common.Models.Base;

namespace Translation.Common.Models.Requests.Project
{
    public sealed class ProjectCloneRequest : BaseAuthenticatedRequest
    {
        public Guid OrganizationUid { get; }
        public Guid CloningProjectUid { get; }
        public string Name { get; }
        public string Url { get; }
        public string Description { get; }

        public ProjectCloneRequest(long currentUserId, Guid organizationUid, Guid cloningProjectUid,
                                   string name, string url, string description) : base(currentUserId)
        {
            if (organizationUid.IsEmptyGuid())
            {
                ThrowArgumentException(nameof(organizationUid), organizationUid);
            }

            if (cloningProjectUid.IsEmptyGuid())
            {
                ThrowArgumentException(nameof(cloningProjectUid), cloningProjectUid);
            }

            if (name.IsEmpty())
            {
                ThrowArgumentException(nameof(name), name);
            }

            if (url.IsNotEmpty() 
                && url.IsNotUrl())
            {
                ThrowArgumentException(nameof(url), url);
            }

            OrganizationUid = organizationUid;
            CloningProjectUid = cloningProjectUid;
            Name = name;
            Url = url;
            Description = description;
        }
    }
}