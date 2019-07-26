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
    public class UserInviteValidateRequestTests
    {
        [Test]
        public void UserInviteValidateRequest_Constructor()
        {
            var request = GetUserInviteValidateRequest(UidOne, EmailOne);

            request.Token.ShouldBe(UidOne);
            request.Email.ShouldBe(EmailOne);
        }

        public static IEnumerable ArgumentTestCases
        {
            get
            {
                yield return new TestCaseData(EmptyUid, EmailOne);
                yield return new TestCaseData(UidOne, EmptyString);

            }
        }

        [TestCaseSource(nameof(ArgumentTestCases))]
        public void UserInviteValidateRequest_Argument_Validations(Guid token, string email)
        {
            Assert.Throws<ArgumentException>(() => { new UserInviteValidateRequest(token, email); });
        }
    }
}