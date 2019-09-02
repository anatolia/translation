using System;
using System.Collections;
using NUnit.Framework;
using Shouldly;
using Translation.Common.Models.Requests.Language;
using static Translation.Common.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Common.Tests.Models.Requests.Language
{
    [TestFixture]
    public class LanguageEditRequestTests
    {

        [Test]
        public void LanguageEditRequest_Constructor()
        {

            var request = GetLanguageEditRequest(CurrentUserId, UidOne, StringOne,
                StringTwo, IsoCode2One, IsoCode3One,
                StringThree, StringFour);

            request.CurrentUserId.ShouldBe(CurrentUserId);
            request.LanguageUid.ShouldBe(UidOne);
            request.Name.ShouldBe(StringOne);
            request.OriginalName.ShouldBe(StringTwo);
            request.IsoCode2.ShouldBe(IsoCode2One);
            request.IsoCode3.ShouldBe(IsoCode3One);
            request.Icon.ShouldBe(StringThree);
            request.Description.ShouldBe(StringFour);
        }

        public static IEnumerable ArgumentTestCases
        {
            get
            {
                yield return new TestCaseData(CurrentUserId, EmptyUid, StringOne, StringOne, IsoCode2One, IsoCode3One, StringThree, StringFour);
                yield return new TestCaseData(CurrentUserId, UidOne, EmptyString, StringOne, IsoCode2One, IsoCode3One, StringThree, StringFour);
                yield return new TestCaseData(CurrentUserId, UidOne, StringOne, StringOne, EmptyString, IsoCode3One, StringThree, StringFour);
                yield return new TestCaseData(CurrentUserId, UidOne, StringOne, StringOne, IsoCode2One, EmptyString, StringThree, StringFour);
                yield return new TestCaseData(CurrentUserId, UidOne, StringOne, StringOne, IsoCode2One, IsoCode3One, EmptyString, StringFour);

            }
        }

        [TestCaseSource(nameof(ArgumentTestCases))]
        public void LanguageEditRequest_Argument_Validations(long currentUserId, Guid languageUid, string name,
            string originalName, string isoCode2, string isoCode3,
            string icon, string description)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                new LanguageEditRequest(currentUserId, languageUid, name, originalName, isoCode2, isoCode3, icon, description);
            });
        }
    }
}