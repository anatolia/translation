using System.Security.Claims;

namespace Translation.Common.Tests.TestFakes
{
    public class FakeClaimsIdentity : ClaimsIdentity
    {
        public FakeClaimsIdentity(params Claim[] claims) : base(claims)
        {
        }

        public override bool IsAuthenticated => true;
    }
}