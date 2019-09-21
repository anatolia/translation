using System;
using System.Collections;

using Shouldly;
using NUnit.Framework;

using Translation.Common.Helpers;
using Translation.Client.Web.Models.Admin;

using static Translation.Common.Tests.TestHelpers.FakeModelTestHelper;
using static Translation.Common.Tests.TestHelpers.AssertViewModelTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Client.Web.Unit.Tests.Models.ViewModels.Admin
{
    [TestFixture]
    public class AdminInviteModelTests
    {
        public AdminInviteModel SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            SystemUnderTest = GetAdminInviteModel();
        }

        [Test]
        public void AdminInviteModel_Title()
        {
            Assert.AreEqual(SystemUnderTest.Title, "admin_invite_title");
        }
        
        [Test]
        public void AdminInviteModel_Organization()
        {
            AssertHiddenInputModel(SystemUnderTest.OrganizationInput, "OrganizationUid");
        }

        [Test]
        public void AdminInviteModel_EmailInput()
        {
            AssertInputModel(SystemUnderTest.EmailInput, "Email", "email", true);
        }

        [Test]
        public void AdminInviteModel_FirstNameInput()
        {
            AssertInputModel(SystemUnderTest.FirstNameInput, "FirstName", "first_name", true);
        }

        [Test]
        public void AdminInviteModel_LastNameInput()
        {
            AssertInputModel(SystemUnderTest.LastNameInput, "LastName", "last_name", true);
        }

        [Test]
        public void AdminInviteModel_SetInputModelValues()
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
                                              new[] { "organization_uid_is_not_valid"
                                              },
                                              new[] { "email_is_not_valid_error_message",
                                                      "first_name_required_error_message",
                                                      "last_name_required_error_message"
                                              },
                                              false);

            }
        }

        [TestCaseSource(nameof(MessageTestCases))]
        public void AdminInviteModel_InputErrorMessages(string caseName,
                                                           Guid organizationUid, string email, string firstName,
                                                           string lastName,
                                                           string[] errorMessages,
                                                           string[] inputErrorMessages,
                                                           bool result)
        {
            var model = GetAdminInviteModel(organizationUid, email, firstName,
                                             lastName);
            model.IsValid().ShouldBe(result);
            model.IsNotValid().ShouldBe(!result);

            AssertMessages(model.ErrorMessages, errorMessages);
            AssertMessages(model.InputErrorMessages, inputErrorMessages);
        }
    }
}