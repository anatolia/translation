using System;

using StandardUtils.Helpers;
using StandardUtils.Models.Requests;

namespace Translation.Common.Models.Requests.Journal
{
    public class UserJournalReadListRequest : BaseAuthenticatedPagedRequest
    {
        public Guid UserUid { get; }

        public UserJournalReadListRequest(long currentUserId, Guid userUid) : base(currentUserId)
        {
            if (userUid.IsEmptyGuid())
            {
                ThrowArgumentException(nameof(userUid), userUid);
            }

            UserUid = userUid;

            PagingInfo.IsAscending = false;
        }
    }
}