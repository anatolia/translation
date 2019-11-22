using System;

using StandardUtils.Helpers;
using StandardUtils.Models.Requests;

namespace Translation.Common.Models.Requests.Label
{
    public class LabelCreateWithTokenRequest : BaseRequest
    {
        public Guid Token { get; set; }
        public Guid ProjectUid { get; }
        public string LabelKey { get; }
        public string[] LanguageIsoCode2s { get; }

        public LabelCreateWithTokenRequest(Guid token, Guid projectUid, string labelKey, string[] isoCode2s)
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
            LanguageIsoCode2s = isoCode2s;
        }
    }
}