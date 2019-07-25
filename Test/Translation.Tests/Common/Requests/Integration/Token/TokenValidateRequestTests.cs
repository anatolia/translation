using System;
using System.Collections;
using NUnit.Framework;
using Shouldly;
using Translation.Common.Models.Requests.Integration.Token;

using static Translation.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Tests.Common.Requests.Integration.Token
{
    [TestFixture]
    public class TokenValidateRequestTests
    {
        [Test]
        public void TokenValidateRequest_Constructor()
        {
            var result = GetTokenValidateRequest(OrganizationOneProjectOneUid,UidOne);
            result.ProjectUid.ShouldBe(OrganizationOneProjectOneUid);
            result.Token.ShouldBe(UidOne);
        }
        public static IEnumerable ArgumentTestCases
        {
            get
            {
                yield return new TestCaseData(EmptyUid, UidOne);
                yield return new TestCaseData(OrganizationOneProjectOneUid, EmptyUid);
            }
        }

        [TestCaseSource(nameof(ArgumentTestCases))]
        public void IntegrationCreateRequest_Argument_Validations(Guid projectUid, Guid token)
        {
            Assert.Throws<ArgumentException>(() => { new TokenValidateRequest(projectUid, token); });
        }
    }
}