using System;
using System.Collections;

using NUnit.Framework;
using Shouldly;

using Translation.Common.Models.Requests.Organization;
using Translation.Common.Models.Shared;

using static Translation.Common.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Common.Tests.Models.Requests.Organization
{

    [TestFixture]
    public class SignUpRequestTests
    {
        [Test]
        public void SignUpRequest_Constructor()
        {
            var model = GetClientLogInfo();
            var request = GetSignUpRequest(StringOne, StringTwo, StringThree,
                                           EmailOne, PasswordOne, model, UidOne);

            request.OrganizationName.ShouldBe(StringOne);
            request.FirstName.ShouldBe(StringTwo);
            request.LastName.ShouldBe(StringThree);
            request.Email.ShouldBe(EmailOne);
            request.Password.ShouldBe(PasswordOne);
            request.ClientLogInfo.ShouldBe(model);
            request.LanguageUid.ShouldBe(UidOne);
        }

        public static IEnumerable ArgumentTestCases
        {
            get
            {
                yield return new TestCaseData(EmptyString, StringTwo, StringThree,
                    EmailOne, PasswordOne, GetClientLogInfo(), UidOne);
                yield return new TestCaseData(StringOne, EmptyString, StringThree,
                    EmailOne, PasswordOne, GetClientLogInfo(), UidOne);
                yield return new TestCaseData(StringOne, StringTwo, EmptyString,
                    EmailOne, PasswordOne, GetClientLogInfo(), UidOne);
                yield return new TestCaseData(StringOne, StringTwo, StringThree,
                    InvalidEmail, PasswordOne, GetClientLogInfo(), UidOne);
                yield return new TestCaseData(StringOne, StringTwo, StringThree,
                    EmailOne, InvalidPassword, GetClientLogInfo(), UidOne);
                yield return new TestCaseData(StringOne, StringTwo, StringThree,
                    EmailOne, PasswordOne, null, UidOne);
            }
        }

        [TestCaseSource(nameof(ArgumentTestCases))]
        public void SignUpRequest_Argument_Validations(string organizationName, string firstName, string lastName,
                                                       string email, string password, ClientLogInfo clientLogInfo,
            Guid languageUid)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                new SignUpRequest(organizationName, firstName, lastName,
                                  email, password, clientLogInfo, 
                                  languageUid); ;
            });
        }
    }
}
