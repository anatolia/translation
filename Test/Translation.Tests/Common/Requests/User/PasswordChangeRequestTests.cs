using System;
using System.Collections;
using NUnit.Framework;

using Shouldly;
using Translation.Common.Models.Requests.User;

using static Translation.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;
using static Translation.Tests.TestHelpers.FakeEntityTestHelper;

namespace Translation.Tests.Common.Requests.User
{
    [TestFixture]
    public class PasswordChangeRequestTests
    {
        [Test]
        public void PasswordChangeRequest_Constructor()
        {
            var request = GetPasswordChangeRequest(CurrentUserId, PasswordOne, PasswordTwo);

            request.CurrentUserId.ShouldBe(CurrentUserId);
            request.OldPassword.ShouldBe(PasswordOne);
            request.NewPassword.ShouldBe(PasswordTwo);
        }

        public static IEnumerable ArgumentTestCases
        {
            get
            {
                yield return new TestCaseData(CurrentUserId, EmptyString, PasswordTwo);
                yield return new TestCaseData(CurrentUserId, PasswordOne, EmptyString);
            }
        }

        [TestCaseSource(nameof(PasswordChangeRequestTests.ArgumentTestCases))]
        public void PasswordChangeRequest_Argument_Validations(long currentUserId, string oldPassword, string newPassword)
        {
            Assert.Throws<ArgumentException>(() => { new PasswordChangeRequest(currentUserId, oldPassword, newPassword); });
        }
    }
}