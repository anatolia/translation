using System;
using System.Collections;
using NUnit.Framework;

using Shouldly;

using Translation.Common.Models.Requests.Admin;
using static Translation.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Tests.Common.Requests.Admin
{
    [TestFixture]
    public class OrganizationChangeActivationRequestTests
    {
        [Test]
        public void OrganizationChangeActivationRequest_Constructor()
        {
            var request = GetOrganizationChangeActivationRequest(CurrentUserId, UidOne);

            request.CurrentUserId.ShouldBe(CurrentUserId);
            request.OrganizationUid.ShouldBe(UidOne);

        }

        public static IEnumerable ArgumentTestCases
        {
            get
            {
                yield return new TestCaseData(CurrentUserId, EmptyUid);
            }
        }

        [TestCaseSource(nameof(ArgumentTestCases))]
        public void OrganizationChangeActivationRequest_Argument_Validations(long currentUserId, Guid organizationUid)
        {
            Assert.Throws<ArgumentException>(() => { new OrganizationChangeActivationRequest(currentUserId, organizationUid); });
        }
    }
}