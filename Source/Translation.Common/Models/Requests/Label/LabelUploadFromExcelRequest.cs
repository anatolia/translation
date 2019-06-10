using System;

using Translation.Common.Helpers;
using Translation.Common.Models.Base;

namespace Translation.Common.Models.Requests.Label
{
    public sealed class LabelUploadFromExcelRequest : BaseAuthenticatedRequest
    {
        public Guid LabelUid { get; }

        public LabelUploadFromExcelRequest(long currentUserId, Guid labelUid) : base(currentUserId)
        {
            if (labelUid.IsEmptyGuid())
            {
                ThrowArgumentException(nameof(labelUid), labelUid);
            }

            LabelUid = labelUid;
        }
    }
}