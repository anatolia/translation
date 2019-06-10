using System;

using Translation.Common.Helpers;
using Translation.Common.Models.Base;

namespace Translation.Common.Models.Requests.User.LoginLog
{
    public class UserLoginLogReadListRequest : BaseAuthenticatedPagedRequest
    {
        public Guid UserUid { get; set; }

        public UserLoginLogReadListRequest(long currentUserId, Guid userUid) : base(currentUserId)
        {
            if (userUid.IsEmptyGuid())
            {
                ThrowArgumentException(nameof(userUid), userUid);
            }

            UserUid = userUid;
        }
    }
}