using System;
using System.Collections;
using NUnit.Framework;
using Shouldly;
using Translation.Common.Models.Requests.Label.LabelTranslation;
using static Translation.Common.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Common.Tests.Models.Requests.Label.LabelTranslation
{
    public class LabelTranslationEditRequestTests
    {
        [Test]
        public void LabelTranslationEditRequest_Constructor()
        {
            var request = GetLabelTranslationEditRequest(CurrentUserId,UidOne,UidTwo,StringOne);

            request.CurrentUserId.ShouldBe(CurrentUserId);
            request.OrganizationUid.ShouldBe(UidOne);
            request.LabelTranslationUid.ShouldBe(UidTwo);
            request.NewTranslation.ShouldBe(StringOne);
         
        }

        public static IEnumerable ArgumentTestCases
        {
            get
            {
                yield return new TestCaseData(CurrentUserId, EmptyUid, UidTwo, StringOne);
                yield return new TestCaseData(CurrentUserId, UidOne, EmptyUid, StringOne);
                yield return new TestCaseData(CurrentUserId, UidOne, UidTwo, EmptyString);
            }
        }

        [TestCaseSource(nameof(ArgumentTestCases))]
        public void LabelTranslationEditRequest_Argument_Validations(long currentUserId, Guid organizationUid, Guid labelTranslationUid,
            string newTranslation)
        {
            Assert.Throws<ArgumentException>(() => { new LabelTranslationEditRequest(currentUserId, organizationUid, labelTranslationUid, newTranslation); });
        }
    }
}