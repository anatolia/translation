using System;
using Translation.Common.Helpers;
using Translation.Common.Models.Base;

namespace Translation.Common.Models.Requests.Label
{
    public class LabelCreateWithTokenRequest : BaseRequest
    {
        public Guid Token { get; set; }
        public Guid ProjectUid { get; }
        public string LabelKey { get; }

        public LabelCreateWithTokenRequest(Guid token, Guid projectUid, string labelKey)
        {
            if (token.IsEmptyGuid())
            {
                ThrowArgumentException(nameof(token), token);
            }

            if (projectUid.IsEmptyGuid())
            {
                ThrowArgumentException(nameof(projectUid), projectUid);
            }

            if (labelKey.IsEmpty())
            {
                ThrowArgumentException(nameof(labelKey), labelKey);
            }

            Token = token;
            ProjectUid = projectUid;
            LabelKey = labelKey;
        }
    }
}