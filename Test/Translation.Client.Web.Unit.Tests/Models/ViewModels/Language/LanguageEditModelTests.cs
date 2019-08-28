using System.Collections;

using Microsoft.AspNetCore.Http;
using NUnit.Framework;
using Shouldly;

using Translation.Client.Web.Models.Language;
using static Translation.Common.Tests.TestHelpers.FakeModelTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeEntityTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;
using static Translation.Common.Tests.TestHelpers.AssertViewModelTestHelper;

namespace Translation.Client.Web.Unit.Tests.Models.ViewModels.Language
{
    [TestFixture]
    public class LanguageEditModelTests
    {
        public LanguageEditModel SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            SystemUnderTest = GetLanguageEditModel();
        }

        [Test]
        public void LanguageEditModel_Title()
        {
            Assert.AreEqual(SystemUnderTest.Title, "language_edit_title");
        }

        [Test]
        public void LanguageEditModel_OrganizationInput()
        {
            AssertInputModel(SystemUnderTest.NameInput, "Name", "name", true);
        }

        [Test]
        public void LanguageEditModel_ProjectInput()
        {
            AssertInputModel(SystemUnderTest.OriginalNameInput, "OriginalName", "original_name", true);
        }

        [Test]
        public void LanguageEditModel_IsoCode2Input()
        {
            AssertShortInputModel(SystemUnderTest.IsoCode2Input, "IsoCode2", "iso_code_2_character", true);
        }

        [Test]
        public void LanguageEditModel_IsoCode3Input()
        {
            AssertShortInputModel(SystemUnderTest.IsoCode3Input, "IsoCode3", "iso_code_3_character", true);
        }

        [Test]
        public void LanguageEditModel_IconInput()
        {
            AssertFileInputModel(SystemUnderTest.IconInput, "Icon", "icon", false);
        }

        [Test]
        public void LanguageEditModel_DescriptionInput()
        {
            AssertLongInputModel(SystemUnderTest.DescriptionInput, "Description", "description");
        }

        [Test]
        public void IntegrationEditModel_SetInputModelValues()
        {
            // arrange

            // act
            SystemUnderTest.SetInputModelValues();

            // assert
            SystemUnderTest.NameInput.Value.ShouldBe(SystemUnderTest.Name);
            SystemUnderTest.OriginalNameInput.Value.ShouldBe(SystemUnderTest.OriginalName);
            SystemUnderTest.IsoCode2Input.Value.ShouldBe(SystemUnderTest.IsoCode2);
            SystemUnderTest.IsoCode3Input.Value.ShouldBe(SystemUnderTest.IsoCode3);
            SystemUnderTest.DescriptionInput.Value.ShouldBe(SystemUnderTest.Description);
        }
        public static IEnumerable MessageTestCases
        {
            get
            {
                yield return new TestCaseData(CaseOne,
                                              StringOne, StringTwo, IsoCode2One,
                                              IsoCode3One, GetIcon(),
                                              null,
                                              null,
                                              true);

                yield return new TestCaseData(CaseTwo,
                                              EmptyString, EmptyString, StringOne,
                                              StringOne, null,
                                              null,
                                              new[] { "language_name_required_error_message",
                                                      "language_original_name_required_error_message",
                                                      "iso_code_2_must_be_2_character",
                                                      "iso_code_3_must_be_3_character",
                                                      "icon_required_error_message"
                                              },
                                              false);
                yield return new TestCaseData(CaseThree,
                                              EmptyString, EmptyString, StringOne,
                                              StringOne, GetInvalidIcon(),
                                              null,
                                              new[] { "language_name_required_error_message",
                                                      "language_original_name_required_error_message",
                                                      "iso_code_2_must_be_2_character",
                                                      "iso_code_3_must_be_3_character",
                                                      "icon_file_type_error_message"
                                              },
                                              false);
                yield return new TestCaseData(CaseFour,
                                              EmptyString, EmptyString, EmptyString,
                                              EmptyString, null,
                                              null,
                                              new[] { "language_name_required_error_message",
                                                      "language_original_name_required_error_message",
                                                      "iso_code_2_required_error_message",
                                                      "iso_code_3_required_error_message",
                                                      "icon_required_error_message"
                                              },
                                              false);
            }
        }

        [TestCaseSource(nameof(MessageTestCases))]
        public void LanguageEditModel_InputErrorMessages(string caseName,
                                                           string name, string originalName, string isoCode2,
                                                           string isoCode3, IFormFile icon,
                                                           string[] errorMessages,
                                                           string[] inputErrorMessages,
                                                           bool result)
        {
            var model = GetLanguageEditModel(name, originalName, isoCode2, isoCode3, icon);
            model.IsValid().ShouldBe(result);
            model.IsNotValid().ShouldBe(!result);

            AssertMessages(model.ErrorMessages, errorMessages);
            AssertMessages(model.InputErrorMessages, inputErrorMessages);
        }
    }
}
