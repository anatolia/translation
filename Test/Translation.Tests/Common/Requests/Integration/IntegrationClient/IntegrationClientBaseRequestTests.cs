using System;
using System.Collections;

using NUnit.Framework;
using Shouldly;

using Translation.Common.Models.Requests.Integration.IntegrationClient;
using static Translation.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Tests.Common.Requests.Integration.IntegrationClient
{
    [TestFixture]
    public class IntegrationClientBaseRequestTests
    {
        [Test]
        public void IntegrationClientBaseRequest_Constructor()
        {
            var result = GetIntegrationClientBaseRequest(CurrentUserId,UidOne);

            result.CurrentUserId.ShouldBe(CurrentUserId);
            result.IntegrationClientUid.ShouldBe(UidOne);

        }

        public static IEnumerable ArgumentTestCases
        {
            get
            {
                yield return new TestCaseData(CurrentUserId, EmptyUid);
            }
        }

        [TestCaseSource(nameof(ArgumentTestCases))]
        public void IntegrationClientBaseRequest_Argument_Validations(long currentUserId, Guid integrationClientUid)
        {
            Assert.Throws<ArgumentException>(() => { new IntegrationClientBaseRequest(currentUserId, integrationClientUid); });
        }
    }
}