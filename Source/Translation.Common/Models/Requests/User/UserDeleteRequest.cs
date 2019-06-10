using System;

namespace Translation.Common.Models.Requests.User
{
    public sealed class UserDeleteRequest : UserBaseRequest
    {
        public UserDeleteRequest(long currentUserId, Guid userUid) : base(currentUserId, userUid)
        {
        }
    }
}