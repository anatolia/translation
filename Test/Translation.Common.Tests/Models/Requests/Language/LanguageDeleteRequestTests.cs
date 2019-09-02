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
    public class LanguageDeleteRequestTests
    {
        [Test]
        public void LanguageDeleteRequest_Constructor()
        {
            var request = GetLanguageDeleteRequest(CurrentUserId, UidOne);

            request.CurrentUserId.ShouldBe(CurrentUserId);
            request.LanguageUid.ShouldBe(UidOne);
        }

        public static IEnumerable ArgumentTestCases
        {
            get
            {
                yield return new TestCaseData(CurrentUserId, EmptyUid);

            }
        }

        [TestCaseSource(nameof(ArgumentTestCases))]
        public void LanguageDeleteRequest_Argument_Validations(long currentUserId, Guid languageUId)
        {
            Assert.Throws<ArgumentException>(() => { new LanguageDeleteRequest(currentUserId, languageUId); });
        }
    }
}