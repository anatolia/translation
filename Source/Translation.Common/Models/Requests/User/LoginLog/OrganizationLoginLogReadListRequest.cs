using System;

using Translation.Common.Helpers;
using Translation.Common.Models.Base;

namespace Translation.Common.Models.Requests.User.LoginLog
{
    public class OrganizationLoginLogReadListRequest : BaseAuthenticatedPagedRequest
    {
        public Guid OrganizationUid { get; set; }

        public OrganizationLoginLogReadListRequest(long currentUserId, Guid organizationUid) : base(currentUserId)
        {
            if (organizationUid.IsEmptyGuid())
            {
                ThrowArgumentException(nameof(organizationUid), organizationUid);
            }

            OrganizationUid = organizationUid;
        }
    }
}