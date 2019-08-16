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
    public class SignUpModelTests
    {
        public SignUpModel SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            SystemUnderTest = GetSignUpModel();
        }

        [Test]
        public void SignUpModel_Title()
        {
            Assert.AreEqual(SystemUnderTest.Title, "sign_up_title");
        }

        [Test]
        public void SignUpModel_EmailInput()
        {
            AssertInputModel(SystemUnderTest.EmailInput, "Email", "email", true);
        }

        [Test]
        public void SignUpModel_FirstNameInput()
        {
            AssertInputModel(SystemUnderTest.FirstNameInput, "FirstName", "first_name", true);
        }

        [Test]
        public void SignUpModel_LastNameInput()
        {
            AssertInputModel(SystemUnderTest.LastNameInput, "LastName", "last_name", true);
        }

        [Test]
        public void SignUpModel_OrganizationNameInput()
        {
            AssertInputModel(SystemUnderTest.OrganizationNameInput, "OrganizationName", "organization_name", true);
        }

        [Test]
        public void SignUpModel_PasswordInput()
        {
            AssertInputModel(SystemUnderTest.PasswordInput, "Password", "password", true);
        }

        [Test]
        public void SignUpModel_LanguageInput()
        {
            AssertSelectInputModel(SystemUnderTest.LanguageInput, "Language", "language", "/Language/SelectData");
            SystemUnderTest.LanguageInput.IsOptionTypeContent.ShouldBeTrue();
        }

        [Test]
        public void SignUpModel_IsTermsAcceptedInput()
        {
            AssertInputModel(SystemUnderTest.IsTermsAcceptedInput, "IsTermsAccepted", "accept_terms", true);
        }

        public void SignUpModel_SetInputModelValues()
        {
            // arrange

            // act
            SystemUnderTest.SetInputModelValues();

            // assert;
            SystemUnderTest.EmailInput.Value.ShouldBe(SystemUnderTest.Email);

            SystemUnderTest.FirstNameInput.Value.ShouldBe(SystemUnderTest.FirstName);
            SystemUnderTest.LastNameInput.Value.ShouldBe(SystemUnderTest.LastName);
            SystemUnderTest.OrganizationNameInput.Value.ShouldBe(SystemUnderTest.OrganizationName);
            SystemUnderTest.PasswordInput.Value.ShouldBe(SystemUnderTest.Password);
            SystemUnderTest.IsTermsAcceptedInput.Value.ShouldBe(SystemUnderTest.IsTermsAccepted);
            SystemUnderTest.LanguageInput.Value.ShouldBe(SystemUnderTest.LanguageUid.ToUidString());
            SystemUnderTest.LanguageInput.Text.ShouldBe(SystemUnderTest.LanguageName);

        }

        public static IEnumerable MessageTestCases
        {
            get
            {
                yield return new TestCaseData(CaseOne,
                                              EmailOne, StringOne, StringTwo,
                                              StringThree, PasswordOne, UidOne,
                                              StringTwo, BooleanTrue,
                                              null,
                                              null,
                                              true);

                yield return new TestCaseData(CaseTwo,
                                              StringOne, EmptyString, EmptyString,
                                              EmptyString, InvalidPassword, EmptyUid,
                                              EmptyString, BooleanFalse,
                                              new[] { "language_uid_not_valid",
                                              },
                                              new[] { "email_is_not_valid_error_message",
                                                      "first_name_required_error_message",
                                                      "last_name_required_error_message",
                                                      "organization_name_required_error_message",
                                                      "password_is_not_valid_error_message",
                                                      "you_must_accept_terms_error_message"
                                              },
                                              false);

                yield return new TestCaseData(CaseThree,
                                              EmptyString, EmptyString, EmptyString,
                                              EmptyString, InvalidPassword, EmptyUid,
                                              EmptyString, BooleanFalse,
                                              new[] { "language_uid_not_valid",
                                              },
                                              new[] { "email_required_error_message",
                                                      "email_is_not_valid_error_message",
                                                      "first_name_required_error_message",
                                                      "last_name_required_error_message",
                                                      "organization_name_required_error_message",
                                                      "password_is_not_valid_error_message",
                                                      "you_must_accept_terms_error_message"
                                              },
                                              false);
            }
        }

        [TestCaseSource(nameof(MessageTestCases))]
        public void SignUpModel_InputErrorMessages(string caseName,
                                                   string email, string firstName, string lastName,
                                                   string organizationName, string password, Guid languageUid,
                                                   string languageName, bool isTermsAccepted,
                                                           string[] errorMessages,
                                                           string[] inputErrorMessages,
                                                           bool result)
        {
            var model = GetSignUpModel(email, firstName,
                                             lastName, organizationName, password, languageUid,
                                             languageName, isTermsAccepted);
            model.IsValid().ShouldBe(result);
            model.IsNotValid().ShouldBe(!result);

            AssertMessages(model.ErrorMessages, errorMessages);
            AssertMessages(model.InputErrorMessages, inputErrorMessages);
        }
    }
}