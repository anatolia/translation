using System;
using System.Collections;

using NUnit.Framework;
using Shouldly;

using Translation.Client.Web.Models.Label;
using Translation.Common.Helpers;
using static Translation.Tests.TestHelpers.FakeModelTestHelper;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;
using static Translation.Tests.TestHelpers.AssertViewModelTestHelper;

namespace Translation.Tests.Client.Models.ViewModels.Label
{
    [TestFixture]
    public class CreateBulkLabelModelTests
    {
        public CreateBulkLabelModel SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            SystemUnderTest = GetCreateBulkLabelModel();
        }

        [Test]
        public void CreateBulkLabelModel_Title()
        {
            Assert.AreEqual(SystemUnderTest.Title, "create_bulk_label_title");
        }

        [Test]
        public void CreateBulkLabelModel_OrganizationInput()
        {
            AssertHiddenInputModel(SystemUnderTest.OrganizationInput, "OrganizationUid");
        }

        [Test]
        public void CreateBulkLabelModel_ProjectInput()
        {
            AssertHiddenInputModel(SystemUnderTest.ProjectInput, "ProjectUid");
        }

        [Test]
        public void CreateBulkLabelModel_ProjectNameInput()
        {
            AssertHiddenInputModel(SystemUnderTest.ProjectNameInput, "ProjectName");
        }

        [Test]
        public void CreateBulkLabelModel_BulkLabelInput()
        {
            AssertInputModel(SystemUnderTest.BulkLabelInput, "BulkLabelData", "bulk_label_data");
        }

        [Test]
        public void IntegrationCreateModel_SetInputModelValues()
        {
            // arrange

            // act
            SystemUnderTest.SetInputModelValues();

            // assert
            SystemUnderTest.OrganizationInput.Value.ShouldBe(SystemUnderTest.OrganizationUid.ToUidString());
            SystemUnderTest.ProjectInput.Value.ShouldBe(SystemUnderTest.ProjectUid.ToUidString());
            SystemUnderTest.ProjectNameInput.Value.ShouldBe(SystemUnderTest.ProjectName);
            SystemUnderTest.BulkLabelInput.Value.ShouldBe(SystemUnderTest.BulkLabelData);
        }

        public static IEnumerable MessageTestCases
        {
            get
            {
                yield return new TestCaseData(CaseOne,
                                              UidOne, UidTwo, StringOne,
                                              StringTwo,
                                              null,
                                              null,
                                              true);

                yield return new TestCaseData(CaseTwo,
                                              EmptyUid, EmptyUid, EmptyString,
                                              EmptyString,
                                              new[] { "organization_uid_is_not_valid",
                                                      "project_uid_is_not_valid",
                                                      "project_name_required" },
                                              new[] { "bulk_label_data_required_error_message" },
                                              false);
            }
        }

        [TestCaseSource(nameof(MessageTestCases))]
        public void CreateBulkLabelModel_InputErrorMessages(string caseName,
                                                              Guid organizationUid, Guid projectUid, string projectName,
                                                              string bulkLabelData,
                                                              string[] errorMessages,
                                                              string[] inputErrorMessages,
                                                              bool result)
        {
            var model = GetCreateBulkLabelModel(organizationUid, projectUid, projectName, 
                                                bulkLabelData);
            model.IsValid().ShouldBe(result);
            model.IsNotValid().ShouldBe(!result);

            AssertMessages(model.ErrorMessages, errorMessages);
            AssertMessages(model.InputErrorMessages, inputErrorMessages);
        }
    }
}
