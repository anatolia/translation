using System;

using StandardUtils.Helpers;

namespace Translation.Common.Models.Requests.User
{
    public class UserEditRequest : UserBaseRequest
    {
        public string FirstName { get; }
        public string LastName { get; }
        public Guid LanguageUid { get; set; }

        public UserEditRequest(long currentUserId, Guid userUid, string firsName, 
                               string lastName, Guid languageUid) : base(currentUserId, userUid)
        {
            if (firsName.IsEmpty())
            {
                ThrowArgumentException(nameof(firsName), firsName);
            }

            if (lastName.IsEmpty())
            {
                ThrowArgumentException(nameof(lastName), lastName);
            }

            if (languageUid.IsEmptyGuid())
            {
                ThrowArgumentException(nameof(languageUid), languageUid);
            }

            FirstName = firsName;
            LastName = lastName;
            LanguageUid = languageUid;
        }
    }
}