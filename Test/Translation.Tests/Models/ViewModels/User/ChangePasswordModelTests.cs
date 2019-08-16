using System;
using System.Collections;
using NUnit.Framework;

using Shouldly;

using Translation.Client.Web.Models.User;
using Translation.Common.Helpers;
using static Translation.Tests.TestHelpers.FakeModelTestHelper;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;
using static Translation.Tests.TestHelpers.AssertViewModelTestHelper;

namespace Translation.Tests.Models.ViewModels.User
{
    [TestFixture]
    public class ChangePasswordModelTests
    {
        public ChangePasswordModel SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            SystemUnderTest = GetChangePasswordModel();
        }

        [Test]
        public void ChangePasswordModel_Title()
        {
            Assert.AreEqual(SystemUnderTest.Title, "user_change_password_title");
        }

        [Test]
        public void ChangePasswordModel_UserInput()
        {
            AssertHiddenInputModel(SystemUnderTest.UserInput, "UserUid");
        }

        [Test]
        public void ChangePasswordModel_OldPasswordInput()
        {
            AssertInputModel(SystemUnderTest.OldPasswordInput, "OldPassword", "old_password", true);
        }

        [Test]
        public void ChangePasswordModel_NewPasswordInput()
        {
            AssertInputModel(SystemUnderTest.NewPasswordInput, "NewPassword", "password", true);
        }

        [Test]
        public void ChangePasswordModel_ReEnterNewPasswordInput()
        {
            AssertInputModel(SystemUnderTest.ReEnterNewPasswordInput, "ReEnterNewPassword", "re_enter_new_password", true);
        }

        [Test]
        public void ChangePasswordModel_SetInputModelValues()
        {
            // arrange

            // act
            SystemUnderTest.SetInputModelValues();

            // assert
            SystemUnderTest.UserInput.Value.ShouldBe(SystemUnderTest.UserUid.ToUidString());
            SystemUnderTest.OldPasswordInput.Value.ShouldBe(SystemUnderTest.OldPassword);
            SystemUnderTest.NewPasswordInput.Value.ShouldBe(SystemUnderTest.NewPassword);
            SystemUnderTest.ReEnterNewPasswordInput.Value.ShouldBe(SystemUnderTest.ReEnterNewPassword);
        }

        public static IEnumerable MessageTestCases
        {
            get
            {
                yield return new TestCaseData(CaseOne,
                                              UidOne, PasswordOne, PasswordTwo,
                                              PasswordTwo,
                                              null,
                                              null,
                                              true);

                yield return new TestCaseData(CaseTwo,
                                              EmptyUid, EmptyString, EmptyString,
                                              EmptyString,
                                              new[] { "user_uid_is_not_valid_error_message" },
                                              new[] { "old_password_required_error_message",
                                                      "new_password_required_error_message",
                                                      "re_entered_password_required_error_message",
                                                      "old_password_is_not_valid_error_message",
                                                      "new_password_is_not_valid_error_message",
                                                      "re_entered_password_is_not_valid_error_message",
                                                      "new_password_can_not_same_as_old_password_error_message",
                                              },
                                              false);

                yield return new TestCaseData(CaseTwo,
                                              EmptyUid, EmptyString, EmptyString,
                                              PasswordOne,
                                              new[] { "user_uid_is_not_valid_error_message" },
                                              new[] { "old_password_required_error_message",
                                                      "new_password_required_error_message",
                                                      "old_password_is_not_valid_error_message",
                                                      "new_password_is_not_valid_error_message",
                                                      "new_password_can_not_same_as_old_password_error_message",
                                                      "re_entered_password_does_not_match_error_message",
                                              },
                                              false);
            }
        }

        [TestCaseSource(nameof(MessageTestCases))]
        public void ChangePasswordModel_InputErrorMessages(string caseName,
                                                           Guid userUid, string oldPassword, string newPassword,
                                                           string reEnterNewPassword,
                                                           string[] errorMessages,
                                                           string[] inputErrorMessages,
                                                           bool result)
        {
            var model = GetChangePasswordModel(userUid, oldPassword, newPassword, reEnterNewPassword);
            model.IsValid().ShouldBe(result);
            model.IsNotValid().ShouldBe(!result);

            AssertMessages(model.ErrorMessages, errorMessages);
            AssertMessages(model.InputErrorMessages, inputErrorMessages);
        }
    }
}