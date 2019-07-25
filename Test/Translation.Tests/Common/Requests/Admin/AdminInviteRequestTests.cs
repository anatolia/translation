using System;
using System.Collections;
using NUnit.Framework;

using Shouldly;
using Translation.Common.Models.Requests.Admin;
using Translation.Common.Models.Requests.Project;
using static Translation.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Tests.Common.Requests.Admin
{
    [TestFixture]
    public class AdminInviteRequestTests
    {
        [Test]
        public void AdminInviteRequest_Constructor()
        {
            var request = GetAdminInviteRequest(CurrentUserId, UidOne, EmailOne, StringOne, StringTwo);

            request.CurrentUserId.ShouldBe(CurrentUserId);
            request.OrganizationUid.ShouldBe(UidOne);
            request.FirstName.ShouldBe(StringOne);
            request.LastName.ShouldBe(StringTwo);
            request.Email.ShouldBe(EmailOne);
        }

        public static IEnumerable ArgumentTestCases
        {
            get
            {
                yield return new TestCaseData(CurrentUserId, EmptyUid, EmailOne, StringOne, StringTwo);
                yield return new TestCaseData(CurrentUserId, UidOne, StringOne, StringOne, StringTwo);
                yield return new TestCaseData(CurrentUserId, UidOne, EmailOne, StringEmpty, StringTwo);
                yield return new TestCaseData(CurrentUserId, UidOne, EmailOne, StringOne, StringEmpty);
            }
        }

        [TestCaseSource(nameof(AdminInviteRequestTests.ArgumentTestCases))]
        public void AdminInviteRequest_Argument_Validations(long currentUserId, Guid organizationUid, string email, string firstName, string lastName)
        {
            Assert.Throws<ArgumentException>(() => { new AdminInviteRequest(currentUserId, organizationUid, email, firstName, lastName); });
        }
    }
}