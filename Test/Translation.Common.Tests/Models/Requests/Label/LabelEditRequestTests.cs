using System;
using System.Collections;
using NUnit.Framework;
using Shouldly;
using Translation.Common.Models.Requests.Label;
using static Translation.Common.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Common.Tests.Models.Requests.Label
{
    public class LabelEditRequestTests
    {
        [Test]
        public void LabelEditRequest_Constructor()
        {
            var request = GetLabelEditRequest(CurrentUserId, UidOne, UidTwo, StringOne, StringTwo);

            request.CurrentUserId.ShouldBe(CurrentUserId);
            request.OrganizationUid.ShouldBe(UidOne);
            request.LabelUid.ShouldBe(UidTwo);
            request.LabelKey.ShouldBe(StringOne);
            request.Description.ShouldBe(StringTwo);
        }

        public static IEnumerable ArgumentTestCases
        {
            get
            {
                yield return new TestCaseData(CurrentUserId, EmptyUid, UidTwo, StringOne, StringTwo);
                yield return new TestCaseData(CurrentUserId, UidOne, EmptyUid, StringOne, StringTwo);
                yield return new TestCaseData(CurrentUserId, UidOne, UidTwo, EmptyString, StringTwo);
            }
        }

        [TestCaseSource(nameof(ArgumentTestCases))]
        public void LabelEditRequest_Argument_Validations(long currentUserId, Guid organizationUid, Guid projectUid,
                                                          string labelKey, string description)
        {
            Assert.Throws<ArgumentException>(() => { new LabelEditRequest(currentUserId, organizationUid, projectUid, labelKey, description); });
        }
    }
}