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
    public class DemandPasswordResetModelTests
    {
        public DemandPasswordResetModel SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            SystemUnderTest = GetDemandPasswordResetModel();
        }

        [Test]
        public void DemandPasswordResetModel_Title()
        {
            Assert.AreEqual(SystemUnderTest.Title, "demand_password_reset_title");
        }

        [Test]
        public void DemandPasswordResetModel_EmailInputModel()
        {
            AssertInputModel(SystemUnderTest.EmailInput, "Email", "email", true);
        }

        [Test]
        public void ChangePasswordModel_SetInputModelValues()
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
                                              EmailOne,
                                              null,
                                              null,
                                              true);

                yield return new TestCaseData(CaseTwo,
                                              EmptyString,
                                              null,
                                              new[] { "email_required_error_message",
                                                      "email_is_not_valid_error_message"},
                                              false);

                yield return new TestCaseData(CaseTwo,
                                              StringOne,
                                              null,
                                              new[] { "email_is_not_valid_error_message" },
                                              false);
            }
        }

        [TestCaseSource(nameof(MessageTestCases))]
        public void DemandPasswordResetModel_InputErrorMessages(string caseName,
                                                                string email,
                                                                string[] errorMessages,
                                                                string[] inputErrorMessages,
                                                                bool result)
        {
            var model = GetDemandPasswordResetModel(email);
            model.IsValid().ShouldBe(result);
            model.IsNotValid().ShouldBe(!result);

            AssertMessages(model.ErrorMessages, errorMessages);
            AssertMessages(model.InputErrorMessages, inputErrorMessages);
        }
    }
}