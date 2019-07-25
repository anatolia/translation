using System;
using System.Collections;

using NUnit.Framework;
using Shouldly;

using Translation.Common.Models.Requests.Label;
using static Translation.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Tests.Common.Requests.Label
{
    public class LabelEditRequestTests
    {
        [Test]
        public void LabelEditRequest_Constructor()
        {
            var request = GetLabelEditRequest(CurrentUserId, UidOne, UidOne, StringOne, StringOne);

            request.CurrentUserId.ShouldBe(CurrentUserId);
            request.OrganizationUid.ShouldBe(UidOne);
            request.LabelUid.ShouldBe(UidOne);
            request.LabelKey.ShouldBe(StringOne);
            request.Description.ShouldBe(StringOne);
        }

        public static IEnumerable ArgumentTestCases
        {
            get
            {
                yield return new TestCaseData(CurrentUserId, EmptyUid, UidOne, StringOne, StringOne);
                yield return new TestCaseData(CurrentUserId, UidOne, EmptyUid, StringOne, StringOne);
                yield return new TestCaseData(CurrentUserId, UidOne, UidOne, EmptyString, StringOne);
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