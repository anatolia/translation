using System;
using System.Collections;
using Microsoft.AspNetCore.Http;

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
            SystemUnderTest = GetLabelUploadFromCSVModel(3);
        }

        [Test]
        public void LabelUploadFromCSVModel_Title()
        {
            Assert.AreEqual(SystemUnderTest.Title, "upload_labels_from_csv_file_title");
        }

        [Test]
        public void LabelUploadFromCSVModel_OrganizationInput()
        {
            AssertHiddenInputModel(SystemUnderTest.OrganizationInput, "OrganizationUid");
        }

        [Test]
        public void LabelUploadFromCSVModel_ProjectInput()
        {
            AssertHiddenInputModel(SystemUnderTest.ProjectInput, "ProjectUid");
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
            SystemUnderTest.OrganizationInput.Value.ShouldBe(SystemUnderTest.OrganizationUid.ToUidString());
            SystemUnderTest.ProjectInput.Value.ShouldBe(SystemUnderTest.ProjectUid.ToUidString());
            SystemUnderTest.ProjectNameInput.Value.ShouldBe(SystemUnderTest.ProjectName);
            SystemUnderTest.UpdateExistedTranslationsInput.Value.ShouldBe(SystemUnderTest.UpdateExistedTranslations);
        }

        public static IEnumerable MessageTestCases
        {
            get
            {
                yield return new TestCaseData(CaseOne,
                                              UidOne, UidTwo, StringOne,
                                              GetCsvFile(2),
                                              null,
                                              null,
                                              true);

                yield return new TestCaseData(CaseTwo,
                                              EmptyUid, EmptyUid, EmptyString,
                                              null,
                                              new[] { "organization_uid_not_valid",
                                                      "project_uid_not_valid",
                                                      "project_name_required" },
                                              new[] { "csv_required_error_message"},
                                              false);

                yield return new TestCaseData(CaseThree,
                                              EmptyUid, EmptyUid, EmptyString,
                                              GetIcon(),
                                              new[] { "organization_uid_not_valid",
                                                      "project_uid_not_valid",
                                                      "project_name_required" },
                                              new[]
                                              {
                                                  "file_is_not_csv_error_message" ,
                                                  "csv_required_error_message"
                                              },
                                              false);
            }
        }

        [TestCaseSource(nameof(MessageTestCases))]
        public void LabelUploadFromCSVModel_InputErrorMessages(string caseName,
                                                               Guid organizationUid, Guid projectUid, string projectName,
                                                               IFormFile csvFile,
                                                               string[] errorMessages,
                                                               string[] inputErrorMessages,
                                                               bool result)
        {
            var model = GetLabelUploadFromCSVModel(organizationUid, projectUid, projectName,
                                                   csvFile);
            model.IsValid().ShouldBe(result);
            model.IsNotValid().ShouldBe(!result);

            AssertMessages(model.ErrorMessages, errorMessages);
            AssertMessages(model.InputErrorMessages, inputErrorMessages);
        }
    }
}
