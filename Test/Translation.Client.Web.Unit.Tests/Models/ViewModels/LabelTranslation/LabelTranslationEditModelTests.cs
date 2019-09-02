using System;
using System.Collections;

using NUnit.Framework;
using Shouldly;

using Translation.Client.Web.Models.LabelTranslation;
using Translation.Common.Helpers;
using static Translation.Common.Tests.TestHelpers.FakeModelTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;
using static Translation.Common.Tests.TestHelpers.AssertViewModelTestHelper;

namespace Translation.Client.Web.Unit.Tests.Models.ViewModels.LabelTranslation
{
    [TestFixture]
    public class LabelTranslationEditModelTests
    {
        public LabelTranslationEditModel SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            SystemUnderTest = GetLabelTranslationEditModel();
        }

        [Test]
        public void LabelTranslationEditModel_Title()
        {
            Assert.AreEqual(SystemUnderTest.Title, "label_translation_edit_title");
        }

        [Test]
        public void LabelTranslationEditModel_OrganizationInput()
        {
            AssertHiddenInputModel(SystemUnderTest.OrganizationInput, "OrganizationUid");
        }

        [Test]
        public void LabelTranslationEditModel_LabelTranslationInput()
        {
            AssertHiddenInputModel(SystemUnderTest.LabelTranslationInput, "LabelTranslationUid");
        }

        [Test]
        public void LabelEditModel_SetInputModelValues()
        {
            // arrange

            // act
            SystemUnderTest.SetInputModelValues();

            // assert
            SystemUnderTest.OrganizationInput.Value.ShouldBe(SystemUnderTest.OrganizationUid.ToUidString());
            SystemUnderTest.LabelInput.Value.ShouldBe(SystemUnderTest.LabelUid.ToUidString());
            SystemUnderTest.LabelTranslationInput.Value.ShouldBe(SystemUnderTest.LabelTranslationUid.ToUidString());
            SystemUnderTest.TranslationInput.Value.ShouldBe(SystemUnderTest.Translation);

        }

        public static IEnumerable MessageTestCases
        {
            get
            {
                yield return new TestCaseData(CaseOne,
                                              UidOne, UidTwo, UidThree, StringOne,
                                              StringTwo, StringThree, StringFour,
                                              null,
                                              null,
                                              true);

                yield return new TestCaseData(CaseTwo,
                                              EmptyUid, EmptyUid, EmptyUid, EmptyString,
                                              EmptyString, EmptyString, EmptyString,
                                              new[] { "organization_uid_not_valid",
                                                      "label_uid_not_valid",
                                                      "label_translation_uid_not_valid"},
                                              new[] { "translation_required_error_message" },
                                              false);
            }
        }

        [TestCaseSource(nameof(MessageTestCases))]
        public void LabelTranslationEditModel_InputErrorMessages(string caseName,
                                                                 Guid organizationUid, Guid labelUid, Guid labelTranslationUid, string labelKey,
                                                                 string languageName, string languageIconUrl, string translation,
                                                                 string[] errorMessages,
                                                                 string[] inputErrorMessages,
                                                                 bool result)
        {
            var model = GetLabelTranslationEditModel(organizationUid, labelUid, labelTranslationUid, labelKey,
                                                     languageName, languageIconUrl, translation);
            model.IsValid().ShouldBe(result);
            model.IsNotValid().ShouldBe(!result);

            AssertMessages(model.ErrorMessages, errorMessages);
            AssertMessages(model.InputErrorMessages, inputErrorMessages);
        }
    }
}
