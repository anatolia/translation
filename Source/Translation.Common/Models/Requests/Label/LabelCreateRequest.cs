using System;
using System.Collections.Generic;
using Translation.Common.Helpers;
using Translation.Common.Models.Base;

namespace Translation.Common.Models.Requests.Label
{
    public sealed class LabelCreateRequest : BaseAuthenticatedRequest
    {
        public Guid OrganizationUid { get; set; }
        public Guid ProjectUid { get; }
        public string LabelKey { get; }
        public string Description { get; }
        public string[] LanguageNames { get; }


        public LabelCreateRequest(long currentUserId, Guid organizationUid, Guid projectUid,
                                  string labelKey, string description, string[] languageNames) : base(currentUserId)
        {
            if (organizationUid.IsEmptyGuid())
            {
                ThrowArgumentException(nameof(organizationUid), organizationUid);
            }

            if (projectUid.IsEmptyGuid())
            {
                ThrowArgumentException(nameof(projectUid), projectUid);
            }

            if (labelKey.IsEmpty())
            {
                ThrowArgumentException(nameof(labelKey), labelKey);
            }

            OrganizationUid = organizationUid;
            ProjectUid = projectUid;
            LabelKey = labelKey;
            Description = description;
            LanguageNames = languageNames;
        }
    }
}