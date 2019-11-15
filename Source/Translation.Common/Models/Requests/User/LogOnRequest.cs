using StandardUtils.Helpers;
using StandardUtils.Models.Requests;
using StandardUtils.Models.Shared;

namespace Translation.Common.Models.Requests.User
{
    public sealed class LogOnRequest : BaseRequest
    {
        public string Email { get; }
        public string Password { get; }

        public ClientLogInfo ClientLogInfo { get; set; }

        public LogOnRequest(string email, string password, ClientLogInfo clientLogInfo)
        {
            if (email.IsNotEmail())
            {
                ThrowArgumentException(nameof(email), email);
            }

            if (password.IsNotValidPassword())
            {
                ThrowArgumentException(nameof(password), "");
            }

            if (clientLogInfo == null)
            {
                ThrowArgumentException(nameof(clientLogInfo), null);
            }

            Email = email;
            Password = password;
            ClientLogInfo = clientLogInfo;
        }
    }
}