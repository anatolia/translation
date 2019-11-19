using StandardUtils.Helpers;
using StandardUtils.Models.Requests;

namespace Translation.Common.Models.Requests.User
{
    public sealed class PasswordChangeRequest : BaseAuthenticatedRequest
    {
        public string OldPassword { get; }
        public string NewPassword { get; }

        public PasswordChangeRequest(long currentUserId, string oldPassword, string newPassword) : base(currentUserId)
        {
            if (oldPassword.IsNotValidPassword())
            {
                ThrowArgumentException(nameof(oldPassword), oldPassword);
            }

            if (newPassword.IsNotValidPassword())
            {
                ThrowArgumentException(nameof(newPassword), newPassword);
            }

            OldPassword = oldPassword;
            NewPassword = newPassword;
        }
    }
}
