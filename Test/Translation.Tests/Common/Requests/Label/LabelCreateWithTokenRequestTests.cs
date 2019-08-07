using System;
using System.Collections;

using NUnit.Framework;
using Shouldly;

using Translation.Common.Models.Requests.Label;
using static Translation.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;
using static Translation.Tests.TestHelpers.FakeEntityTestHelper;

namespace Translation.Tests.Common.Requests.Label
{
    public class LabelCreateWithTokenRequestTests
    {

        [Test]
        public void LabelCreateWithTokenRequest_Constructor()
        {
            var request = GetLabelCreateWithTokenRequest(UidOne, UidTwo, StringOne, StringTwo);
            request.Token.ShouldBe(UidOne);
            request.ProjectUid.ShouldBe(UidTwo);
            request.LabelKey.ShouldBe(StringOne);
            request.LanguagesIsoCode2Char.ShouldBe(StringTwo);

        }

        public static IEnumerable ArgumentTestCases
        {
            get
            {
                yield return new TestCaseData(EmptyUid, UidOne, StringOne, StringTwo);
                yield return new TestCaseData(UidOne, EmptyUid, StringOne, StringTwo);
                yield return new TestCaseData(UidOne, UidOne, EmptyString, StringTwo);
                yield return new TestCaseData(UidOne, UidOne, StringOne, EmptyString);
            }
        }

        [TestCaseSource(nameof(ArgumentTestCases))]
        public void LabelCreateWithTokenRequest_Argument_Validations(Guid token, Guid projectUid, string labelKey, string LanguagesIsoCode2Char)
        {
            Assert.Throws<ArgumentException>(() => { new LabelCreateWithTokenRequest(token, projectUid, labelKey, LanguagesIsoCode2Char); });
        }
    }
}