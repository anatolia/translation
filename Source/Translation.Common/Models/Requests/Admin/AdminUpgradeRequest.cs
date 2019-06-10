using System;

using Translation.Common.Models.Requests.User;

namespace Translation.Common.Models.Requests.Admin
{
    public sealed class AdminUpgradeRequest : UserBaseRequest
    {
        public AdminUpgradeRequest(long currentUserId, Guid userUid) : base(currentUserId, userUid)
        {
        }
    }
}