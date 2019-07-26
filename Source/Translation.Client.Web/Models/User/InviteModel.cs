using System;
using Translation.Client.Web.Models.Base;
using Translation.Client.Web.Models.InputModels;
using Translation.Common.Helpers;

namespace Translation.Client.Web.Models.User
{
    public sealed class InviteModel : BaseModel
    {
        public Guid OrganizationUid { get; set; }

        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public HiddenInputModel OrganizationInput { get; }

        public EmailInputModel EmailInput { get; }
        public InputModel FirstNameInput { get; }
        public InputModel LastNameInput { get; }

        public InviteModel()
        {
            Title = "user_invite_title";

            OrganizationInput = new HiddenInputModel("OrganizationUid");

            EmailInput = new EmailInputModel("Email", "email", true);
            FirstNameInput = new InputModel("FirstName", "first_name", true);
            LastNameInput = new InputModel("LastName", "last_name", true);
        }

        public override void SetInputModelValues()
        {
            OrganizationInput.Value = OrganizationUid.ToUidString();

            EmailInput.Value = Email;
            FirstNameInput.Value = FirstName;
            LastNameInput.Value = LastName;
        }

        public override void SetInputErrorMessages()
        {
            if (OrganizationUid.IsEmptyGuid())
            {
                ErrorMessages.Add("organization_uid_not_valid");
            }

            Email = Email.TrimOrDefault();
            if (Email.IsEmpty())
            {
                EmailInput.ErrorMessage.Add("email_required_error_massage");
                InputErrorMessages.AddRange(EmailInput.ErrorMessage);
            }

            if (Email.IsNotEmail())
            {
                EmailInput.ErrorMessage.Add("email_is_not_valid_error_message");
                InputErrorMessages.AddRange(EmailInput.ErrorMessage);
            }

            FirstName = FirstName.TrimOrDefault();
            if (FirstName.IsEmpty())
            {
                FirstNameInput.ErrorMessage.Add("first_name_required_error_massage");
                InputErrorMessages.AddRange(FirstNameInput.ErrorMessage);
            }

            LastName = LastName.TrimOrDefault();
            if (LastName.IsEmpty())
            {
                LastNameInput.ErrorMessage.Add("last_name_required_error_massage");
                InputErrorMessages.AddRange(LastNameInput.ErrorMessage);
            }
        }
    }
}