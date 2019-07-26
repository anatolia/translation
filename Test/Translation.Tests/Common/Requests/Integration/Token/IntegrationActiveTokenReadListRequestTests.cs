using System;
using System.Collections;

using NUnit.Framework;
using Shouldly;
using Translation.Common.Models.Requests.Integration.Token;
using  static  Translation.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Tests.Common.Requests.Integration.Token
{
    [TestFixture]
    public class IntegrationActiveTokenReadListRequestTests
    {
        [Test]
        public void IntegrationActiveTokenReadListRequest_Constructor()
        {
            var request = GetIntegrationActiveTokenReadListRequest(CurrentUserId,UidOne);

           request.CurrentUserId.ShouldBe(CurrentUserId);
           request.IntegrationUid.ShouldBe(UidOne);
        }
        public static IEnumerable ArgumentTestCases
        {
            get
            {
                yield return new TestCaseData(CurrentUserId, EmptyUid);
            }
        }

        [TestCaseSource(nameof(ArgumentTestCases))]
        public void IntegrationCreateRequest_Argument_Validations(long currentUserId, Guid integrationUid)
        {
            Assert.Throws<ArgumentException>(() => { new IntegrationActiveTokenReadListRequest(currentUserId, integrationUid); });
        }
    }
}