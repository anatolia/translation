using System;

using Translation.Common.Helpers;
using Translation.Common.Models.Base;

namespace Translation.Common.Models.Requests.Label
{
    public sealed class LabelCloneRequest : BaseAuthenticatedRequest
    {
        public Guid OrganizationUid { get; set; }
        public Guid CloningLabelUid { get; }
        public Guid ProjectUid { get; }
        public string LabelKey { get; }
        public string Description { get; }

        public LabelCloneRequest(long currentUserId, Guid organizationUid, Guid cloningLabelUid, Guid projectUid,
                                 string labelKey, string description) : base(currentUserId)
        {
            if (organizationUid.IsEmptyGuid())
            {
                ThrowArgumentException(nameof(organizationUid), organizationUid);
            }

            if (cloningLabelUid.IsEmptyGuid())
            {
                ThrowArgumentException(nameof(cloningLabelUid), cloningLabelUid);
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
            CloningLabelUid = cloningLabelUid;
            ProjectUid = projectUid;
            LabelKey = labelKey;
            Description = description;
        }
    }
}