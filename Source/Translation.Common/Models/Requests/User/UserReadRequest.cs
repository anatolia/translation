using System;

using Translation.Common.Helpers;
using Translation.Common.Models.Base;

namespace Translation.Common.Models.Requests.User
{
    public sealed class UserReadRequest : BaseAuthenticatedRequest
    {
        public Guid UserUid { get; }

        public UserReadRequest(long currentUserId, Guid userUid) : base(currentUserId)
        {
            if (userUid.IsEmptyGuid())
            {
                ThrowArgumentException(nameof(userUid), userUid);
            }

            UserUid = userUid;
        }
    }
}
