using Cheviri.Client.Web.Helpers;
using Cheviri.Client.Web.Models.InputModels;
using Cheviri.Common.Helpers;

namespace Cheviri.Client.Web.Models
{
    public class SignUpModel : BaseModel
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string OrganizationName { get; set; }
        public string Password { get; set; }
        public bool IsTermsAccepted { get; set; }

        public EmailInputModel EmailInput { get; set; }
        public InputModel FirstNameInput { get; set; }
        public InputModel LastNameInput { get; set; }
        public LongInputModel OrganizationNameInput { get; set; }
        public PasswordInputModel PasswordInput { get; set; }
        public CheckboxInputModel IsTermsAcceptedInput { get; set; }

        public SignUpModel()
        {
            Title = Localizer.Localize("sign_up_title");

            EmailInput = new EmailInputModel("Email", "email", true);
            FirstNameInput = new InputModel("FirstName", "first_name", true);
            LastNameInput = new InputModel("LastName", "last_name", true);
            OrganizationNameInput = new LongInputModel("OrganizationName", "organization_name", true);
            PasswordInput = new PasswordInputModel("Password", "password", true);
            IsTermsAcceptedInput = new CheckboxInputModel("IsTermsAccepted", "accept_terms", true);
        }

        public override void SetInputModelValues()
        {
            EmailInput.Value = Email;
            FirstNameInput.Value = FirstName;
            LastNameInput.Value = LastName;
            OrganizationNameInput.Value = OrganizationName;
            PasswordInput.Value = Password;
            IsTermsAcceptedInput.Value = IsTermsAccepted;
        }

        public override void SetInputErrorMessages()
        {
            if (Email.IsEmpty())
            {
                EmailInput.ErrorMessage = "email_required_error_message";
                ErrorMessages.Add(EmailInput.ErrorMessage);
            }

            if (Email.IsNotEmail())
            {
                EmailInput.ErrorMessage = "email_is_not_valid_error_message";
                ErrorMessages.Add(EmailInput.ErrorMessage);
            }

            if (FirstName.IsEmpty())
            {
                FirstNameInput.ErrorMessage = "first_name_required_error_message";
                ErrorMessages.Add(FirstNameInput.ErrorMessage);
            }

            if (LastName.IsEmpty())
            {
                LastNameInput.ErrorMessage = "last_name_required_error_message";
                ErrorMessages.Add(LastNameInput.ErrorMessage);
            }

            if (OrganizationName.IsEmpty())
            {
                OrganizationNameInput.ErrorMessage = "organization_name_required_error_message";
                ErrorMessages.Add(OrganizationNameInput.ErrorMessage);
            }

            if (Password.IsNotValidPassword())
            {
                OrganizationNameInput.ErrorMessage = "password_is_not_valid_error_message";
                ErrorMessages.Add(OrganizationNameInput.ErrorMessage);
            }

            if (!IsTermsAccepted)
            {
                OrganizationNameInput.ErrorMessage = "you_must_accept_terms_error_message";
                ErrorMessages.Add(OrganizationNameInput.ErrorMessage);
            }
        }
    }
}