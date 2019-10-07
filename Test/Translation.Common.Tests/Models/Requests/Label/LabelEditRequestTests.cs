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
            var request = GetLabelEditRequest(CurrentUserId, UidOne, UidTwo, UidThree, StringOne, StringTwo);

            request.CurrentUserId.ShouldBe(CurrentUserId);
            request.OrganizationUid.ShouldBe(UidOne);
            request.ProjectUid.ShouldBe(UidTwo);
            request.LabelUid.ShouldBe(UidThree);
            request.LabelKey.ShouldBe(StringOne);
            request.Description.ShouldBe(StringTwo);
        }

        public static IEnumerable ArgumentTestCases
        {
            get
            {
                yield return new TestCaseData(CurrentUserId, EmptyUid, UidTwo, UidThree, StringOne, StringTwo);
                yield return new TestCaseData(CurrentUserId, UidOne, EmptyUid, UidThree, StringOne, StringTwo);
                yield return new TestCaseData(CurrentUserId, UidOne, UidTwo, EmptyUid, StringOne, StringTwo);
            }
        }

        [TestCaseSource(nameof(ArgumentTestCases))]
        public void LabelEditRequest_Argument_Validations(long currentUserId, Guid organizationUid, Guid projectUid, Guid labelUid,
                                                          string labelKey, string description)
        {
            Assert.Throws<ArgumentException>(() => { new LabelEditRequest(currentUserId, organizationUid, projectUid, labelUid, labelKey, description); });
        }
    }
}