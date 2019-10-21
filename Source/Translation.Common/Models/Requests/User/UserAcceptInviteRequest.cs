using System;
using Translation.Common.Helpers;
using Translation.Common.Models.Base;

namespace Translation.Common.Models.Requests.User
{
    public sealed class UserAcceptInviteRequest : BaseRequest
    {
        public Guid Token { get; }
        public string Email { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Password { get; }
        public Guid LanguageUid { get; }
        public string LanguageName { get; }

        public UserAcceptInviteRequest(Guid token, string email, string firstName,
                                       string lastName, string password, string languageName,
                                       Guid languageUid)
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
                ThrowArgumentException(nameof(password), "");
            }

            if (languageUid.IsEmptyGuid())
            {
                ThrowArgumentException(nameof(languageUid), languageUid);
            }
            if (languageName.IsEmpty())
            {
                ThrowArgumentException(nameof(languageName), languageName);
            }

            Token = token;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Password = password;
            LanguageUid = languageUid;
            LanguageName = languageName;
        }
    }
}

