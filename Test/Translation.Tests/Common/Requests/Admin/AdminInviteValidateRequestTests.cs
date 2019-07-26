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
    public class AdminInviteValidateRequestTests
    {
        [Test]
        public void AdminInviteValidateRequest_Constructor()
        {
            var request = GetAdminInviteValidateRequest(UidOne, EmailOne);

            request.Token.ShouldBe(UidOne);
            request.Email.ShouldBe(EmailOne);
        }

        public static IEnumerable ArgumentTestCases
        {
            get
            {
                yield return new TestCaseData(EmptyUid,EmailOne);
                yield return new TestCaseData(UidOne,StringOne);
            }
        }

        [TestCaseSource(nameof(AdminInviteValidateRequestTests.ArgumentTestCases))]
        public void AdminInviteValidateRequest_Argument_Validations(Guid token, string email)
        {
            Assert.Throws<ArgumentException>(() => { new AdminInviteValidateRequest(token, email); });
        }
    }
}