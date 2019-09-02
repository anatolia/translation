using System;
using System.Collections;
using NUnit.Framework;

using Shouldly;

using Translation.Client.Web.Models.User;
using Translation.Common.Helpers;
using static Translation.Common.Tests.TestHelpers.FakeModelTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;
using static Translation.Common.Tests.TestHelpers.AssertViewModelTestHelper;

namespace Translation.Client.Web.Unit.Tests.Models.ViewModels.User
{
    [TestFixture]
    public class ResetPasswordModelTests
    {
        public ResetPasswordModel SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            SystemUnderTest = GetResetPasswordModel();
        }

        [Test]
        public void ResetPasswordModel_Title()
        {
            Assert.AreEqual(SystemUnderTest.Title, "reset_password_title");
        }

        [Test]
        public void ResetPasswordModel_TokenInput()
        {
            AssertHiddenInputModel(SystemUnderTest.TokenInput, "Token");
        }

        [Test]
        public void ResetPasswordModel_EmailInput()
        {
            AssertHiddenInputModel(SystemUnderTest.EmailInput, "Email");
        }

        [Test]
        public void ResetPasswordModel_PasswordInput()
        {
            AssertInputModel(SystemUnderTest.PasswordInput, "Password", "password", true);
        }

        [Test]
        public void ResetPasswordModel_ReEnterNewPasswordInput()
        {
            AssertInputModel(SystemUnderTest.ReEnterPasswordInput, "ReEnterNewPassword", "re_enter_password", true);
        }

        [Test]
        public void ResetPasswordModel_SetInputModelValues()
        {
            // arrange

            // act
            SystemUnderTest.SetInputModelValues();

            // assert
            SystemUnderTest.TokenInput.Value.ShouldBe(SystemUnderTest.Token.ToUidString());
            SystemUnderTest.EmailInput.Value.ShouldBe(SystemUnderTest.Email);
            SystemUnderTest.PasswordInput.Value.ShouldBe(SystemUnderTest.Password);
            SystemUnderTest.ReEnterPasswordInput.Value.ShouldBe(SystemUnderTest.ReEnterPassword);
        }

        public static IEnumerable MessageTestCases
        {
            get
            {
                yield return new TestCaseData(CaseOne,
                                              UidOne, EmailOne, PasswordTwo,
                                              PasswordTwo,
                                              null,
                                              null,
                                              true);

                yield return new TestCaseData(CaseTwo,
                                              EmptyUid, EmptyString, EmptyString,
                                              EmptyString,
                                              new[] { "token_is_not_valid",
                                                      "email_is_not_valid",
                                              },
                                              new[] { "password_required_error_message",
                                                      "re_entered_password_required_error_message",
                                                      "password_is_not_valid_error_message",
                                                      "re_entered_password_is_not_valid_error_message"
                                              },
                                              false);
                yield return new TestCaseData(CaseThree,
                                              EmptyUid, EmptyString, EmptyString,
                                              PasswordTwo,
                                              new[] { "token_is_not_valid",
                                                      "email_is_not_valid",
                                              },
                                              new[] { "password_required_error_message",
                                                      "password_is_not_valid_error_message",
                                                      "re_entered_password_does_not_match_error_message"
                                              },
                                              false);
            }
        }

        [TestCaseSource(nameof(MessageTestCases))]
        public void ResetPasswordModel_InputErrorMessages(string caseName,
                                                          Guid token, string email, string password,
                                                           string reEnterPassword,
                                                           string[] errorMessages,
                                                           string[] inputErrorMessages,
                                                           bool result)
        {
            var model = GetResetPasswordModel(token, email, password, reEnterPassword);
            model.IsValid().ShouldBe(result);
            model.IsNotValid().ShouldBe(!result);

            AssertMessages(model.ErrorMessages, errorMessages);
            AssertMessages(model.InputErrorMessages, inputErrorMessages);
        }
    }
}