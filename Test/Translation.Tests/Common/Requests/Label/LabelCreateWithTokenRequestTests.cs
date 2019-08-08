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
            var request = GetLabelCreateWithTokenRequest(UidOne, UidTwo, StringOne,StringArray);
            request.Token.ShouldBe(UidOne);
            request.ProjectUid.ShouldBe(UidTwo);
            request.LabelKey.ShouldBe(StringOne);
            request.LanguageNames.ShouldBe(StringArray);

        }

        public static IEnumerable ArgumentTestCases
        {
            get
            {
                yield return new TestCaseData(EmptyUid, UidOne, StringOne, StringArray);
                yield return new TestCaseData(UidOne, EmptyUid, StringOne, StringArray);
                yield return new TestCaseData(UidOne, UidOne, EmptyString, StringArray);
            }
        }

        [TestCaseSource(nameof(ArgumentTestCases))]
        public void LabelCreateWithTokenRequest_Argument_Validations(Guid token, Guid projectUid, string labelKey, string[] languageNames)
        {
            Assert.Throws<ArgumentException>(() => { new LabelCreateWithTokenRequest(token, projectUid, labelKey, languageNames); });
        }
    }
}