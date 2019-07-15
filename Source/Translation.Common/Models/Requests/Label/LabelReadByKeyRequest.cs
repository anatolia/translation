using Translation.Common.Helpers;
using Translation.Common.Models.Base;

namespace Translation.Common.Models.Requests.Label
{
    public sealed class LabelReadByKeyRequest : BaseAuthenticatedRequest
    {
        public string LabelKey { get; }

        public LabelReadByKeyRequest(long currentUserId, string labelKey) : base(currentUserId)
        {
            if (labelKey.IsEmpty())
            {
                ThrowArgumentException(nameof(labelKey), labelKey);
            }

            LabelKey = labelKey;
        }
    }
}