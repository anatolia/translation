using System;
using System.Collections;

using NUnit.Framework;
using Shouldly;

using Translation.Client.Web.Models.Admin;
using Translation.Common.Helpers;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeModelTestHelper;
using static Translation.Common.Tests.TestHelpers.AssertViewModelTestHelper;

namespace Translation.Client.Web.Unit.Tests.Models.ViewModels.Admin
{
    [TestFixture]
    public class AdminAcceptInviteModelTests
    {
        public AdminAcceptInviteModel SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            SystemUnderTest = GetAdminAcceptInviteModel();
        }

        [Test]
        public void AdminAcceptInviteModel_Title()
        {
            Assert.AreEqual(SystemUnderTest.Title, "admin_accept_title");
        }

        [Test]
        public void AdminAcceptInviteModel_OrganizationInput()
        {
            AssertHiddenInputModel(SystemUnderTest.OrganizationInput, "OrganizationUid");
        }

        [Test]
        public void AdminAcceptInviteModel_TokenInput()
        {
            AssertHiddenInputModel(SystemUnderTest.TokenInput, "Token");
        }

        [Test]
        public void AdminAcceptInviteModel_EmailInput()
        {
            AssertHiddenInputModel(SystemUnderTest.EmailInput, "Email");
        }

        [Test]
        public void AdminAcceptInviteModel_SetInputModelValues()
        {
            // arrange

            // act
            SystemUnderTest.SetInputModelValues();

            // assert
            SystemUnderTest.OrganizationInput.Value.ShouldBe(SystemUnderTest.OrganizationUid.ToUidString());
            SystemUnderTest.TokenInput.Value.ShouldBe(SystemUnderTest.Token.ToUidString());
            SystemUnderTest.EmailInput.Value.ShouldBe(SystemUnderTest.Email);
            SystemUnderTest.FirstNameInput.Value.ShouldBe(SystemUnderTest.FirstName);
            SystemUnderTest.LastNameInput.Value.ShouldBe(SystemUnderTest.LastName);
            SystemUnderTest.PasswordInput.Value.ShouldBe(SystemUnderTest.Password);
            SystemUnderTest.ReEnterPasswordInput.Value.ShouldBe(SystemUnderTest.ReEnterPassword);
        }

        public static IEnumerable MessageTestCases
        {
            get
            {
                yield return new TestCaseData(CaseOne,
                                              UidOne, UidTwo, EmailOne,
                                              StringOne, StringTwo, PasswordOne,
                                              PasswordOne,
                                              null,
                                              null,
                                              true);

                yield return new TestCaseData(CaseTwo,
                                              EmptyUid, EmptyUid, EmptyString,
                                              EmptyString, EmptyString, EmptyString,
                                              EmptyString,
                                              new[] { "organization_uid_is_not_valid",
                                                      "token_uid_is_not_valid",
                                                      "email_is_not_valid" },
                                              new[] { "first_name_required_error_message",
                                                      "last_name_required_error_message",
                                                      "password_is_not_valid_error_message",
                                                      "re_enter_password_is_not_valid_error_message"
                                              },
                                              false);
                yield return new TestCaseData(CaseThree,
                                              EmptyUid, EmptyUid, EmptyString,
                                              EmptyString, EmptyString, EmptyString,
                                              StringOne,
                                              new[] { "organization_uid_is_not_valid",
                                                      "token_uid_is_not_valid",
                                                      "email_is_not_valid" },
                                              new[] { "first_name_required_error_message",
                                                      "last_name_required_error_message",
                                                      "password_is_not_valid_error_message",
                                                      "re_enter_password_is_not_valid_error_message",
                                                      "password_and_re_entered_password_does_not_match_error_message"
                                              },
                                              false);
            }
        }

        [TestCaseSource(nameof(MessageTestCases))]
        public void AdminAcceptInviteModel_InputErrorMessages(string caseName,
                                                              Guid organizationUid, Guid token, string email,
                                                              string firstName, string lastName, string password,
                                                              string reEnterPassword,
                                                              string[] errorMessages,
                                                              string[] inputErrorMessages,
                                                              bool result)
        {
            var model = GetAdminAcceptInviteModel(organizationUid, token, email,
                                                  firstName, lastName, password,
                                                  reEnterPassword);
            model.IsValid().ShouldBe(result);
            model.IsNotValid().ShouldBe(!result);

            AssertMessages(model.ErrorMessages, errorMessages);
            AssertMessages(model.InputErrorMessages, inputErrorMessages);
        }
    }
}
