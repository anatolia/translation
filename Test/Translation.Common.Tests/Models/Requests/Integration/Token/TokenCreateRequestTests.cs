using System;
using System.Collections;
using System.Net;
using NUnit.Framework;
using Shouldly;
using Translation.Common.Models.Requests.Integration.Token;
using static Translation.Common.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Common.Tests.Models.Requests.Integration.Token
{
    [TestFixture]
    public class TokenCreateRequestTests
    {
        [Test]
        public void TokenCreateRequest_Constructor()
        {
            var request = GetTokenCreateRequest(UidOne, UidTwo, IPAddress.Any);

            request.ClientId.ShouldBe(UidOne);
            request.ClientSecret.ShouldBe(UidTwo);
            request.IP.ShouldBe(IPAddress.Any);
        }

        public static IEnumerable ArgumentTestCases
        {
            get
            {
                yield return new TestCaseData(EmptyUid, UidTwo, IPAddress.Any);
                yield return new TestCaseData(UidOne, EmptyUid, IPAddress.Any);
            }
        }

        [TestCaseSource(nameof(ArgumentTestCases))]
        public void ProjectCreateRequest_Argument_Validations(Guid clientId, Guid clientSecret, IPAddress ip)
        {
            Assert.Throws<ArgumentException>(() => { new TokenCreateRequest(clientId, clientSecret, ip); });
        }
    }
}