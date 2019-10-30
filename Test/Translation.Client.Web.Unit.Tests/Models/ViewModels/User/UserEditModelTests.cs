using System;
using System.Collections;

using NUnit.Framework;
using Shouldly;

using Translation.Client.Web.Models.User;
using Translation.Common.Helpers;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;
using static Translation.Client.Web.Unit.Tests.TestHelpers.AssertViewModelTestHelper;
using static Translation.Client.Web.Unit.Tests.TestHelpers.FakeModelTestHelper;

namespace Translation.Client.Web.Unit.Tests.Models.ViewModels.User
{
    [TestFixture]
    public class UserEditModelTests
    {
        public UserEditModel SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            SystemUnderTest = GetUserEditModel();
        }

        [Test]
        public void UserEditModel_Title()
        {
            Assert.AreEqual(SystemUnderTest.Title, "user_edit_title");
        }


        [Test]
        public void SignUpModel_FirstNameInput()
        {
            AssertInputModel(SystemUnderTest.FirstNameInput, "FirstName", "first_name", true);
        }

        [Test]
        public void SignUpModel_UserInput()
        {
            AssertHiddenInputModel(SystemUnderTest.UserInput, "UserUid");
        }

        [Test]
        public void SignUpModel_LastNameInput()
        {
            AssertInputModel(SystemUnderTest.LastNameInput, "LastName", "last_name", true);
        }

        [Test]
        public void SignUpModel_LanguageInput()
        {
            AssertSelectInputModel(SystemUnderTest.LanguageInput, "Language", "language", "/Language/SelectData");
            SystemUnderTest.LanguageInput.IsOptionTypeContent.ShouldBeTrue();
        }

        [Test]
        public void UserEditModel_SetInputModelValues()
        {
            // arrange

            // act
            SystemUnderTest.SetInputModelValues();

            // assert;
            SystemUnderTest.UserInput.Value.ShouldBe(SystemUnderTest.UserUid.ToUidString());
            SystemUnderTest.FirstNameInput.Value.ShouldBe(SystemUnderTest.FirstName);
            SystemUnderTest.LastNameInput.Value.ShouldBe(SystemUnderTest.LastName);
            SystemUnderTest.LanguageInput.Value.ShouldBe(SystemUnderTest.LanguageUid.ToUidString());
            SystemUnderTest.LanguageInput.Text.ShouldBe(SystemUnderTest.LanguageName);
        }

        public static IEnumerable MessageTestCases
        {
            get
            {
                yield return new TestCaseData(CaseOne,
                                              UidOne, StringOne, StringTwo, UidTwo,
                                              null,
                                              null,
                                              true);

                yield return new TestCaseData(CaseTwo,
                                              EmptyUid, EmptyString, EmptyString, EmptyUid,
                                              new[] { "user_uid_not_valid" },
                                              new[] { "first_name_required_error_message",
                                                      "last_name_required_error_message" ,
                                                      "language_uid_not_valid" },
                                              false);
            }
        }

        [TestCaseSource(nameof(MessageTestCases))]
        public void UserEditModel_InputErrorMessages(string caseName,
                                                     Guid userUid, string firstName, string lastName, Guid languageUid,
                                                       string[] errorMessages,
                                                       string[] inputErrorMessages,
                                                       bool result)
        {
            var model = GetUserEditModel(userUid, firstName, lastName, languageUid);
            model.IsValid().ShouldBe(result);
            model.IsNotValid().ShouldBe(!result);

            AssertMessages(model.ErrorMessages, errorMessages);
            AssertMessages(model.InputErrorMessages, inputErrorMessages);
        }
    }
}
