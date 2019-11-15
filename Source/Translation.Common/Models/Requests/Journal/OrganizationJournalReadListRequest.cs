using System;

using StandardUtils.Helpers;
using StandardUtils.Models.Requests;

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