using System;
using System.Collections;

using NUnit.Framework;
using Shouldly;

using Translation.Common.Models.Requests.Label;
using static Translation.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Tests.Common.Requests.Label
{
    public class LabelGetTranslatedTextRequestTests
    {

        [Test]
        public void LabelGetTranslatedTextRequest_Constructor()
        {
            var request = GetLabelGetTranslatedTextRequest(StringOne, StringTwo, StringThree);
            request.TextToTranslate.ShouldBe(StringOne);
            request.TargetLanguageIsoCode2.ShouldBe(StringTwo);
            request.SourceLanguageIsoCode2.ShouldBe(StringThree);
        }

        public static IEnumerable ArgumentTestCases
        {
            get
            {
                yield return new TestCaseData(StringOne, EmptyString, StringOne);
                yield return new TestCaseData(StringOne, StringOne, EmptyString);
            }
        }

        [TestCaseSource(nameof(ArgumentTestCases))]
        public void LabelGetTranslatedTextRequest_Argument_Validations(string textToTranslate, string targetLanguageIsoCode2, string sourceLanguageIsoCode2)
        {
            Assert.Throws<ArgumentException>(() => { new LabelGetTranslatedTextRequest(CurrentUserId, textToTranslate, targetLanguageIsoCode2, sourceLanguageIsoCode2); });
        }
    }
}