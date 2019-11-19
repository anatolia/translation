using System;

using StandardUtils.Helpers;
using StandardUtils.Models.Requests;

namespace Translation.Common.Models.Requests.Label.LabelTranslation
{
    public sealed class LabelTranslationEditRequest : BaseAuthenticatedRequest
    {
        public Guid OrganizationUid { get;}
        public Guid LabelTranslationUid { get; }
        public string NewTranslation { get; }

        public LabelTranslationEditRequest(long currentUserId, Guid organizationUid, Guid labelTranslationUid,
                                           string newTranslation) : base(currentUserId)
        {
            if (organizationUid.IsEmptyGuid())
            {
                ThrowArgumentException(nameof(organizationUid), organizationUid);
            }

            if (labelTranslationUid.IsEmptyGuid())
            {
                ThrowArgumentException(nameof(organizationUid), labelTranslationUid);
            }

            if (newTranslation.IsEmpty())
            {
                ThrowArgumentException(nameof(newTranslation), newTranslation);
            }

            OrganizationUid = organizationUid;
            LabelTranslationUid = labelTranslationUid;
            NewTranslation = newTranslation;
        }
    }
}