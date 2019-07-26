using System;
using System.Collections;
using Microsoft.AspNetCore.Http;
using NUnit.Framework;
using Shouldly;

using Translation.Client.Web.Models.LabelTranslation;
using Translation.Common.Helpers;
using static Translation.Tests.TestHelpers.FakeModelTestHelper;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;
using static Translation.Tests.TestHelpers.FakeEntityTestHelper;
using static Translation.Tests.TestHelpers.AssertViewModelTestHelper;

namespace Translation.Tests.Client.Models.ViewModels.LabelTranslation
{
    [TestFixture]
    public class UploadLabelTranslationFromCSVFileModelTests
    {
        public UploadLabelTranslationFromCSVFileModel SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            SystemUnderTest = GetUploadLabelTranslationFromCSVFileModel(2);
        }

        [Test]
        public void UploadLabelTranslationFromCSVFileModel_Title()
        {
            Assert.AreEqual(SystemUnderTest.Title, "upload_labels_from_csv_file_title");
        }

        [Test]
        public void UploadLabelTranslationFromCSVFileModel_OrganizationInput()
        {
            AssertHiddenInputModel(SystemUnderTest.OrganizationInput, "OrganizationUid");
        }

        [Test]
        public void LabelTranslationEditModel_LabelInput()
        {
            AssertHiddenInputModel(SystemUnderTest.LabelInput, "LabelUid");
        }

        [Test]
        public void LabelTranslationEditModel_LabelKeyInput()
        {
            AssertHiddenInputModel(SystemUnderTest.LabelKeyInput, "LabelKey");
        }

        [Test]
        public void LabelTranslationEditModel_CSVFileInput()
        {
            AssertFileInputModel(SystemUnderTest.CSVFileInput, "CSVFile", "csv_file", true);
        }

        [Test]
        public void LabelTranslationEditModel_SetInputModelValues()
        {
            // arrange

            // act
            SystemUnderTest.SetInputModelValues();

            // assert
            SystemUnderTest.OrganizationInput.Value.ShouldBe(SystemUnderTest.OrganizationUid.ToUidString());
            SystemUnderTest.LabelInput.Value.ShouldBe(SystemUnderTest.LabelUid.ToUidString());
            SystemUnderTest.LabelKeyInput.Value.ShouldBe(SystemUnderTest.LabelKey);
        }

        public static IEnumerable MessageTestCases
        {
            get
            {
                yield return new TestCaseData(CaseOne,
                                              UidOne, UidTwo, StringOne,
                                              GetCsvFile(10),
                                              null,
                                              null,
                                              true);

                yield return new TestCaseData(CaseTwo,
                                              EmptyUid, EmptyUid, EmptyString,
                                              null,
                                              new[] { "organization_uid_not_valid",
                                                      "label_uid_not_valid",
                                                      "label_key_not_valid" },
                                              new[] { "csv_required_error_message" },
                                              false);

                yield return new TestCaseData(CaseThree,
                                              EmptyUid, EmptyUid, EmptyString,
                                              GetCsvFile(4),
                                              new[] { "organization_uid_not_valid",
                                                                      "label_uid_not_valid",
                                                                      "label_key_not_valid" },
                                              new[] { "csv_required_error_message" },
                                              false);

            }
        }

        [TestCaseSource(nameof(MessageTestCases))]
        public void LabelTranslationEditModel_InputErrorMessages(string caseName,
                                                                 Guid organizationUid, Guid labelUid, string labelKey,
                                                                 IFormFile csvFile,
                                                                 string[] errorMessages,
                                                                 string[] inputErrorMessages,
                                                                 bool result)
        {
            var model = GetUploadLabelTranslationFromCSVFileModel(organizationUid, labelUid, labelKey, 
                                                                  csvFile);
            model.IsValid().ShouldBe(result);
            model.IsNotValid().ShouldBe(!result);

            AssertMessages(model.ErrorMessages, errorMessages);
            AssertMessages(model.InputErrorMessages, inputErrorMessages);
        }
    }
}
