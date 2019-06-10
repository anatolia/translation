using System;

namespace Translation.Common.Models.Requests.Organization
{
    public class OrganizationReadRequest : OrganizationBaseRequest
    {
        public OrganizationReadRequest(long currentUserId, Guid organizationUid) : base(currentUserId, organizationUid)
        {
        }
    }
}
