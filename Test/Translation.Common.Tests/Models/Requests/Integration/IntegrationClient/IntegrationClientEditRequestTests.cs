using System;
using System.Collections;
using NUnit.Framework;
using Shouldly;
using Translation.Common.Models.Requests.Integration.IntegrationClient;
using static Translation.Common.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Common.Tests.Models.Requests.Integration.IntegrationClient
{
    [TestFixture]
    public class IntegrationClientEditRequestTests
    {
        [Test]
        public void IntegrationClientEditRequest_Constructor()
        {
            var result = GetIntegrationClientEditRequest(CurrentUserId, OrganizationOneIntegrationOneUid, StringOne,
                                                         StringTwo);
            result.CurrentUserId.ShouldBe(CurrentUserId);
            result.IntegrationClientUid.ShouldBe(OrganizationOneIntegrationOneUid);
            result.Name.ShouldBe(StringOne);
            result.Description.ShouldBe(StringTwo);
        }

        public static IEnumerable ArgumentTestCases
        {
            get
            {
                yield return new TestCaseData(CurrentUserId, OrganizationOneIntegrationOneUid, EmptyString, StringTwo);
            }
        }

        [TestCaseSource(nameof(ArgumentTestCases))]
        public void IntegrationClientEditRequest_Argument_Validations(long currentUserId, Guid integrationClientUid, string name, string description)
        {
            Assert.Throws<ArgumentException>(() => { new IntegrationClientEditRequest(currentUserId, integrationClientUid, name, description); });
        }
    }
}