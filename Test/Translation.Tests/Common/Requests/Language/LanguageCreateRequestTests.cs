using System;
using System.Collections;

using NUnit.Framework;
using Shouldly;

using Translation.Common.Models.Requests.Language;
using static Translation.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Tests.Common.Requests.Language
{
    [TestFixture]
    public class LanguageCreateRequestTests
    {
        [Test]
        public void LanguageCreateRequest_Constructor()
        {
            var request = GetLanguageCreateRequest(CurrentUserId, StringOne, StringTwo,
                                                   IsoCode2One, IsoCode3One, StringThree,
                                                   StringFour);

            request.CurrentUserId.ShouldBe(CurrentUserId);
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
                yield return new TestCaseData(CurrentUserId, EmptyString, StringTwo, IsoCode2One, IsoCode3One, StringThree, StringFour);
                yield return new TestCaseData(CurrentUserId, StringOne, EmptyString, IsoCode2One, IsoCode3One, StringThree, StringFour);
                yield return new TestCaseData(CurrentUserId, StringOne, StringTwo, EmptyString, IsoCode3One, StringThree, StringFour);
                yield return new TestCaseData(CurrentUserId, StringOne, StringTwo, IsoCode2One, EmptyString, StringThree, StringFour);
                yield return new TestCaseData(CurrentUserId, StringOne, StringTwo, IsoCode2One, IsoCode3One, EmptyString, StringFour);
            }
        }

        [TestCaseSource(nameof(ArgumentTestCases))]
        public void LanguageCreateRequest_Argument_Validations(long currentUserId, string name, string originalName, string isoCode2,
                                                               string isoCode3, string icon, string description)
        {
            Assert.Throws<ArgumentException>(() => { new LanguageCreateRequest(currentUserId, name, originalName, isoCode2, isoCode3, icon, description); });
        }
    }
}