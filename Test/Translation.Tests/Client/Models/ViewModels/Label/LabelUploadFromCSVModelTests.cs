using System;
using System.Collections;

using NUnit.Framework;
using Shouldly;

using Translation.Client.Web.Models.Label;
using Translation.Common.Helpers;
using static Translation.Tests.TestHelpers.FakeModelTestHelper;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;
using static Translation.Tests.TestHelpers.FakeEntityTestHelper;
using static Translation.Tests.TestHelpers.AssertViewModelTestHelper;

namespace Translation.Tests.Client.Models.ViewModels.Label
{
    [TestFixture]
    public class LabelUploadFromCSVModelTests
    {
        public LabelUploadFromCSVModel SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            SystemUnderTest = GetLabelUploadFromCSVModel();
        }

        [Test]
        public void LabelUploadFromCSVModel_Title()
        {
            Assert.AreEqual(SystemUnderTest.Title, "upload_labels_from_csv_file_title");
        }

        [Test]
        public void LabelUploadFromCSVModel_OrganizationUidInput()
        {
            AssertHiddenInputModel(SystemUnderTest.OrganizationUidInput, "OrganizationUid");
        }

        [Test]
        public void LabelUploadFromCSVModel_ProjectUidInput()
        {
            AssertHiddenInputModel(SystemUnderTest.ProjectUidInput, "ProjectUid");
        }

        [Test]
        public void LabelUploadFromCSVModel_ProjectNameInput()
        {
            AssertHiddenInputModel(SystemUnderTest.ProjectNameInput, "ProjectName");
        }

        [Test]
        public void LabelUploadFromCSVModel_CSVFileInput()
        {
            AssertFileInputModel(SystemUnderTest.CSVFileInput, "CSVFile", "csv_file", true);
        }

        [Test]
        public void LabelUploadFromCSVModel_SetInputModelValues()
        {
            // arrange

            // act
            SystemUnderTest.SetInputModelValues();

            // assert
            SystemUnderTest.OrganizationUidInput.Value.ShouldBe(SystemUnderTest.OrganizationUid.ToUidString());
            SystemUnderTest.ProjectUidInput.Value.ShouldBe(SystemUnderTest.ProjectUid.ToUidString());
            SystemUnderTest.ProjectNameInput.Value.ShouldBe(SystemUnderTest.ProjectName);
        }

        public static IEnumerable MessageTestCases
        {
            get
            {
                yield return new TestCaseData(CaseOne,
                                              UidOne, UidTwo, StringOne,
                                              null,
                                              null,
                                              true);

                yield return new TestCaseData(CaseTwo,
                                              EmptyUid, EmptyUid, EmptyString,
                                              new[] { "organization_uid_not_valid",
                                                      "project_uid_not_valid",
                                                      "project_name_required" },
                                              new[] { "csv_required_error_message",
                                                      "file_is_not_csv_error_message" },
                                              false);
            }
        }

        [TestCaseSource(nameof(MessageTestCases))]
        public void LabelUploadFromCSVModel_InputErrorMessages(string caseName,
                                                               Guid organizationUid, Guid projectUid, string projectName,
                                                               string[] errorMessages,
                                                               string[] inputErrorMessages,
                                                               bool result)
        {
            var model = GetLabelUploadFromCSVModel(organizationUid, projectUid, projectName,
                                                   GetUploadLabelCsvTemplateFileThreeValue());
            model.IsValid().ShouldBe(result);
            model.IsNotValid().ShouldBe(!result);

            AssertMessages(model.ErrorMessages, errorMessages);
            AssertMessages(model.InputErrorMessages, inputErrorMessages);
        }
    }
}
