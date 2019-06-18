using System;

using Translation.Client.Web.Models.Base;
using Translation.Client.Web.Models.InputModels;
using Translation.Common.Helpers;

namespace Translation.Client.Web.Models.Admin
{
    public sealed class AdminAcceptInviteModel : BaseModel
    {
        public Guid OrganizationUid { get; set; }
        public Guid Token { get; set; }
        public string Email { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string ReEnterPassword { get; set; }

        public HiddenInputModel OrganizationUidInput { get; set; }
        public HiddenInputModel TokenInput { get; }
        public HiddenInputModel EmailInput { get; }

        public InputModel FirstNameInput { get; }
        public InputModel LastNameInput { get; }
        public PasswordInputModel PasswordInput { get; }
        public PasswordInputModel ReEnterPasswordInput { get; }

        public AdminAcceptInviteModel()
        {
            Title = "admin_accept_title";

            OrganizationUidInput = new HiddenInputModel("OrganizationUid");
            TokenInput = new HiddenInputModel("Token");
            EmailInput = new HiddenInputModel("Email");

            FirstNameInput = new InputModel("FirstName", "first_name", true);
            LastNameInput = new InputModel("LastName", "last_name", true);
            PasswordInput = new PasswordInputModel("Password", "password", true);
            ReEnterPasswordInput = new PasswordInputModel("ReEnterPassword", "re_enter_password", true);
        }

        public override void SetInputModelValues()
        {
            OrganizationUidInput.Value = OrganizationUid.ToUidString();
            TokenInput.Value = Token.ToUidString();
            EmailInput.Value = Email;

            FirstNameInput.Value = FirstName;
            LastNameInput.Value = LastName;
            PasswordInput.Value = Password;
            ReEnterPasswordInput.Value = ReEnterPassword;
        }

        public override void SetInputErrorMessages()
        {
            if (OrganizationUid.IsEmptyGuid())
            {
                ErrorMessages.Add("organization_uid_is_not_valid");
            }
            
            if (Token.IsEmptyGuid())
            {
                ErrorMessages.Add("token_uid_is_not_valid");
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
            if (Password.IsNotValidPassword())
            {
                PasswordInput.ErrorMessage.Add("password_is_not_valid_error_message");
                InputErrorMessages.AddRange(PasswordInput.ErrorMessage);
            }

            ReEnterPassword.TrimOrDefault();
            if (ReEnterPassword.IsNotValidPassword())
            {
                ReEnterPasswordInput.ErrorMessage.Add("re_enter_password_is_not_valid_error_message");
                InputErrorMessages.AddRange(ReEnterPasswordInput.ErrorMessage);
            }

            Password = Password.TrimOrDefault();
            if (Password != ReEnterPassword)
            {
                PasswordInput.ErrorMessage.Add("password_and_re_entered_password_does_not_match_error_message");
                InputErrorMessages.AddRange(PasswordInput.ErrorMessage);
            }
        }
    }
}