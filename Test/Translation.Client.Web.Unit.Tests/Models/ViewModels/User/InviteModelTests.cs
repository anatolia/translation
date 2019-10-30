using System;
using System.Collections;

using NUnit.Framework;
using Shouldly;

using Translation.Client.Web.Models.User;
using Translation.Common.Helpers;
using static Translation.Client.Web.Unit.Tests.TestHelpers.FakeModelTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;
using static Translation.Client.Web.Unit.Tests.TestHelpers.AssertViewModelTestHelper;

namespace Translation.Client.Web.Unit.Tests.Models.ViewModels.User
{
    [TestFixture]
    public class InviteModelTests
    {
        public InviteModel SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            SystemUnderTest = GetInviteModel();
        }

        [Test]
        public void InviteModel_Title()
        {
            Assert.AreEqual(SystemUnderTest.Title, "user_invite_title");
        }

        [Test]
        public void InviteModel_EmailInput()
        {
            AssertInputModel(SystemUnderTest.EmailInput, "Email", "email", true);
        }

        [Test]
        public void InviteModel_FirstNameInput()
        {
            AssertInputModel(SystemUnderTest.FirstNameInput, "FirstName", "first_name", true);
        }

        [Test]
        public void InviteModel_LastNameInput()
        {
            AssertInputModel(SystemUnderTest.LastNameInput, "LastName", "last_name", true);
        }

        [Test]
        public void InviteModel_SetInputModelValues()
        {
            // arrange

            // act
            SystemUnderTest.SetInputModelValues();

            // assert
            SystemUnderTest.OrganizationInput.Value.ShouldBe(SystemUnderTest.OrganizationUid.ToUidString());
            SystemUnderTest.EmailInput.Value.ShouldBe(SystemUnderTest.Email);
            SystemUnderTest.FirstNameInput.Value.ShouldBe(SystemUnderTest.FirstName);
            SystemUnderTest.LastNameInput.Value.ShouldBe(SystemUnderTest.LastName);
        }

        public static IEnumerable MessageTestCases
        {
            get
            {
                yield return new TestCaseData(CaseOne,
                                              UidOne, EmailOne, StringOne,
                                              StringTwo,
                                              null,
                                              null,
                                              true);

                yield return new TestCaseData(CaseTwo,
                                              EmptyUid, EmptyString, EmptyString,
                                              EmptyString,
                                              new[] { "organization_uid_not_valid" },
                                              new[] { "email_required_error_message",
                                                      "email_is_not_valid_error_message",
                                                      "first_name_required_error_message",
                                                      "last_name_required_error_message"
                                              },
                                              false);
            }
        }

        [TestCaseSource(nameof(MessageTestCases))]
        public void InviteModel_InputErrorMessages(string caseName,
                                                   Guid organizationUid, string email, string firstName,
                                                   string lastName,
                                                   string[] errorMessages,
                                                   string[] inputErrorMessages,
                                                   bool result)
        {
            var model = GetInviteModel(organizationUid, email, firstName, lastName);
            model.IsValid().ShouldBe(result);
            model.IsNotValid().ShouldBe(!result);

            AssertMessages(model.ErrorMessages, errorMessages);
            AssertMessages(model.InputErrorMessages, inputErrorMessages);
        }
    }
}