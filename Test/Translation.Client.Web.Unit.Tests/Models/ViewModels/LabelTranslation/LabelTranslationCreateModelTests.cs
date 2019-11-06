using System;
using System.Collections;

using NUnit.Framework;
using Shouldly;

using Translation.Client.Web.Models.LabelTranslation;
using Translation.Common.Helpers;
using static Translation.Client.Web.Unit.Tests.TestHelpers.FakeModelTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;
using static Translation.Client.Web.Unit.Tests.TestHelpers.AssertViewModelTestHelper;

namespace Translation.Client.Web.Unit.Tests.Models.ViewModels.LabelTranslation
{
    [TestFixture]
    public class LabelTranslationCreateModelTests
    {
        public LabelTranslationCreateModel SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            SystemUnderTest = GetLabelTranslationCreateModel();
        }

        [Test]
        public void LabelTranslationCreateModel_Title()
        {
            Assert.AreEqual(SystemUnderTest.Title, "label_translation_create_title");
        }

        [Test]
        public void LabelTranslationCreateModel_OrganizationInput()
        {
            AssertHiddenInputModel(SystemUnderTest.OrganizationInput, "OrganizationUid");
        }

        [Test]
        public void LabelTranslationCreateModel_ProjectInput()
        {
            AssertHiddenInputModel(SystemUnderTest.ProjectInput, "ProjectUid");
        }

        [Test]
        public void LabelTranslationCreateModel_ProjectNameInput()
        {
            AssertHiddenInputModel(SystemUnderTest.ProjectNameInput, "ProjectName");
        }

        [Test]
        public void LabelCreateModel_SetInputModelValues()
        {
            // arrange

            // act
            SystemUnderTest.SetInputModelValues();

            // assert
            SystemUnderTest.OrganizationInput.Value.ShouldBe(SystemUnderTest.OrganizationUid.ToUidString());
            SystemUnderTest.ProjectInput.Value.ShouldBe(SystemUnderTest.ProjectUid.ToUidString());
            SystemUnderTest.ProjectNameInput.Value.ShouldBe(SystemUnderTest.ProjectName);
            SystemUnderTest.LabelInput.Value.ShouldBe(SystemUnderTest.LabelUid.ToUidString());
            SystemUnderTest.LabelKeyInput.Value.ShouldBe(SystemUnderTest.LabelKey);
            SystemUnderTest.LanguageInput.Value.ShouldBe(SystemUnderTest.LanguageUid.ToUidString());
            SystemUnderTest.LanguageInput.Text.ShouldBe(SystemUnderTest.LanguageName);
            SystemUnderTest.LabelTranslationInput.Value.ShouldBe(SystemUnderTest.LabelTranslation);
        }

        public static IEnumerable MessageTestCases
        {
            get
            {
                yield return new TestCaseData(CaseOne,
                                              UidOne, UidTwo, StringOne,
                                              UidThree, StringTwo, UidThree,
                                              StringThree,
                                              null,
                                              null,
                                              true);

                yield return new TestCaseData(CaseTwo,
                                              EmptyUid, EmptyUid, EmptyString,
                                              EmptyUid, EmptyString, EmptyUid,
                                              EmptyString,
                                              new[] { "organization_uid_not_valid",
                                                      "project_uid_not_valid",
                                                      "project_name_not_valid",
                                                      "label_uid_not_valid",
                                                      "label_key_not_valid" },
                                              new[] { "language_uid_not_valid",
                                                      "label_translation_required_error_message" },
                                              false);
            }
        }

        [TestCaseSource(nameof(MessageTestCases))]
        public void LabelTranslationCreateModel_InputErrorMessages(string caseName,
                                                        Guid organizationUid, Guid projectUid, string projectName,
                                                        Guid labelUid, string labelKey, Guid languageUid,
                                                        string LabelTranslation,
                                                        string[] errorMessages,
                                                        string[] inputErrorMessages,
                                                        bool result)
        {
            var model = GetLabelTranslationCreateModel(organizationUid, projectUid, projectName,
                                                       labelUid, labelKey, languageUid,
                                                       LabelTranslation);
            model.IsValid().ShouldBe(result);
            model.IsNotValid().ShouldBe(!result);

            AssertMessages(model.ErrorMessages, errorMessages);
            AssertMessages(model.InputErrorMessages, inputErrorMessages);
        }
    }
}
