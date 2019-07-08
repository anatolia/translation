using System;

using Translation.Common.Helpers;
using Translation.Common.Models.Base;

namespace Translation.Common.Models.Requests.Label
{
    public class LabelRevisionReadListRequest : BaseAuthenticatedRequest
    {
        public Guid LabelUid { get; }

        public LabelRevisionReadListRequest(long currentUserId, Guid labelUid) : base(currentUserId)
        {
            if (labelUid.IsEmptyGuid())
            {
                ThrowArgumentException(nameof(labelUid), labelUid);
            }

            LabelUid = labelUid;
        }
    }
}