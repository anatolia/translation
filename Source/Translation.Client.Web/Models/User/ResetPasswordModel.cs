using System;

using StandardUtils.Helpers;

using Translation.Client.Web.Models.Base;
using Translation.Client.Web.Models.InputModels;

namespace Translation.Client.Web.Models.User
{
    public sealed class ResetPasswordModel : BaseModel
    {
        public Guid Token { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ReEnterPassword { get; set; }

        public HiddenInputModel TokenInput { get; }
        public HiddenInputModel EmailInput { get; }
        public PasswordInputModel PasswordInput { get; }
        public PasswordInputModel ReEnterPasswordInput { get; }

        public ResetPasswordModel()
        {
            Title = "reset_password_title";

            TokenInput = new HiddenInputModel("Token");
            EmailInput = new HiddenInputModel("Email");
            PasswordInput = new PasswordInputModel("Password", "password", true);
            ReEnterPasswordInput = new PasswordInputModel("ReEnterNewPassword", "re_enter_password", true);
        }

        public override void SetInputModelValues()
        {
            TokenInput.Value = Token.ToUidString();
            EmailInput.Value = Email;
            PasswordInput.Value = Password;
            ReEnterPasswordInput.Value = ReEnterPassword;
        }

        public override void SetInputErrorMessages()
        {
            if (Token.IsEmptyGuid())
            {
                ErrorMessages.Add("token_is_not_valid");
            }

            Email = Email.TrimOrDefault();
            if (Email.IsNotEmail())
            {
                ErrorMessages.Add("email_is_not_valid");
            }

            Password = Password.TrimOrDefault();
            if (Password.IsEmpty())
            {
                PasswordInput.ErrorMessage.Add("password_required_error_message");
                InputErrorMessages.AddRange(PasswordInput.ErrorMessage);
            }

            if (ReEnterPassword.IsEmpty())
            {
                PasswordInput.ErrorMessage.Add("re_entered_password_required_error_message");
                InputErrorMessages.AddRange(PasswordInput.ErrorMessage);
            }

            if (Password.IsNotValidPassword())
            {
                PasswordInput.ErrorMessage.Add("password_is_not_valid_error_message");
                InputErrorMessages.AddRange(PasswordInput.ErrorMessage);
            }

            if (ReEnterPassword.IsNotValidPassword())
            {
                ReEnterPasswordInput.ErrorMessage.Add("re_entered_password_is_not_valid_error_message");
                InputErrorMessages.AddRange(ReEnterPasswordInput.ErrorMessage);
            }

            if (Password != ReEnterPassword)
            {
                ReEnterPasswordInput.ErrorMessage.Add("re_entered_password_does_not_match_error_message");
                InputErrorMessages.AddRange(ReEnterPasswordInput.ErrorMessage);
            }
        }
    }
}
