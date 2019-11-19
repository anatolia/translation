using System;

using StandardUtils.Helpers;
using StandardUtils.Models.Requests;
using StandardUtils.Models.Shared;

namespace Translation.Common.Models.Requests.Organization
{
    public class SignUpRequest : BaseRequest
    {
        public string FirstName { get; }
        public string LastName { get; }
        public string OrganizationName { get; }
        public string Email { get; }
        public string Password { get; }
        public ClientLogInfo ClientLogInfo { get; set; }
        public Guid LanguageUid { get; }

        public SignUpRequest(string organizationName, string firstName, string lastName,
                             string email, string password, ClientLogInfo clientLogInfo, 
                             Guid languageUid = default)
        {
            if (organizationName.IsEmpty())
            {
                ThrowArgumentException(nameof(organizationName), organizationName);
            }

            if (firstName.IsEmpty())
            {
                ThrowArgumentException(nameof(firstName), firstName);
            }

            if (lastName.IsEmpty())
            {
                ThrowArgumentException(nameof(lastName), lastName);
            }

            if (email.IsNotEmail())
            {
                ThrowArgumentException(nameof(email), email);
            }

            if (password.IsNotValidPassword())
            {
                ThrowArgumentException(nameof(password), password);
            }

            if (clientLogInfo == null)
            {
                ThrowArgumentException(nameof(clientLogInfo), null);
            }

            if (languageUid.IsNotEmptyGuid())
            {
                LanguageUid = languageUid;
            }

            OrganizationName = organizationName;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            ClientLogInfo = clientLogInfo;
        }
    }
}