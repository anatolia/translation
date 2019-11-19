using System;

using StandardUtils.Helpers;
using StandardUtils.Models.Requests;

namespace Translation.Common.Models.Requests.Label.LabelTranslation
{
    public sealed class LabelTranslationDeleteRequest : BaseAuthenticatedRequest
    {
        public Guid OrganizationUid { get;}
        public Guid LabelTranslationUid { get; }

        public LabelTranslationDeleteRequest(long currentUserId, Guid organizationUid, Guid labelTranslationUid) : base(currentUserId)
        {
            if (organizationUid.IsEmptyGuid())
            {
                ThrowArgumentException(nameof(organizationUid), organizationUid);
            }

            if (labelTranslationUid.IsEmptyGuid())
            {
                ThrowArgumentException(nameof(organizationUid), labelTranslationUid);
            }

            OrganizationUid = organizationUid;
            LabelTranslationUid = labelTranslationUid;
        }
    }
}