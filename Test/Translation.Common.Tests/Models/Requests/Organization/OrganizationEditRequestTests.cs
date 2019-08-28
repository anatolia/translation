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
    internal class OrganizationEditRequestTests
    {
        [Test]
        public void OrganizationEditRequest_Constructor()
        {
            var request = GetOrganizationEditRequest(CurrentUserId, UidOne, StringOne,
                StringTwo);

            request.CurrentUserId.ShouldBe(CurrentUserId);
            request.OrganizationUid.ShouldBe(UidOne);
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
        public void OrganizationEditRequest_Argument_Validations(long currentUserId, Guid organizationUid, string name,
            string description = StringOne)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                new OrganizationEditRequest(currentUserId, organizationUid, name, description);
            });
        }
    }
}