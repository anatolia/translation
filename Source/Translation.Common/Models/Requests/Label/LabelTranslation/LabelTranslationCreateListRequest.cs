using System;
using System.Collections.Generic;

using Translation.Common.Helpers;
using Translation.Common.Models.Base;

namespace Translation.Common.Models.Requests.Label.LabelTranslation
{
    public sealed class LabelTranslationCreateListRequest : BaseAuthenticatedRequest
    {
        public Guid OrganizationUid { get;}
        public Guid LabelUid { get; }
        public List<TranslationListInfo> LabelTranslations { get;}

        public bool UpdateExistedTranslations { get; set; }

        public LabelTranslationCreateListRequest(long currentUserId, Guid organizationUid, Guid labelUid,
                                                 bool updateExistedTranslations, List<TranslationListInfo> labelTranslations) : base(currentUserId)
        {
            if (organizationUid.IsEmptyGuid())
            {
                ThrowArgumentException(nameof(organizationUid), organizationUid);
            }

            if (labelUid.IsEmptyGuid())
            {
                ThrowArgumentException(nameof(labelUid), labelUid);
            }

            OrganizationUid = organizationUid;
            LabelUid = labelUid;
            LabelTranslations = labelTranslations;
            UpdateExistedTranslations = updateExistedTranslations;
        }
    }
}