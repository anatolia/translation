using System;

using StandardUtils.Helpers;
using StandardUtils.Models.Requests;

namespace Translation.Common.Models.Requests.Project
{
    public sealed class ProjectCloneRequest : BaseAuthenticatedRequest
    {
        public Guid OrganizationUid { get; }
        public Guid CloningProjectUid { get; }
        public string Name { get; }
        public string Url { get; }
        public string Description { get; }
        public int LabelCount { get; set; }
        public int LabelTranslationCount { get; set; }
        public bool IsSuperProject { get; set; }
        public string Slug { get; set; }
        public Guid LanguageUid { get; set; }

        public ProjectCloneRequest(long currentUserId, Guid organizationUid, Guid cloningProjectUid,
                                   string name, string url, string description,
                                   int labelCount, int labelTranslationCount, bool isSuperProject,
                                   string slug, Guid languageUid) : base(currentUserId)
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

            if (slug.IsEmpty())
            {
                ThrowArgumentException(nameof(slug), slug);
            }

            if (languageUid.IsEmptyGuid())
            {
                ThrowArgumentException(nameof(languageUid), languageUid);
            }

            OrganizationUid = organizationUid;
            CloningProjectUid = cloningProjectUid;
            Name = name;
            Url = url;
            Description = description;
            LabelCount = labelCount;
            LabelTranslationCount = labelTranslationCount;
            IsSuperProject = isSuperProject;
            Slug = slug;
            LanguageUid = languageUid;
        }
    }
}