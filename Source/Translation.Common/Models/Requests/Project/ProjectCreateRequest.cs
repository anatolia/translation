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
        public string ProjectSlug { get; set; }
        public Guid LanguageUid { get; set; }

        public ProjectCreateRequest(long currentUserId, Guid organizationUid, string projectName,
                                    string url, string description, string projectSlug,
                                    Guid languageUid) : base(currentUserId)
        {
            if (organizationUid.IsEmptyGuid())
            {
                ThrowArgumentException(nameof(organizationUid), organizationUid);
            }

            if (projectName.IsEmpty())
            {
                ThrowArgumentException(nameof(projectName), projectName);
            }

            if (projectSlug.IsEmpty())
            {
                ThrowArgumentException(nameof(projectSlug), projectSlug);
            }

            if (url.IsNotEmpty()
                && url.IsNotUrl())
            {
                ThrowArgumentException(nameof(url), url);
            }

            if (languageUid.IsEmptyGuid())
            {
                ThrowArgumentException(nameof(languageUid), languageUid);
            }

            OrganizationUid = organizationUid;
            ProjectName = projectName;
            ProjectSlug = projectSlug;
            Url = url;
            Description = description;
            LanguageUid = languageUid;
        }
    }
}