using System;
using System.Reflection.Emit;
using Translation.Common.Helpers;
using Translation.Common.Models.Base;

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