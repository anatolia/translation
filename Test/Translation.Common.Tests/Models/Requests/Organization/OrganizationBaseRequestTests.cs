using System;
using System.Collections;
using NUnit.Framework;
using Shouldly;
using Translation.Common.Models.Requests.Organization;
using static Translation.Common.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Common.Tests.Models.Requests.Organization
{
    [TestFixture]
    public class OrganizationBaseRequestTests
    {
        [Test]
        public void OrganizationBaseRequest_Constructor()
        {
            var request = GetOrganizationBaseRequest(CurrentUserId, UidOne);

            request.CurrentUserId.ShouldBe(CurrentUserId);
            request.OrganizationUid.ShouldBe(UidOne);
        }

        public static IEnumerable ArgumentTestCases
        {
            get { yield return new TestCaseData(CurrentUserId, EmptyUid); }
        }

        [TestCaseSource(nameof(ArgumentTestCases))]
        public void OrganizationBaseRequest_Argument_Validations(long currentUserId, Guid organizationUid)
        {
            Assert.Throws<ArgumentException>(() => {new OrganizationBaseRequest(currentUserId, organizationUid);});
        }
    }
}
