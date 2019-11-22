using System;

using StandardUtils.Helpers;
using StandardUtils.Models.Requests;

namespace Translation.Common.Models.Requests.Admin
{
    public sealed class AdminAcceptInviteRequest : BaseRequest
    {
        public Guid Token { get; }
        public string Email { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Password { get; }

        public AdminAcceptInviteRequest(Guid token, string email, string firstName,
                                        string lastName, string password)
        {
            if (token.IsEmptyGuid())
            {
                ThrowArgumentException(nameof(token), token);
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

            if (password.IsNotValidPassword())
            {
                ThrowArgumentException(nameof(password), string.Empty);
            }

            Token = token;
            FirstName = firstName;
            LastName = lastName;
            Password = password;
            Email = email;
        }
    }
}