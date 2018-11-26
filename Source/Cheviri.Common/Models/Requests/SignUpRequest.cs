using Cheviri.Common.Helpers;
using Cheviri.Common.Models.Base;

namespace Cheviri.Common.Models.Requests
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