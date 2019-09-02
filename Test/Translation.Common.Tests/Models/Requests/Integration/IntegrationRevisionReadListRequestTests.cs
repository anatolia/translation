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
    public class IntegrationRevisionReadListRequestTests
    {
        [Test]
        public void IntegrationRevisionReadListRequest_Constructor()
        {
            var request = GetIntegrationRevisionReadListRequest(CurrentUserId,UidOne);

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
        public void IntegrationRevisionReadListRequest_Argument_Validations(long currentUserId, Guid integrationUid)
        {
            Assert.Throws<ArgumentException>(() => { new IntegrationRevisionReadListRequest(currentUserId,integrationUid); });
        }
    }
}