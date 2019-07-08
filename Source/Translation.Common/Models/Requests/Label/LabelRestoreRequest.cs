using System;

using Translation.Common.Models.Base;

namespace Translation.Common.Models.Requests.Label
{
    public class LabelRestoreRequest : BaseAuthenticatedRequest
    {
        public Guid LabelUid { get; set; }
        public int Revision { get; set; }

        public LabelRestoreRequest(long currentUserId, Guid labelUid, int revision) : base(currentUserId)
        {
            LabelUid = labelUid;
            Revision = revision;
        }
    }
}