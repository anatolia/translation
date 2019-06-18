using System;

using Translation.Common.Helpers;
using Translation.Common.Models.Base;

namespace Translation.Common.Models.Requests.Journal
{
    public class OrganizationJournalReadListRequest : BaseAuthenticatedPagedRequest
    {
        public Guid OrganizationUid { get; set; }

        public OrganizationJournalReadListRequest(long currentUserId, Guid organizationUid) : base(currentUserId)
        {
            if (organizationUid.IsEmptyGuid())
            {
                ThrowArgumentException(nameof(organizationUid), organizationUid);
            }

            OrganizationUid = organizationUid;

            PagingInfo.IsAscending = false;
        }
    }
}