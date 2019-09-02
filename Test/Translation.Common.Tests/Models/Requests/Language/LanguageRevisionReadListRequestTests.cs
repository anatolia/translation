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
    public class LanguageRevisionReadListRequestTests
    {
        [Test]
        public void LanguageRevisionReadListRequest_Constructor()
        {
            var request = GetLanguageRevisionReadListRequest(CurrentUserId,UidOne);
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
        public void LanguageRevisionReadListRequest_Argument_Validations(long currentUserId, Guid languageUid)
        {
            Assert.Throws<ArgumentException>(() => { new LanguageRevisionReadListRequest(currentUserId, languageUid); });
        }
    }
}