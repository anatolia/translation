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
    public class AdminAcceptInviteRequestTests
    {
        [Test]
        public void AdminAcceptInviteRequest_Constructor()
        {
            var request = GetAdminAcceptInviteRequest(UidOne, EmailOne, StringOne, StringTwo, PasswordOne);

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

        [TestCaseSource(nameof(AdminAcceptInviteRequestTests.ArgumentTestCases))]
        public void AdminAcceptInviteRequest_Argument_Validations(Guid token, string email, string firstName,
            string lastName, string password)
        {
            Assert.Throws<ArgumentException>(() => { new AdminAcceptInviteRequest(token, email, firstName, lastName, password); });
        }
    }
}