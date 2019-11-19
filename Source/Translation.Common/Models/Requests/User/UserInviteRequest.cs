using System;

using StandardUtils.Helpers;
using StandardUtils.Models.Requests;

namespace Translation.Common.Models.Requests.User
{
    public sealed class UserInviteRequest : BaseAuthenticatedRequest
    {
        public Guid OrganizationUid { get; set; }
        public string Email { get; }
        public string FirstName { get; }
        public string LastName { get; }

        public UserInviteRequest(long currentUserId, Guid organizationUid, string email,
                                 string firstName, string lastName) : base(currentUserId)
        {
            if (organizationUid.IsEmptyGuid())
            {
                ThrowArgumentException(nameof(organizationUid), organizationUid);
            }

            if (email.IsNotEmail())
            {
                ThrowArgumentException(nameof(email), email);
            }

            if (firstName.IsEmpty())
            {
                ThrowArgumentException(nameof(firstName), firstName);
            }

            if (lastName.IsEmpty())
            {
                ThrowArgumentException(nameof(lastName), lastName);
            }

            OrganizationUid = organizationUid;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
