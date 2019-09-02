using System;
using System.Collections;

using NUnit.Framework;
using Shouldly;

using Translation.Common.Models.Requests.User;
using static Translation.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Tests.Common.Requests.User
{
    [TestFixture]
    public class UserInviteRequestTests
    {
        [Test]
        public void UserInviteRequest_Constructor()
        {
            var request = GetUserInviteRequest(CurrentUserId, UidOne, EmailOne, StringOne, StringTwo);

            request.CurrentUserId.ShouldBe(CurrentUserId);
            request.OrganizationUid.ShouldBe(UidOne);
            request.Email.ShouldBe(EmailOne);
            request.FirstName.ShouldBe(StringOne);
            request.LastName.ShouldBe(StringTwo);
        }

        public static IEnumerable ArgumentTestCases
        {
            get
            {
                yield return new TestCaseData(CurrentUserId, EmptyUid, EmailOne, StringOne, StringTwo);
                yield return new TestCaseData(CurrentUserId, UidOne, EmptyString, StringOne, StringTwo);
                yield return new TestCaseData(CurrentUserId, UidOne, EmailOne, EmptyString, StringTwo);
                yield return new TestCaseData(CurrentUserId, UidOne, EmailOne, StringOne, EmptyString);
            }
        }

        [TestCaseSource(nameof(ArgumentTestCases))]
        public void UserInviteRequest_Argument_Validations(long currentUserId, Guid organizationUid, string email,
            string firstName, string lastName)
        {
            Assert.Throws<ArgumentException>(() => { new UserInviteRequest(currentUserId, organizationUid, email, firstName, lastName); });
        }
    }
}