using Translation.Common.Helpers;
using Translation.Common.Models.Base;

namespace Translation.Common.Models.Requests
{
    public class SignUpRequest : BaseRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string OrganizationName { get; set; }

        public override bool IsValid()
        {
            return Email.IsEmail()
                   && FirstName.IsNotEmpty()
                   && LastName.IsNotEmpty()
                   && OrganizationName.IsNotEmpty();
        }
    }
}