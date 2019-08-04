using System;
using System.Collections;
using NUnit.Framework;

using Shouldly;
using Translation.Common.Models.Requests.User;
using Translation.Common.Models.Shared;

using static Translation.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;
using static Translation.Tests.TestHelpers.FakeEntityTestHelper;

namespace Translation.Tests.Common.Requests.User
{
    [TestFixture]
    public class LogOnRequestTests
    {
        [Test]
        public void LogOnRequest_Constructor()
        {
            var clientLogInfo = GetClientLogInfo();
            var request = GetLogOnRequest(EmailOne, PasswordOne, clientLogInfo);

            request.Email.ShouldBe(EmailOne);
            request.Password.ShouldBe(PasswordOne);
            request.ClientLogInfo.ShouldBe(clientLogInfo);
        }

        public static IEnumerable ArgumentTestCases
        {
            get
            {
                yield return new TestCaseData(EmptyString, PasswordOne, GetClientLogInfo());
                yield return new TestCaseData(EmailOne, InvalidPassword, GetClientLogInfo());
                yield return new TestCaseData(EmailOne, PasswordOne,null);
            }
        }

        [TestCaseSource(nameof(LogOnRequestTests.ArgumentTestCases))]
        public void LogOnRequest_Argument_Validations(string email, string password, ClientLogInfo clientLogInfo)
        {
            Assert.Throws<ArgumentException>(() => { new LogOnRequest(email, password, clientLogInfo); });
        }
    }
}