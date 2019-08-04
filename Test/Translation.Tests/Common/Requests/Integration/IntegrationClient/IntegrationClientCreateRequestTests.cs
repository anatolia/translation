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
    public class IntegrationClientCreateRequestTests
    {
        [Test]
        public void IntegrationClientCreateRequest_Constructor()
        {
            var request = GetIntegrationClientCreateRequest(CurrentUserId,UidOne);

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
        public void IntegrationClientCreateRequest_Argument_Validations(long currentUserId, Guid integrationUid)
        {
            Assert.Throws<ArgumentException>(() => { new IntegrationClientCreateRequest(currentUserId, integrationUid); });
        }
    }
}