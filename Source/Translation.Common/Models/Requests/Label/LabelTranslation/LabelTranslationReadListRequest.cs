using System;

using Translation.Common.Helpers;
using Translation.Common.Models.Base;

namespace Translation.Common.Models.Requests.Label.LabelTranslation
{
    public sealed class LabelTranslationReadListRequest : BaseAuthenticatedPagedRequest
    {
        public Guid LabelUid { get; }

        public LabelTranslationReadListRequest(long currentUserId, Guid labelUid) : base(currentUserId)
        {
            if (labelUid.IsEmptyGuid())
            {
                ThrowArgumentException(nameof(labelUid), labelUid);
            }

            LabelUid = labelUid;
        }
    }
}