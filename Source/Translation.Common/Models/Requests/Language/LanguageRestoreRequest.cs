using System;

using StandardUtils.Models.Requests;

namespace Translation.Common.Models.Requests.Language
{
    public class LanguageRestoreRequest : BaseAuthenticatedRequest
    {
        public Guid LanguageUid { get; set; }
        public int Revision { get; set; }

        public LanguageRestoreRequest(long currentUserId, Guid languageUid, int revision) : base(currentUserId)
        {
            LanguageUid = languageUid;
            Revision = revision;
        }
    }
}