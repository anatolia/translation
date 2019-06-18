using System;

using Translation.Common.Helpers;
using Translation.Common.Models.Base;

namespace Translation.Common.Models.Requests.Label
{
    public class AllLabelReadListRequest : BaseRequest
    {
        public Guid Token { get; }
        public Guid ProjectUid { get; }

        public long CurrentUserId { get; set; }
        public bool IsAddLabelsNotTranslated { get; set; }
        public bool IsDefaultProject { get; set; }

        public AllLabelReadListRequest(Guid token, Guid projectUid)
        {
            if (token.IsEmptyGuid())
            {
                ThrowArgumentException(nameof(token), token);
            }

            if (projectUid.IsEmptyGuid())
            {
                ThrowArgumentException(nameof(projectUid), projectUid);
            }

            ProjectUid = projectUid;
            Token = token;
        }

        public AllLabelReadListRequest(long currentUserId, Guid projectUid)
        {
            if (currentUserId < 1)
            {
                ThrowArgumentException(nameof(currentUserId), currentUserId);
            }
            
            if (projectUid.IsEmptyGuid())
            {
                ThrowArgumentException(nameof(projectUid), projectUid);
            }

            CurrentUserId = currentUserId;
            ProjectUid = projectUid;
        }

        public AllLabelReadListRequest(bool isDefaultProject = true)
        {
            IsDefaultProject = isDefaultProject;
        }
    }
}