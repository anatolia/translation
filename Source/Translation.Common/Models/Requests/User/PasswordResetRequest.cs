using System;

using StandardUtils.Helpers;
using StandardUtils.Models.Requests;

namespace Translation.Common.Models.Requests.User
{
    public class PasswordResetRequest : BaseRequest
    {
        public Guid Token { get; }
        public string Email { get; }
        public string Password { get; }

        public PasswordResetRequest(Guid token, string email, string password)
        {
            if (token.IsEmptyGuid())
            {
                ThrowArgumentException(nameof(token),token);
            }

            if (email.IsNotEmail())
            {
                ThrowArgumentException(nameof(email), email);
            }

            if (password.IsNotValidPassword())
            {
                ThrowArgumentException(nameof(password), "");
            }

            Token = token;
            Email = email;
            Password = password;
        }
    }
}