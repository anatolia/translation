using System;

using Translation.Common.Helpers;
using Translation.Common.Models.Base;

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