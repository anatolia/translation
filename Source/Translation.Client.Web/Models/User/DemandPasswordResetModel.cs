using Translation.Client.Web.Models.Base;
using Translation.Client.Web.Models.InputModels;
using Translation.Common.Helpers;

namespace Translation.Client.Web.Models.User
{
    public sealed class DemandPasswordResetModel : BaseModel
    {
        public string Email { get; set; }

        public EmailInputModel EmailInput { get; }

        public DemandPasswordResetModel()
        {
            Title = "demand_password_reset_title";

            EmailInput = new EmailInputModel("Email", "email", true);
        }

        public override void SetInputModelValues()
        {
            EmailInput.Value = Email;
        }

        public override void SetInputErrorMessages()
        {
            Email = Email.TrimOrDefault();
            if (Email.IsEmpty())
            {
                EmailInput.ErrorMessage.Add("email_required_error_message");
                InputErrorMessages.AddRange(EmailInput.ErrorMessage);
            }

            if (Email.IsNotEmail())
            {
                EmailInput.ErrorMessage.Add("email_is_not_valid_error_message");
                InputErrorMessages.AddRange(EmailInput.ErrorMessage);
            }
        }
    }
}
