using System;

using StandardUtils.Helpers;
using StandardUtils.Models.Requests;

namespace Translation.Common.Models.Requests.Project
{
    public sealed class ProjectEditRequest : BaseAuthenticatedRequest
    {
        public Guid OrganizationUid { get; }
        public Guid ProjectUid { get; }
        public string ProjectName { get; }
        public string Url { get; }
        public string Description { get; }
        public string ProjectSlug { get; set; }
        public Guid LanguageUid { get; set; }

        public ProjectEditRequest(long currentUserId, Guid organizationUid, Guid projectUid,
                                  string projectName, string url, string description,
                                  string projectSlug, Guid languageUid) : base(currentUserId)
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

            if (projectSlug.IsEmpty())
            {
                ThrowArgumentException(nameof(projectSlug), projectSlug);
            }


            if (languageUid.IsEmptyGuid())
            {
                ThrowArgumentException(nameof(languageUid), languageUid);
            }

            OrganizationUid = organizationUid;
            ProjectUid = projectUid;
            ProjectName = projectName;
            Url = url;
            Description = description;
            ProjectSlug = projectSlug;
            LanguageUid = languageUid;
        }
    }
}