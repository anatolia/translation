using System;
using System.Collections;

using NUnit.Framework;
using Shouldly;

using Translation.Client.Web.Models.Organization;
using Translation.Common.Helpers;
using static Translation.Common.Tests.TestHelpers.FakeModelTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;
using static Translation.Common.Tests.TestHelpers.AssertViewModelTestHelper;

namespace Translation.Client.Web.Unit.Tests.Models.ViewModels.Organization
{
    [TestFixture]
    public class OrganizationEditModelTests
    {
        public OrganizationEditModel SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            SystemUnderTest = GetOrganizationEditModel();
        }

        [Test]
        public void OrganizationEditModel_Title()
        {
            Assert.AreEqual(SystemUnderTest.Title, "organization_edit_title");
        }

        [Test]
        public void OrganizationEditModel_OrganizationInput()
        {
            AssertHiddenInputModel(SystemUnderTest.OrganizationInput, "OrganizationUid");
        }

        [Test]
        public void OrganizationEditModel_NameInput()
        {
            AssertInputModel(SystemUnderTest.NameInput, "Name", "name", true);
        }

        [Test]
        public void OrganizationEditModel_DescriptionInput()
        {
            AssertInputModel(SystemUnderTest.DescriptionInput, "Description", "description");
        }

        [Test]
        public void OrganizationEditModel_SetInputModelValues()
        {
            // arrange

            // act
            SystemUnderTest.SetInputModelValues();

            // assert
            SystemUnderTest.OrganizationInput.Value.ShouldBe(SystemUnderTest.OrganizationUid.ToUidString());
            SystemUnderTest.NameInput.Value.ShouldBe(SystemUnderTest.Name);
            SystemUnderTest.DescriptionInput.Value.ShouldBe(SystemUnderTest.Description);
        }

        public static IEnumerable MessageTestCases
        {
            get
            {
                yield return new TestCaseData(CaseOne,
                                              UidOne, StringOne,
                                              null,
                                              null,
                                              true);

                yield return new TestCaseData(CaseTwo,
                                              EmptyUid, EmptyString,
                                              new[] { "organization_uid_is_not_valid" },
                                              new[] { "organization_name_required_error_message" },
                                              false);
            }
        }

        [TestCaseSource(nameof(MessageTestCases))]
        public void OrganizationEditModel_InputErrorMessages(string caseName,
                                                             Guid organizationUid, string name,
                                                             string[] errorMessages,
                                                             string[] inputErrorMessages,
                                                             bool result)
        {
            var model = GetOrganizationEditModel( organizationUid, name);
            model.IsValid().ShouldBe(result);
            model.IsNotValid().ShouldBe(!result);

            AssertMessages(model.ErrorMessages, errorMessages);
            AssertMessages(model.InputErrorMessages, inputErrorMessages);
        }
    }
}
