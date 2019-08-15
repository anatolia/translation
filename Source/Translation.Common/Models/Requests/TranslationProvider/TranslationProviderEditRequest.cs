using System;

using Translation.Common.Helpers;
using Translation.Common.Models.Base;

namespace Translation.Common.Models.Requests.TranslationProvider
{
    public sealed class TranslationProviderEditRequest : BaseAuthenticatedRequest
    {
        public Guid TranslationProviderUid { get; }
        public string Value { get; }
        public string Description { get; }

        public TranslationProviderEditRequest(long currentUserId,  Guid translationProviderUid,
                                string value, string description) : base(currentUserId)
        {
           

            if (translationProviderUid.IsEmptyGuid())
            {
                ThrowArgumentException(nameof(translationProviderUid), translationProviderUid);
            }

            if (value.IsEmpty())
            {
                ThrowArgumentException(nameof(value), value);
            }

            TranslationProviderUid = translationProviderUid;
            Value = value;
            Description = description;
        }
    }
}