using System;

using StandardUtils.Helpers;
using StandardUtils.Models.Requests;

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