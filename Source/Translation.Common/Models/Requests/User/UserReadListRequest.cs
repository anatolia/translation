using System;

using Translation.Common.Helpers;
using Translation.Common.Models.Base;

namespace Translation.Common.Models.Requests.User
{
    public sealed class UserReadListRequest : BaseAuthenticatedPagedRequest
    {
        public Guid OrganizationUid { get; }

        public UserReadListRequest(long currentUserId, Guid organizationUid) : base(currentUserId)
        {
            if (organizationUid.IsEmptyGuid())
            {
                ThrowArgumentException(nameof(organizationUid), organizationUid);
            }

            OrganizationUid = organizationUid;
        }
    }
}