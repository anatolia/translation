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
    public class IntegrationCreateRequestTests
    {
        [Test]
        public void IntegrationCreateRequest_Constructor()
        {
            var request = GetIntegrationCreateRequest(CurrentUserId,OrganizationOneUid,StringOne,StringTwo);

            request.CurrentUserId.ShouldBe(CurrentUserId);
            request.OrganizationUid.ShouldBe(OrganizationOneUid);
            request.Name.ShouldBe(StringOne);
            request.Description.ShouldBe(StringTwo);
        }

        public static IEnumerable ArgumentTestCases
        {
            get
            {
                yield return new TestCaseData(CurrentUserId, EmptyUid, StringOne, StringTwo);
                yield return new TestCaseData(CurrentUserId, UidOne, EmptyString, StringTwo);
            }
        }

        [TestCaseSource(nameof(ArgumentTestCases))]
        public void IntegrationCreateRequest_Argument_Validations(long currentUserId, Guid organizationUid, string name,
            string description)
        {
            Assert.Throws<ArgumentException>(() => { new IntegrationCreateRequest(currentUserId, organizationUid, name, description); });
        }
    }
}