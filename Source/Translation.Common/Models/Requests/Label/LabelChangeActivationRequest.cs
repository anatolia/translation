using System;

using Translation.Common.Helpers;
using Translation.Common.Models.Base;

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