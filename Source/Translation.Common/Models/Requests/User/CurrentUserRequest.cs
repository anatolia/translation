using Translation.Common.Helpers;
using Translation.Common.Models.Base;

namespace Translation.Common.Models.Requests.User
{
    public sealed class CurrentUserRequest : BaseRequest
    {
        public string Email { get; set; }

        public CurrentUserRequest(string email)
        {
            if (email.IsNotEmail())
            {
                ThrowArgumentException(nameof(email), email);
            }

            Email = email;
        }
    }
}