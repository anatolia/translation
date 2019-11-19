using System;

using StandardUtils.Helpers;
using StandardUtils.Models.Requests;

namespace Translation.Common.Models.Requests.Label
{
    public class LabelChangeActivationRequest : BaseAuthenticatedRequest
    {
        public Guid OrganizationUid { get; }
        public Guid LabelUid { get; }

        public LabelChangeActivationRequest(long currentUserId, Guid organizationUid, Guid labelUid) : base(currentUserId)
        {
            if (organizationUid.IsEmptyGuid())
            {
                ThrowArgumentException(nameof(organizationUid), organizationUid);
            }

            if (labelUid.IsEmptyGuid())
            {
                ThrowArgumentException(nameof(labelUid), labelUid);
            }

            OrganizationUid = organizationUid;
            LabelUid = labelUid;
        }
    }
}