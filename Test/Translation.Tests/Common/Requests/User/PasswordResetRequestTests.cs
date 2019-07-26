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
    public class PasswordResetRequestTests
    {
        [Test]
        public void PasswordResetRequest_Constructor()
        {
            var request = GetPasswordResetRequest(UidOne, EmailOne, PasswordOne);

            request.Token.ShouldBe(UidOne);
            request.Email.ShouldBe(EmailOne);
            request.Password.ShouldBe(PasswordOne);
        }

        public static IEnumerable ArgumentTestCases
        {
            get
            {
                yield return new TestCaseData(EmptyUid, EmailOne, PasswordOne);
                yield return new TestCaseData(UidOne, EmptyString, PasswordOne);
                yield return new TestCaseData(UidOne, EmailOne, EmptyString);
            }
        }

        [TestCaseSource(nameof(PasswordResetRequestTests.ArgumentTestCases))]
        public void PasswordResetRequest_Argument_Validations(Guid token, string email, string password)
        {
            Assert.Throws<ArgumentException>(() => { new PasswordResetRequest(token, email, password); });
        }
    }
}