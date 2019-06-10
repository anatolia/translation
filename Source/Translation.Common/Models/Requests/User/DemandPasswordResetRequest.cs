using Translation.Common.Helpers;
using Translation.Common.Models.Base;

namespace Translation.Common.Models.Requests.User
{
    public sealed class DemandPasswordResetRequest : BaseRequest
    {
        public string Email { get; }

        public DemandPasswordResetRequest(string email)
        {
            if (email.IsNotEmail())
            {
                ThrowArgumentException(nameof(email), email);
            }

            Email = email.ToLowerInvariant();
        }
    }
}
