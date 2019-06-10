using System;

using Translation.Common.Helpers;
using Translation.Common.Models.Base;

namespace Translation.Common.Models.Requests.User
{
    public class UserBaseRequest : BaseAuthenticatedRequest
    {
        public Guid UserUid { get; }

        public UserBaseRequest(long currentUserId, Guid userUid) : base(currentUserId)
        {
            if (userUid.IsEmptyGuid())
            {
                ThrowArgumentException(nameof(userUid), userUid);
            }

            UserUid = userUid;
        }
    }
}