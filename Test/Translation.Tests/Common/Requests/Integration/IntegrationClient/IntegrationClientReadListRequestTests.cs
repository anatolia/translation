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
    public class IntegrationClientReadListRequestTests
    {
        [Test]
        public void IntegrationClientReadListRequest_Constructor()
        {
            var request = GetIntegrationClientReadListRequest(CurrentUserId, UidOne);

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
        public void IntegrationClientReadListRequest_Argument_Validations(long currentUserId, Guid integrationUid)
        {
            Assert.Throws<ArgumentException>(() => { new IntegrationClientReadListRequest(currentUserId, integrationUid); });
        }
    }
}