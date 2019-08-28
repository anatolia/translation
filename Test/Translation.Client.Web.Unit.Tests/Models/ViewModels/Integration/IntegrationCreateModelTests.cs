using System;
using System.Collections;

using NUnit.Framework;
using Shouldly;

using Translation.Client.Web.Models.Integration;
using Translation.Common.Helpers;
using static Translation.Common.Tests.TestHelpers.FakeModelTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;
using static Translation.Common.Tests.TestHelpers.AssertViewModelTestHelper;

namespace Translation.Client.Web.Unit.Tests.Models.ViewModels.Integration
{
    [TestFixture]
    public class IntegrationCreateModelTests
    {
        public IntegrationCreateModel SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            SystemUnderTest = GetIntegrationCreateModel();
        }

        [Test]
        public void IntegrationCreateModel_Title()
        {
            Assert.AreEqual(SystemUnderTest.Title, "integration_create_title");
        }

        [Test]
        public void IntegrationCreateModel_OrganizationInput()
        {
            AssertHiddenInputModel(SystemUnderTest.OrganizationInput, "OrganizationUid");
        }

        [Test]
        public void IntegrationCreateModel_NameInput()
        {
            AssertInputModel(SystemUnderTest.NameInput, "Name", "name", true);
        }

        [Test]
        public void IntegrationCreateModel_DescriptionInput()
        {
            AssertInputModel(SystemUnderTest.DescriptionInput, "Description", "description");
        }

        [Test]
        public void IntegrationCreateModel_SetInputModelValues()
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
                                              new[] { "integration_name_required_error_message" },
                                              false);
            }
        }

        [TestCaseSource(nameof(MessageTestCases))]
        public void IntegrationCreateModel_InputErrorMessages(string caseName,
                                                              Guid organizationUid, string name,
                                                              string[] errorMessages,
                                                              string[] inputErrorMessages,
                                                              bool result)
        {
            var model = GetIntegrationCreateModel(organizationUid, name);
            model.IsValid().ShouldBe(result);
            model.IsNotValid().ShouldBe(!result);

            AssertMessages(model.ErrorMessages, errorMessages);
            AssertMessages(model.InputErrorMessages, inputErrorMessages);
        }
    }
}
