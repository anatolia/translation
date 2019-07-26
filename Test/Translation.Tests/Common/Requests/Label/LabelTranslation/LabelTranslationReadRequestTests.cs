
using System;
using System.Collections;

using NUnit.Framework;
using Shouldly;

using Translation.Common.Models.Requests.Label.LabelTranslation;
using static Translation.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Tests.Common.Requests.Label.LabelTranslation
{
    public class LabelTranslationReadRequestTests
    {
        [Test]
        public void LabelTranslationReadRequest_Constructor()
        {
            var request = GetLabelTranslationReadRequest(CurrentUserId,UidOne);

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
        public void LabelTranslationReadRequest_Argument_Validations(long currentUserId, Guid labelTranslationUid)
        {
            Assert.Throws<ArgumentException>(() => { new LabelTranslationReadRequest(currentUserId, labelTranslationUid); });
        }
    }
}