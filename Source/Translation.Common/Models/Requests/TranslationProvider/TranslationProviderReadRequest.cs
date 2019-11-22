using System;

using StandardUtils.Helpers;
using StandardUtils.Models.Requests;

namespace Translation.Common.Models.Requests.TranslationProvider
{
    public sealed class TranslationProviderReadRequest : BaseAuthenticatedRequest
    {
        public Guid TranslationProviderUid { get; }

        public TranslationProviderReadRequest(long currentUserId, Guid translationProviderUid) : base(currentUserId)
        {
            if (translationProviderUid.IsEmptyGuid())
            {
                ThrowArgumentException(nameof(translationProviderUid), translationProviderUid);
            }

            TranslationProviderUid = translationProviderUid;
        }
    }
}