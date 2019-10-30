using System.Collections;
using NUnit.Framework;

using Shouldly;
using Translation.Client.Web.Models.User;
using static Translation.Client.Web.Unit.Tests.TestHelpers.FakeModelTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;
using static Translation.Client.Web.Unit.Tests.TestHelpers.AssertViewModelTestHelper;

namespace Translation.Client.Web.Unit.Tests.Models.ViewModels.User
{
    [TestFixture]
    public class LoginOnModelTests
    {
        public LogOnModel SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            SystemUnderTest = GetLoginOnModel();
        }

        [Test]
        public void ProjectCreateModel_Title()
        {
            Assert.AreEqual(SystemUnderTest.Title, "log_on_title");
        }

        [Test]
        public void LoginOnModel_PasswordInput()
        {
            AssertInputModel(SystemUnderTest.PasswordInput, "Password", "password", true);
        }

        [Test]
        public void LoginOnModel_EmailInput()
        {
            AssertInputModel(SystemUnderTest.EmailInput, "Email", "email", true);
        }

        [Test]
        public void LoginOnModel_SetInputModelValues()
        {
            // arrange

            // act
            SystemUnderTest.SetInputModelValues();

            // assert
            SystemUnderTest.EmailInput.Value.ShouldBe(SystemUnderTest.Email);
        }

        public static IEnumerable MessageTestCases
        {
            get
            {
                yield return new TestCaseData(CaseOne,
                                              EmailOne, PasswordOne,
                                              null,
                                              null,
                                              true);

                yield return new TestCaseData(CaseTwo,
                                              EmptyString, EmptyString,
                                              new[] {
                                                  "email_required_error_message",
                                                  "password_required_error_message",
                                                  "email_is_not_valid_error_message",
                                                  "password_is_not_valid_error_message"
                                              },
                                              new[] {
                                                  "email_required_error_message",
                                                  "password_required_error_message",
                                                  "email_is_not_valid_error_message",
                                                  "password_is_not_valid_error_message"
                                              },
                                              false);
                yield return new TestCaseData(CaseThree,
                                              StringOne, InvalidPassword,
                                              new[] {
                                                  "email_is_not_valid_error_message",
                                                  "password_is_not_valid_error_message",
                                              },
                                              new[] {
                                                  "email_is_not_valid_error_message",
                                                  "password_is_not_valid_error_message",
                                              },
                                              false);
            }
        }

        [TestCaseSource(nameof(MessageTestCases))]
        public void LoginOnModel_InputErrorMessages(string caseName,
                                                    string email, string password,
                                                    string[] errorMessages,
                                                    string[] inputErrorMessages,
                                                    bool result)
        {
            var model = GetLoginOnModel(email, password);
            model.IsValid().ShouldBe(result);
            model.IsNotValid().ShouldBe(!result);

            AssertMessages(model.ErrorMessages, errorMessages);
            AssertMessages(model.InputErrorMessages, inputErrorMessages);
        }
    }
}