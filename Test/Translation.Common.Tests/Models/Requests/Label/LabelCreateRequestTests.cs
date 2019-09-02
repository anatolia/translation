using System;
using System.Collections;
using NUnit.Framework;
using Shouldly;
using Translation.Common.Models.Requests.Label;
using static Translation.Common.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Common.Tests.Models.Requests.Label
{
    public class LabelCreateRequestTests
    {
        [Test]
        public void LabelCreateRequest_Constructor()
        {
            var request = GetLabelCreateRequest(CurrentUserId, UidOne, UidTwo,
                                                StringOne, StringTwo, GuidArrayOne);

            request.CurrentUserId.ShouldBe(CurrentUserId);
            request.OrganizationUid.ShouldBe(UidOne);
            request.ProjectUid.ShouldBe(UidTwo);
            request.LabelKey.ShouldBe(StringOne);
            request.Description.ShouldBe(StringTwo);
            request.LanguageUids.ShouldBe(GuidArrayOne);
        }

        public static IEnumerable ArgumentTestCases
        {
            get
            {
                yield return new TestCaseData(CurrentUserId, EmptyUid, UidTwo, StringOne, StringTwo, GuidArrayOne);
                yield return new TestCaseData(CurrentUserId, UidOne, EmptyUid, StringOne, StringTwo, GuidArrayOne);
            }
        }

        [TestCaseSource(nameof(ArgumentTestCases))]
        public void LabelCreateRequest_Argument_Validations(long currentUserId, Guid organizationUid, Guid projectUid,
                                                            string labelKey, string description, Guid[] languageIsoCodes)
        {
            Assert.Throws<ArgumentException>(() => {new LabelCreateRequest(currentUserId, organizationUid, projectUid, labelKey, description, languageIsoCodes);
            });
        }
    }
}