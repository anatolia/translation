using System;

using Translation.Common.Helpers;
using Translation.Common.Models.Base;

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