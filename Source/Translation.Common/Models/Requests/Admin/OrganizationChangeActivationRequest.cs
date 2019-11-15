using System;

using StandardUtils.Helpers;
using StandardUtils.Models.Requests;

namespace Translation.Common.Models.Requests.Admin
{
    public sealed class OrganizationChangeActivationRequest : BaseAuthenticatedRequest
    {
        public Guid OrganizationUid { get; }

        public OrganizationChangeActivationRequest(long currentUserId, Guid organizationUid) : base(currentUserId)
        {
            if (organizationUid.IsEmptyGuid())
            {
                ThrowArgumentException(nameof(organizationUid), organizationUid);
            }

            OrganizationUid = organizationUid;
        }
    }
}