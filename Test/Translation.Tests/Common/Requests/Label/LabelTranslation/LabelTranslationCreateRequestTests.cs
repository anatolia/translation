using System;
using System.Collections;

using NUnit.Framework;
using Shouldly;
using Translation.Common.Models.Requests.Label.LabelTranslation;
using static Translation.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;
namespace Translation.Tests.Common.Requests.Label.LabelTranslation
{
    public class LabelTranslationCreateRequestTests
    {
        [Test]
        public void LabelTranslationCreateRequest_Constructor()
        {
            var request = GetLabelTranslationCreateRequest(CurrentUserId, UidOne, UidTwo,
                                                           UidThree, StringOne);

            request.CurrentUserId.ShouldBe(CurrentUserId);
            request.OrganizationUid.ShouldBe(UidOne);
            request.LabelUid.ShouldBe(UidTwo);
            request.LanguageUid.ShouldBe(UidThree);
            request.LabelTranslation.ShouldBe(StringOne);
        }

        public static IEnumerable ArgumentTestCases
        {
            get
            {
                yield return new TestCaseData(CurrentUserId, EmptyUid, UidTwo, UidThree, StringOne);
                yield return new TestCaseData(CurrentUserId, UidOne, EmptyUid, UidThree, StringOne);
                yield return new TestCaseData(CurrentUserId, UidOne, UidTwo, EmptyUid, StringOne);
                yield return new TestCaseData(CurrentUserId, UidOne, UidTwo, UidThree, EmptyString);
            }
        }

        [TestCaseSource(nameof(ArgumentTestCases))]
        public void LabelTranslationCreateRequest_Argument_Validations(long currentUserId, Guid organizationUid, Guid labelUid, Guid languageUid, string labelTranslation)
        {
            Assert.Throws<ArgumentException>(() => { new LabelTranslationCreateRequest(currentUserId, organizationUid, labelUid, languageUid, labelTranslation); });
        }
    }
}