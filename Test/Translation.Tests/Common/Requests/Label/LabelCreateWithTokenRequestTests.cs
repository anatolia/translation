using System;
using System.Collections;

using NUnit.Framework;
using Shouldly;

using Translation.Common.Models.Requests.Label;
using static Translation.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Tests.Common.Requests.Label
{
    public class LabelCreateWithTokenRequestTests
    {

        [Test]
        public void LabelCreateWithTokenRequest_Constructor()
        {
            var request = GetLabelCreateWithTokenRequest(UidOne, UidTwo, StringOne,IsoCode2ArrayOne);
            request.Token.ShouldBe(UidOne);
            request.ProjectUid.ShouldBe(UidTwo);
            request.LabelKey.ShouldBe(StringOne);
            request.LanguageIsoCode2s.ShouldBe(IsoCode2ArrayOne);

        }

        public static IEnumerable ArgumentTestCases
        {
            get
            {
                yield return new TestCaseData(EmptyUid, UidOne, StringOne, IsoCode2ArrayOne);
                yield return new TestCaseData(UidOne, EmptyUid, StringOne, IsoCode2ArrayOne);
                yield return new TestCaseData(UidOne, UidOne, EmptyString, IsoCode2ArrayOne);
            }
        }

        [TestCaseSource(nameof(ArgumentTestCases))]
        public void LabelCreateWithTokenRequest_Argument_Validations(Guid token, Guid projectUid, string labelKey, string[] languageNames)
        {
            Assert.Throws<ArgumentException>(() => { new LabelCreateWithTokenRequest(token, projectUid, labelKey, languageNames); });
        }
    }
}