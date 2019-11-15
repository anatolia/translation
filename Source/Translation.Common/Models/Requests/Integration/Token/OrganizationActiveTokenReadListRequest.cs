using System;

using StandardUtils.Helpers;
using StandardUtils.Models.Requests;

namespace Translation.Common.Models.Requests.Integration.Token
{
    public class OrganizationActiveTokenReadListRequest : BaseAuthenticatedPagedRequest
    {
        public Guid OrganizationUid { get; }

        public OrganizationActiveTokenReadListRequest(long currentUserId, Guid organizationUid) : base(currentUserId)
        {
            if (organizationUid.IsEmptyGuid())
            {
                ThrowArgumentException(nameof(organizationUid), organizationUid);
            }

            OrganizationUid = organizationUid;
        }
    }
}