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
    public class UserAcceptInviteRequestTests
    {
        [Test]
        public void UserAcceptInviteRequest_Constructor()
        {
            var request = GetUserAcceptInviteRequest(UidOne, EmailOne, StringOne, StringTwo, PasswordOne);

            request.Token.ShouldBe(UidOne);
            request.FirstName.ShouldBe(StringOne);
            request.LastName.ShouldBe(StringTwo);
            request.Email.ShouldBe(EmailOne);
            request.Password.ShouldBe(PasswordOne);
        }

        public static IEnumerable ArgumentTestCases
        {
            get
            {
                yield return new TestCaseData(EmptyUid, EmailOne, StringOne, StringTwo, PasswordOne);
                yield return new TestCaseData(UidOne, StringTwo, StringOne, StringTwo, PasswordOne);
                yield return new TestCaseData(UidOne, EmailOne, EmptyString, StringTwo, PasswordOne);
                yield return new TestCaseData(UidOne, EmailOne, StringOne, EmptyString, PasswordOne);
                yield return new TestCaseData(UidOne, EmailOne, StringOne, StringTwo, StringOne);

            }
        }

        [TestCaseSource(nameof(UserAcceptInviteRequestTests.ArgumentTestCases))]
        public void UserAcceptInviteRequest_Argument_Validations(Guid token, string email, string firstName,
            string lastName, string password)
        {
            Assert.Throws<ArgumentException>(() => { new UserAcceptInviteRequest(token, email, firstName, lastName, password); });
        }
    }
}