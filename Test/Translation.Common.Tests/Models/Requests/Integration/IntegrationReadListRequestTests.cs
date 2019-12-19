using System;
using System.Collections;
using NUnit.Framework;
using Shouldly;
using Translation.Common.Models.Requests.Integration;
using static Translation.Common.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Common.Tests.Models.Requests.Integration
{
    [TestFixture]
    public class IntegrationReadListRequestTests
    {
        [Test]
        public void IntegrationReadListRequest_Constructor()
        {
            var request = GetIntegrationReadListRequest(CurrentUserId,OrganizationOneUid);

            request.CurrentUserId.ShouldBe(CurrentUserId);

        }

        public static IEnumerable ArgumentTestCases
        {
            get
            {
                yield return new TestCaseData(CurrentUserId, EmptyUid);
            }
        }

        [TestCaseSource(nameof(ArgumentTestCases))]
        public void IntegrationReadListRequest_Argument_Validations(long currentUserId)
        {
            Assert.Throws<ArgumentException>(() => { new IntegrationReadListRequest(currentUserId); });
        }
    }
}