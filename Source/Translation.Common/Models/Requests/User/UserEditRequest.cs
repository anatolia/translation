using System;

using Translation.Common.Helpers;

namespace Translation.Common.Models.Requests.User
{
    public class UserEditRequest : UserBaseRequest
    {
        public string UserEmail { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public bool IsActive { get; }

        public UserEditRequest(long currentUserId, Guid userUid, string email,
                               string firsName, string lastName, bool isActive) : base(currentUserId, userUid)
        {
            if (email.IsNotEmail())
            {
                ThrowArgumentException(nameof(email), email);
            }

            if (firsName.IsEmpty())
            {
                ThrowArgumentException(nameof(firsName), firsName);
            }

            if (lastName.IsEmpty())
            {
                ThrowArgumentException(nameof(lastName), lastName);
            }

            UserEmail = email;
            FirstName = firsName;
            LastName = lastName;
            IsActive = isActive;
        }
    }
}