using StandardUtils.Helpers;
using StandardUtils.Models.Requests;

namespace Translation.Common.Models.Requests.Label
{
    public sealed class LabelReadByKeyRequest : BaseAuthenticatedRequest
    {
        public string ProjectName { get; }
        public string LabelKey { get; }

        public LabelReadByKeyRequest(long currentUserId, string labelKey, string projectName) : base(currentUserId)
        {
            if (projectName.IsEmpty())
            {
                ThrowArgumentException(nameof(projectName), projectName);
            }

            if (labelKey.IsEmpty())
            {
                ThrowArgumentException(nameof(labelKey), labelKey);
            }

            ProjectName = projectName;
            LabelKey = labelKey;
        }
    }
}