using System;

using Translation.Common.Helpers;
using Translation.Common.Models.Base;

namespace Translation.Common.Models.Requests.Integration.Token
{
    public class OrganizationTokenRequestLogReadListRequest : BaseAuthenticatedPagedRequest
    {
        public Guid OrganizationUid { get; }

        public OrganizationTokenRequestLogReadListRequest(long currentUserId, Guid organizationUid) : base(currentUserId)
        {
            if (organizationUid.IsEmptyGuid())
            {
                ThrowArgumentException(nameof(organizationUid), organizationUid);
            }

            OrganizationUid = organizationUid;
        }
    }
}