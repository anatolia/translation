using System;
using System.Collections;

using NUnit.Framework;
using Shouldly;

using Translation.Common.Models.Requests.Label.LabelTranslation;
using static Translation.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Tests.Common.Requests.Label.LabelTranslation
{
    public class LabelTranslationDeleteRequestTests
    {
        [Test]
        public void LabelTranslationDeleteRequest_Constructor()
        {
            var request = GetLabelTranslationDeleteRequest(CurrentUserId,OrganizationOneUid,UidOne);

            request.CurrentUserId.ShouldBe(CurrentUserId);
            request.LabelTranslationUid.ShouldBe(UidOne);
            request.OrganizationUid.ShouldBe(OrganizationOneUid);
        }

        public static IEnumerable ArgumentTestCases
        {
            get
            {
                yield return new TestCaseData(CurrentUserId, EmptyUid,UidOne);
                yield return new TestCaseData(CurrentUserId, OrganizationOneUid,EmptyUid);
            }
        }

        [TestCaseSource(nameof(ArgumentTestCases))]
        public void LabelTranslationDeleteRequest_Argument_Validations(long currentUserId, Guid organizationUid, Guid labelTranslationUid)
        {
            Assert.Throws<ArgumentException>(() => { new LabelTranslationDeleteRequest(currentUserId, organizationUid, labelTranslationUid); });
        }
    }
}