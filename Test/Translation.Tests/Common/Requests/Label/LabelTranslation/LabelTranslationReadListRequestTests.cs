using System;
using System.Collections;

using NUnit.Framework;
using Shouldly;

using Translation.Common.Models.Requests.Label.LabelTranslation;
using static Translation.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Tests.Common.Requests.Label.LabelTranslation
{
    public class LabelTranslationReadListRequestTests
    {
        [Test]
        public void LabelTranslationReadListRequest_Constructor()
        {
            var request = GetLabelTranslationReadListRequest(CurrentUserId, UidOne);

            request.CurrentUserId.ShouldBe(CurrentUserId);
            request.LabelUid.ShouldBe(UidOne);
        }

        public static IEnumerable ArgumentTestCases
        {
            get
            {
                yield return new TestCaseData(CurrentUserId, EmptyUid);

            }
        }

        [TestCaseSource(nameof(ArgumentTestCases))]
        public void LabelTranslationReadListRequest_Argument_Validations(long currentUserId, Guid labelUid)
        {
            Assert.Throws<ArgumentException>(() => { new LabelTranslationReadListRequest(currentUserId, labelUid); });
        }
    }
}