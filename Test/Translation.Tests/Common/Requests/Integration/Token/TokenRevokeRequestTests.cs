using System;
using System.Collections;
using System.Net;

using NUnit.Framework;
using Shouldly;

using Translation.Common.Models.Requests.Integration.Token;
using static Translation.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;
namespace Translation.Tests.Common.Requests.Integration.Token
{
    [TestFixture]
    public class TokenRevokeRequestTests
    {
        [Test]
        public void TokenRevokeRequest_Constructor()
        {
            var request = GetTokenRevokeRequest(CurrentUserId, UidOne, OrganizationOneIntegrationOneUid);

            request.CurrentUserId.ShouldBe(CurrentUserId);
            request.IntegrationClientUid.ShouldBe(OrganizationOneIntegrationOneUid);
            request.Token.ShouldBe(UidOne);
        }

        public static IEnumerable ArgumentTestCases
        {
            get
            {
                yield return new TestCaseData(CurrentUserId, EmptyUid, UidOne);
                yield return new TestCaseData(CurrentUserId, UidOne, EmptyUid);
            }
        }

        [TestCaseSource(nameof(ArgumentTestCases))]
        public void ProjectCreateRequest_Argument_Validations(long currentUserId, Guid token, Guid integrationClientUid)
        {
            Assert.Throws<ArgumentException>(() => { new TokenRevokeRequest(currentUserId, token, integrationClientUid); });
        }
    }
}