using System;

namespace Translation.Common.Models.Requests.User
{
    public sealed class UserChangeActivationRequest : UserBaseRequest
    {
        public UserChangeActivationRequest(long currentUserId, Guid userUid) : base(currentUserId, userUid)
        {
        }
    }
}