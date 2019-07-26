using System;
using Translation.Client.Web.Models.Base;
using Translation.Client.Web.Models.InputModels;
using Translation.Common.Helpers;

namespace Translation.Client.Web.Models.User
{
    public class LoginLogsModel : BaseModel
    {
        public Guid UserUid { get; set; }

        public HiddenInputModel UserInput { get; set; }

        public LoginLogsModel()
        {
            Title = "user_login_logs_title";

            UserInput = new HiddenInputModel("UserUid");
        }

        public override void SetInputModelValues()
        {
            UserInput.Value = UserUid.ToUidString();
        }

        public override void SetInputErrorMessages()
        {
            if (UserUid.IsEmptyGuid())
            {
                ErrorMessages.Add("user_uid_is_not_valid");
            }
        }
    }
}