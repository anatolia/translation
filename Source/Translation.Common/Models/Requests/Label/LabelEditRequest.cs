using System;

using Translation.Common.Helpers;
using Translation.Common.Models.Base;

namespace Translation.Common.Models.Requests.Label
{
    public sealed class LabelEditRequest : BaseAuthenticatedRequest
    {
        public Guid OrganizationUid { get; set; }
        public Guid LabelUid { get; }
        public string LabelKey { get; }
        public string Description { get; }

        public LabelEditRequest(long currentUserId, Guid organizationUid, Guid labelUid,
                                string labelKey, string description) : base(currentUserId)
        {
            if (organizationUid.IsEmptyGuid())
            {
                ThrowArgumentException(nameof(organizationUid), organizationUid);
            }

            if (labelUid.IsEmptyGuid())
            {
                ThrowArgumentException(nameof(labelUid), labelUid);
            }

            if (labelKey.IsEmpty())
            {
                ThrowArgumentException(nameof(labelKey), labelKey);
            }

            OrganizationUid = organizationUid;
            LabelUid = labelUid;
            LabelKey = labelKey;
            Description = description;
        }
    }
}