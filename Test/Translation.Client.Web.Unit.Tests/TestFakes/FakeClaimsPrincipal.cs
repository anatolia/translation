using System.Security.Claims;

namespace Translation.Common.Tests.TestFakes
{
    public class FakeClaimsPrincipal : ClaimsPrincipal
    {
        public FakeClaimsPrincipal(params Claim[] claims) : base(new FakeClaimsIdentity(claims))
        {
        }
    }
}