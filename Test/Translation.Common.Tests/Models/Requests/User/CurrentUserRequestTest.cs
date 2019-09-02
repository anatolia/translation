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
    public class CurrentUserRequestTests
    {
        [Test]
        public void CurrentUserRequest_Constructor()
        {
            var request = GetCurrentUserRequest(EmailOne);

            request.Email.ShouldBe(EmailOne);
        }

        public static IEnumerable ArgumentTestCases
        {
            get
            {
                yield return new TestCaseData(StringOne);
            }
        }

        [TestCaseSource(nameof(CurrentUserRequestTests.ArgumentTestCases))]
        public void CurrentUserRequest_Argument_Validations(string email)
        {
            Assert.Throws<ArgumentException>(() => { new CurrentUserRequest(email); });
        }
    }
}