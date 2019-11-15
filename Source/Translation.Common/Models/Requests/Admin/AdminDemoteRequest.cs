using System;

using Translation.Common.Models.Requests.User;

namespace Translation.Common.Models.Requests.Admin
{
    public sealed class AdminDemoteRequest : UserBaseRequest
    {
        public AdminDemoteRequest(long currentUserId, Guid userUid) : base(currentUserId, userUid)
        {
        }
    }
}