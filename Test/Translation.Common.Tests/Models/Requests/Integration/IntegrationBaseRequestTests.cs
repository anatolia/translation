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
    public class IntegrationBaseRequestTests
    {
        [Test]
        public void IntegrationBaseRequest_Constructor()
        {
            var result = GetIntegrationBaseRequest(CurrentUserId, UidOne);
                 result.CurrentUserId.ShouldBe(CurrentUserId);
                 result.IntegrationUid.ShouldBe(UidOne);
         
        }

        public static IEnumerable ArgumentTestCases
        {
            get
            {
                yield return new TestCaseData(CurrentUserId, EmptyUid);
            }
        }

        [TestCaseSource(nameof(ArgumentTestCases))]
        public void IntegrationBaseRequest_Argument_Validations(long currentUserId, Guid integrationUid)
        {
            Assert.Throws<ArgumentException>(() => { new IntegrationBaseRequest(currentUserId, integrationUid); });
        }
    }
}