using System;

using StandardUtils.Helpers;
using StandardUtils.Models.Requests;

namespace Translation.Common.Models.Requests.Label.LabelTranslation
{
    public sealed class LabelTranslationCreateRequest : BaseAuthenticatedRequest
    {
        public Guid OrganizationUid { get;}
        public Guid LabelUid { get; }
        public Guid LanguageUid { get; }
        public string LabelTranslation { get; }

        public LabelTranslationCreateRequest(long currentUserId, Guid organizationUid,
                                             Guid labelUid, Guid languageUid, string labelTranslation) : base(currentUserId)
        {
            if (organizationUid.IsEmptyGuid())
            {
                ThrowArgumentException(nameof(organizationUid), organizationUid);
            }

            if (labelUid.IsEmptyGuid())
            {
                ThrowArgumentException(nameof(labelUid), labelUid);
            }

            if (languageUid.IsEmptyGuid())
            {
                ThrowArgumentException(nameof(languageUid), languageUid);
            }

            if (labelTranslation.IsEmpty())
            {
                ThrowArgumentException(nameof(labelTranslation), labelTranslation);
            }

            OrganizationUid = organizationUid;
            LabelUid = labelUid;
            LanguageUid = languageUid;
            LabelTranslation = labelTranslation;
        }
    }
}