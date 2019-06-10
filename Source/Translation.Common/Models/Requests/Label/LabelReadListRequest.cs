using System;

using Translation.Common.Helpers;
using Translation.Common.Models.Base;

namespace Translation.Common.Models.Requests.Label
{
    public sealed class LabelReadListRequest : BaseAuthenticatedPagedRequest
    {
        public Guid ProjectUid { get; }

        public LabelReadListRequest(long currentUserId, Guid projectUid) : base(currentUserId)
        {
            if (projectUid.IsEmptyGuid())
            {
                ThrowArgumentException(nameof(projectUid), projectUid);
            }

            ProjectUid = projectUid;
        }
    }
}