using System;
using System.Collections;
using NUnit.Framework;
using Shouldly;
using Translation.Common.Models.Requests.Label.LabelTranslation;
using static Translation.Common.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Common.Tests.Models.Requests.Label.LabelTranslation
{
    public class LabelTranslationRevisionReadListRequestTests
    {
        [Test]
        public void LabelTranslationRevisionReadListRequest_Constructor()
        {
            var request = GetLabelTranslationRevisionReadListRequest(CurrentUserId, UidOne);

            request.CurrentUserId.ShouldBe(CurrentUserId);
            request.LabelTranslationUid.ShouldBe(UidOne);
        }

        public static IEnumerable ArgumentTestCases
        {
            get
            {
                yield return new TestCaseData(CurrentUserId, EmptyUid);
            }
        }

        [TestCaseSource(nameof(ArgumentTestCases))]
        public void LabelTranslationRevisionReadListRequest_Argument_Validations(long currentUserId, Guid labelTranslationUid)
        {
            Assert.Throws<ArgumentException>(() => { new LabelTranslationRevisionReadListRequest(currentUserId, labelTranslationUid); });
        }
    }
}