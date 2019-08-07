using System;
using System.Collections;

using NUnit.Framework;
using Shouldly;

using Translation.Client.Web.Models.User;
using Translation.Common.Helpers;
using static Translation.Tests.TestHelpers.FakeModelTestHelper;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;
using static Translation.Tests.TestHelpers.AssertViewModelTestHelper;

namespace Translation.Tests.Client.Models.ViewModels.User
{
    [TestFixture]
    public class InviteAcceptModelTests
    {
        public InviteAcceptModel SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            SystemUnderTest = GetInviteAcceptModel();
        }

        [Test]
        public void InviteAcceptModel_Title()
        {
            Assert.AreEqual(SystemUnderTest.Title, "accept_invite_title");
        }

        [Test]
        public void InviteAcceptModel_UserInput()
        {
            AssertHiddenInputModel(SystemUnderTest.TokenInput, "Token");
        }

        [Test]
        public void InviteAcceptModel_EmailInput()
        {
            AssertHiddenInputModel(SystemUnderTest.EmailInput, "Email");
        }

        [Test]
        public void InviteAcceptModel_FirstNameInput()
        {
            AssertInputModel(SystemUnderTest.FirstNameInput, "FirstName", "first_name", true);
        }

        [Test]
        public void InviteAcceptModel_LastNameInput()
        {
            AssertInputModel(SystemUnderTest.LastNameInput, "LastName", "last_name", true);
        }

        [Test]
        public void InviteAcceptModel_PasswordInput()
        {
            AssertInputModel(SystemUnderTest.PasswordInput, "Password", "password", true);
        }

        [Test]
        public void InviteAcceptModel_ReEnterPasswordInput()
        {
            AssertInputModel(SystemUnderTest.ReEnterPasswordInput, "ReEnterPassword", "re_enter_password", true);
        }

        [Test]
        public void InviteAcceptModel_LanguageInput()
        {
            AssertSelectInputModel(SystemUnderTest.LanguageInput, "LanguageUid", "LanguageName", "language", "/Language/SelectData");
            SystemUnderTest.LanguageInput.IsOptionTypeContent.ShouldBeTrue();
        }

        public void InviteAcceptModel_SetInputModelValues()
        {
            // arrange

            // act
            SystemUnderTest.SetInputModelValues();

            // assert
            SystemUnderTest.TokenInput.Value.ShouldBe(SystemUnderTest.Token.ToUidString());
            SystemUnderTest.EmailInput.Value.ShouldBe(SystemUnderTest.Email);

            SystemUnderTest.FirstNameInput.Value.ShouldBe(SystemUnderTest.FirstName);
            SystemUnderTest.LastNameInput.Value.ShouldBe(SystemUnderTest.LastName);
            SystemUnderTest.PasswordInput.Value.ShouldBe(SystemUnderTest.Password);
            SystemUnderTest.ReEnterPasswordInput.Value.ShouldBe(SystemUnderTest.ReEnterPassword);
            SystemUnderTest.LanguageInput.Value.ShouldBe(SystemUnderTest.LanguageUid.ToUidString());
            SystemUnderTest.LanguageInput.Text.ShouldBe(SystemUnderTest.LanguageName);
        }

        public static IEnumerable MessageTestCases
        {
            get
            {
                yield return new TestCaseData(CaseOne,
                                              UidOne, EmailOne, StringOne,
                                              StringTwo, PasswordOne, PasswordOne,
                                              UidOne, StringTwo,
                                              null,
                                              null,
                                              true);

                yield return new TestCaseData(CaseTwo,
                                              EmptyUid, EmptyString, EmptyString,
                                              EmptyString, EmptyString, EmptyString,
                                              EmptyUid, EmptyString,
                                              new[] { "token_is_not_valid",
                                                      "email_is_not_valid"
                                              },
                                              new[] { "first_name_required_error_message",
                                                      "last_name_required_error_message",
                                                      "password_required_error_message",
                                                      "re_enter_password_required_error_message",
                                                      "password_is_not_valid_error_message",
                                                      "re_enter_password_is_not_valid_error_message",
                                                      "language_uid_not_valid"
                                              },
                                              false);


                yield return new TestCaseData(CaseThree,
                                              EmptyUid, InvalidEmail, EmptyString,
                                              EmptyString, InvalidPassword, StringTwo,
                                              EmptyUid, EmptyString,
                                              new[] { "token_is_not_valid",
                                                      "email_is_not_valid"
                                              },
                                              new[] { "first_name_required_error_message",
                                                      "last_name_required_error_message",
                                                      "password_is_not_valid_error_message",
                                                      "re_enter_password_is_not_valid_error_message",
                                                      "re_entered_password_does_not_match_error_message",
                                                      "language_uid_not_valid"
                                              },
                                              false);
            }
        }

        [TestCaseSource(nameof(MessageTestCases))]
        public void InviteAcceptModel_InputErrorMessages(string caseName,
                                                           Guid token, string email, string firstName,
                                                           string lastName, string password, string reEnterPassword,
                                                           Guid languageUid, string languageName,
                                                           string[] errorMessages,
                                                           string[] inputErrorMessages,
                                                           bool result)
        {
            var model = GetInviteAcceptModel(token, email, firstName,
                                             lastName, password, reEnterPassword,
                                             languageUid, languageName);
            model.IsValid().ShouldBe(result);
            model.IsNotValid().ShouldBe(!result);

            AssertMessages(model.ErrorMessages, errorMessages);
            AssertMessages(model.InputErrorMessages, inputErrorMessages);
        }
    }
}