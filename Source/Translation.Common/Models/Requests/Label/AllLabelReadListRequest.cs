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
            ProjectUid = projectUid.ThrowIfWrongUid(nameof(projectUid));
            Token = token.ThrowIfWrongUid(nameof(token));
        }

        public AllLabelReadListRequest(long currentUserId, Guid projectUid)
        {
            CurrentUserId = currentUserId.ThrowIfWrongId(nameof(currentUserId));
            ProjectUid = projectUid.ThrowIfWrongUid(nameof(projectUid));
        }

        public AllLabelReadListRequest(bool isDefaultProject = true)
        {
            IsDefaultProject = isDefaultProject;
        }
    }
}