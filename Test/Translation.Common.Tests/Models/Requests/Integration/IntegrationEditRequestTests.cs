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
    public class IntegrationEditRequestTests
    {
        [Test]
        public void IntegrationEditRequest_Constructor()
        {
            var request = GetIntegrationEditRequest(CurrentUserId,UidOne,StringOne,StringTwo);

            request.CurrentUserId.ShouldBe(CurrentUserId);
            request.IntegrationUid.ShouldBe(UidOne);
            request.Name.ShouldBe(StringOne);
            request.Description.ShouldBe(StringTwo);
        }

        public static IEnumerable ArgumentTestCases
        {
            get
            {
                yield return new TestCaseData(CurrentUserId, EmptyUid,StringOne, StringTwo);
            }
        }

        [TestCaseSource(nameof(ArgumentTestCases))]
        public void IntegrationEditRequest_Argument_Validations(long currentUserId, Guid integrationUid, string name,
            string description)
        {
            Assert.Throws<ArgumentException>(() => { new IntegrationEditRequest(currentUserId, integrationUid, name, description); });
        }
    }
}