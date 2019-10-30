using System;
using System.Collections;
using System.Collections.Generic;

using NUnit.Framework;
using Shouldly;

using Translation.Common.Models.Requests.Label.LabelTranslation;

using static Translation.Common.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Common.Tests.Models.Requests.Label.LabelTranslation
{
    public class LabelTranslationCreateListRequestTests
    {
        [Test]
        public void LabelTranslationCreateListRequest_Constructor()
        {
            var labels = GetTranslationListInfoList();
            var request = GetLabelTranslationCreateListRequest(CurrentUserId, UidOne, UidTwo, BooleanTrue, labels);

            request.CurrentUserId.ShouldBe(CurrentUserId);
            request.OrganizationUid.ShouldBe(UidOne);
            request.LabelUid.ShouldBe(UidTwo);
            request.UpdateExistedTranslations.ShouldBe(BooleanTrue);
            request.LabelTranslations.ShouldBe(labels);
        }

        public static IEnumerable ArgumentTestCases
        {
            get
            {
                yield return new TestCaseData(CurrentUserId, EmptyUid, UidTwo, BooleanTrue, GetTranslationListInfoList());
                yield return new TestCaseData(CurrentUserId, UidOne, EmptyUid, BooleanTrue, GetTranslationListInfoList());
            }
        }

        [TestCaseSource(nameof(ArgumentTestCases))]
        public void LabelTranslationCreateListRequest_Argument_Validations(long currentUserId, Guid organizationUid, Guid labelUid,
                                                                           bool updateExistedTranslations, List<TranslationListInfo> labelTranslations)
        {
            Assert.Throws<ArgumentException>(() => { new LabelTranslationCreateListRequest(currentUserId, organizationUid, labelUid, updateExistedTranslations, labelTranslations); });
        }
    }
}