using StandardUtils.Helpers;
using StandardUtils.Models.Requests;

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
