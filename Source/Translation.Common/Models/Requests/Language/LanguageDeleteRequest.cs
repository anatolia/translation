using System;

using Translation.Common.Helpers;
using Translation.Common.Models.Base;

namespace Translation.Common.Models.Requests.Language
{
    public sealed class LanguageDeleteRequest : BaseAuthenticatedRequest
    {
        public Guid LanguageUid { get; }

        public LanguageDeleteRequest(long currentUserId, Guid languageUid) : base(currentUserId)
        {
            if (languageUid.IsEmptyGuid())
            {
                ThrowArgumentException(nameof(languageUid), languageUid);
            }

            LanguageUid = languageUid;
        }
    }
}