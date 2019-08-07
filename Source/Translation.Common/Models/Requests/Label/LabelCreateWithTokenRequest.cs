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
        public string LanguagesIsoCode2Char { get; }

        public LabelCreateWithTokenRequest(Guid token, Guid projectUid, string labelKey, string languagesIsoCode2Char)
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
            if (languagesIsoCode2Char.IsEmpty())
            {
                ThrowArgumentException(nameof(languagesIsoCode2Char), languagesIsoCode2Char);
            }


            Token = token;
            ProjectUid = projectUid;
            LabelKey = labelKey;
            LanguagesIsoCode2Char = languagesIsoCode2Char;
        }
    }
}