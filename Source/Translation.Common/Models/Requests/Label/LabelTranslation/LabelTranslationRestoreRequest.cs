using System;

using StandardUtils.Models.Requests;

namespace Translation.Common.Models.Requests.Label.LabelTranslation
{
    public class LabelTranslationRestoreRequest : BaseAuthenticatedRequest
    {
        public Guid LabelTranslationUid { get; set; }
        public int Revision { get; set; }

        public LabelTranslationRestoreRequest(long currentUserId, Guid labelTranslationUid, int revision) : base(currentUserId)
        {
            LabelTranslationUid = labelTranslationUid;
            Revision = revision;
        }
    }
}