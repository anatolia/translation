using System;

using Translation.Common.Helpers;
using Translation.Common.Models.Base;

namespace Translation.Common.Models.Requests.Admin
{
    public sealed class TranslationProviderChangeActivationRequest : BaseAuthenticatedRequest
    {
        public Guid TranslationProviderUid { get; }

        public TranslationProviderChangeActivationRequest(long currentUserId, Guid translationProviderUid) : base(currentUserId)
        {
            if (translationProviderUid.IsEmptyGuid())
            {
                ThrowArgumentException(nameof(translationProviderUid), translationProviderUid);
            }

            TranslationProviderUid = translationProviderUid;
        }
    }
}