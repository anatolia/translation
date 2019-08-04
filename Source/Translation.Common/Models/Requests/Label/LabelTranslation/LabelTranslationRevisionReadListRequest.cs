using System;

using Translation.Common.Helpers;
using Translation.Common.Models.Base;

namespace Translation.Common.Models.Requests.Label.LabelTranslation
{
    public class LabelTranslationRevisionReadListRequest : BaseAuthenticatedRequest
    {
        public Guid LabelTranslationUid { get; }

        public LabelTranslationRevisionReadListRequest(long currentUserId, Guid labelTranslationUid) : base(currentUserId)
        {
            if (labelTranslationUid.IsEmptyGuid())
            {
                ThrowArgumentException(nameof(labelTranslationUid), labelTranslationUid);
            }

            LabelTranslationUid = labelTranslationUid;
        }
    }
}