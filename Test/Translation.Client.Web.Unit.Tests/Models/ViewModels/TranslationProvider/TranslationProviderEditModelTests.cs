using System;
using System.Collections;

using NUnit.Framework;
using Shouldly;
using StandardUtils.Helpers;

using Translation.Client.Web.Models.TranslationProvider;

using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;
using static Translation.Client.Web.Unit.Tests.TestHelpers.AssertViewModelTestHelper;
using static Translation.Client.Web.Unit.Tests.TestHelpers.FakeModelTestHelper;

namespace Translation.Client.Web.Unit.Tests.Models.ViewModels.TranslationProvider
{
    [TestFixture]
    public class TranslationProviderEditModelTests
    {
        public TranslationProviderEditModel SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            SystemUnderTest = GetTranslationProviderEditModel();
        }

        [Test]
        public void TranslationProviderEditModel_Title()
        {
            Assert.AreEqual(SystemUnderTest.Title, "translation_provider_edit_title");
        }

        [Test]
        public void SignUpModel_TranslationProviderInput()
        {
            AssertHiddenInputModel(SystemUnderTest.TranslationProviderInput, "TranslationProviderUid");
        }

        [Test]
        public void SignUpModel_TranslationProviderNameInput()
        {
            AssertHiddenInputModel(SystemUnderTest.TranslationProviderNameInput, "Name");
        }

        [Test]
        public void SignUpModel_DescriptionInput()
        {
            AssertLongInputModel(SystemUnderTest.DescriptionInput, "Description", "description");
        }

        [Test]
        public void SignUpModel_ValueInput()
        {
            AssertInputModel(SystemUnderTest.ValueInput, "Value", "value");
        }

        [Test]
        public void TranslationProviderEditModel_SetInputModelValues()
        {
            // arrange

            // act
            SystemUnderTest.SetInputModelValues();

            // assert;
            SystemUnderTest.TranslationProviderInput.Value.ShouldBe(SystemUnderTest.TranslationProviderUid.ToUidString());
            SystemUnderTest.TranslationProviderNameInput.Value.ShouldBe(SystemUnderTest.Name);
            SystemUnderTest.DescriptionInput.Value.ShouldBe(SystemUnderTest.Description);
            SystemUnderTest.ValueInput.Value.ShouldBe(SystemUnderTest.Value);
        }

        public static IEnumerable MessageTestCases
        {
            get
            {
                yield return new TestCaseData(CaseOne,
                                              UidOne, StringOne, StringTwo, StringThree, HttpUrl,
                                              null,
                                              null,
                                              true);

                yield return new TestCaseData(CaseTwo,
                                              EmptyUid, EmptyString, EmptyString, EmptyString, EmptyString,
                                              new[] { "translation_provider_uid_is_not_valid" },
                                              new[] { "translation_provider_name_required_error_message",
                                                      "translation_provider_api_key_not_valid"},
                                              false); 

                yield return new TestCaseData(CaseThree,
                                              EmptyUid, EmptyString, GoogleName, EmptyString, EmptyString,
                                              new[] { "translation_provider_uid_is_not_valid" },
                                              new[] { "translation_provider_api_key_not_valid",
                                                      "google_api_must_place_between_{_}",
                                                      "google_api_Informations_format_not_valid"},
                                              false); 

                yield return new TestCaseData(CaseFour,
                                              EmptyUid, EmptyString, YandexName, EmptyString, EmptyString,
                                              new[] { "translation_provider_uid_is_not_valid" },
                                              new[] { "translation_provider_api_key_not_valid",
                                                      "yandex_api_key_must_start_with_trns" },
                                              false);
            }
        }

        [TestCaseSource(nameof(MessageTestCases))]
        public void TranslationProviderEditModel_InputErrorMessages(string caseName,
                                                       Guid userUid, string value, string name, string description, string googleDescriptionLink,
                                                       string[] errorMessages,
                                                       string[] inputErrorMessages,
                                                       bool result)
        {
            var model = GetTranslationProviderEditModel(userUid, value, name, description, googleDescriptionLink);
            model.IsValid().ShouldBe(result);
            model.IsNotValid().ShouldBe(!result);

            AssertMessages(model.ErrorMessages, errorMessages);
            AssertMessages(model.InputErrorMessages, inputErrorMessages);
        }
    }
}
