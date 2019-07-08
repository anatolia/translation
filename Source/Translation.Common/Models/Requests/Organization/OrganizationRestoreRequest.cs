using System;

using Translation.Common.Models.Base;

namespace Translation.Common.Models.Requests.Organization
{
    public class OrganizationRestoreRequest : BaseAuthenticatedRequest
    {
        public Guid OrganizationUid { get; set; }
        public int Revision { get; set; }

        public OrganizationRestoreRequest(long currentUserId, Guid organizationUid, int revision) : base(currentUserId)
        {
            OrganizationUid = organizationUid;
            Revision = revision;
        }
    }
}