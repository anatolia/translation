using System;
using Translation.Common.Helpers;
using Translation.Common.Models.Base;

namespace Translation.Common.Models.Requests.Integration.Token
{
    public class UserLoginLogReadListRequest : BaseAuthenticatedPagedRequest
    {
        public Guid OrganizationUid { get; }

        public UserLoginLogReadListRequest(long currentUserId, Guid organizationUid) : base(currentUserId)
        {
            if (organizationUid.IsEmptyGuid())
            {
                ThrowArgumentException(nameof(organizationUid), organizationUid);
            }

            OrganizationUid = organizationUid;
        }
    }
}