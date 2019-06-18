using Translation.Client.Web.Models.Base;

namespace Translation.Client.Web.Models.User
{
    public sealed class ResetPasswordDoneModel : BaseModel
    {
        public ResetPasswordDoneModel()
        {
            Title = "user_reset_password_done";
        }
    }
}