using System;
using Translation.Client.Web.Models.Base;
using Translation.Client.Web.Models.InputModels;
using Translation.Common.Helpers;

namespace Translation.Client.Web.Models.User
{
    public sealed class InviteAcceptModel : BaseModel
    {
        public Guid Token { get; set; }
        public string Email { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string ReEnterPassword { get; set; }
        public Guid LanguageUid { get; set; }
        public string LanguageName { get; set; }

        public HiddenInputModel TokenInput { get; }
        public HiddenInputModel EmailInput { get; }

        public InputModel FirstNameInput { get; }
        public InputModel LastNameInput { get; }
        public PasswordInputModel PasswordInput { get; }
        public PasswordInputModel ReEnterPasswordInput { get; }
        public SelectInputModel LanguageInput { get;}

        public InviteAcceptModel()
        {
            Title = "accept_invite_title";

            TokenInput = new HiddenInputModel("Token");
            EmailInput = new HiddenInputModel("Email");

            FirstNameInput = new InputModel("FirstName", "first_name", true);
            LastNameInput = new InputModel("LastName", "last_name", true);
            PasswordInput = new PasswordInputModel("Password", "password", true);
            ReEnterPasswordInput = new PasswordInputModel("ReEnterPassword", "re_enter_password", true);
            LanguageInput = new SelectInputModel("LanguageUid", "LanguageName", "language", "/Language/SelectData");
            LanguageInput.IsOptionTypeContent = true;
        }

        public override void SetInputModelValues()
        {
            TokenInput.Value = Token.ToUidString();
            EmailInput.Value = Email;

            FirstNameInput.Value = FirstName;
            LastNameInput.Value = LastName;
            PasswordInput.Value = Password;
            ReEnterPasswordInput.Value = ReEnterPassword;

            if (LanguageUid.IsNotEmptyGuid())
            {
                LanguageInput.Value = LanguageUid.ToUidString();
                LanguageInput.Text = LanguageName;
            }
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

            FirstName = FirstName.TrimOrDefault();
            if (FirstName.IsEmpty())
            {
                FirstNameInput.ErrorMessage.Add("first_name_required_error_message");
                InputErrorMessages.AddRange(FirstNameInput.ErrorMessage);
            }

            LastName = LastName.TrimOrDefault();
            if (LastName.IsEmpty())
            {
                LastNameInput.ErrorMessage.Add("last_name_required_error_message");
                InputErrorMessages.AddRange(LastNameInput.ErrorMessage);
            }

            Password = Password.TrimOrDefault();
            if (Password.IsEmpty())
            {
                PasswordInput.ErrorMessage.Add("password_required_error_message");
                InputErrorMessages.AddRange(PasswordInput.ErrorMessage);
            }

            ReEnterPassword = ReEnterPassword.TrimOrDefault();
            if (ReEnterPassword.IsEmpty())
            {
                ReEnterPasswordInput.ErrorMessage.Add("re_enter_password_required_error_message");
                InputErrorMessages.AddRange(ReEnterPasswordInput.ErrorMessage);
            }

            Password = Password.TrimOrDefault();
            if (Password.IsNotValidPassword())
            {
                PasswordInput.ErrorMessage.Add("password_is_not_valid_error_message");
                InputErrorMessages.AddRange(PasswordInput.ErrorMessage);
            }

            ReEnterPassword = ReEnterPassword.TrimOrDefault();
            if (ReEnterPassword.IsNotValidPassword())
            {
                ReEnterPasswordInput.ErrorMessage.Add("re_enter_password_is_not_valid_error_message");
                InputErrorMessages.AddRange(ReEnterPasswordInput.ErrorMessage);
            }

            Password = Password.TrimOrDefault();
            if (Password != ReEnterPassword)
            {
                ReEnterPasswordInput.ErrorMessage.Add("re_entered_password_does_not_match_error_message");
                InputErrorMessages.AddRange(ReEnterPasswordInput.ErrorMessage);
            }

            if (LanguageUid.IsEmptyGuid())
            {
                LanguageInput.ErrorMessage.Add("language_uid_not_valid");
                InputErrorMessages.AddRange(LanguageInput.ErrorMessage);
            }
        }
    }
}
