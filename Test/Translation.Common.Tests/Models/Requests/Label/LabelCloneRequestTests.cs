using System;
using System.Collections;
using NUnit.Framework;
using Shouldly;
using Translation.Common.Models.Requests.Label;
using static Translation.Common.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Common.Tests.Models.Requests.Label
{
    public class LabelCloneRequestTests
    {
        [Test]
        public void LabelCloneRequest_Constructor()
        {
            var request = GetLabelCloneRequest(CurrentUserId, UidOne, UidTwo,
                UidOne, StringOne, StringTwo);

            request.CurrentUserId.ShouldBe(CurrentUserId);
            request.OrganizationUid.ShouldBe(UidOne);
            request.CloningLabelUid.ShouldBe(UidTwo);
            request.ProjectUid.ShouldBe(UidOne);
            request.LabelKey.ShouldBe(StringOne);
            request.Description.ShouldBe(StringTwo);
        }

        public static IEnumerable ArgumentTestCases
        {
            get
            {
                yield return new TestCaseData(CurrentUserId, EmptyUid, UidTwo, UidOne, StringOne, StringTwo);
                yield return new TestCaseData(CurrentUserId, UidOne, EmptyUid, UidOne, StringOne, StringTwo);
                yield return new TestCaseData(CurrentUserId, UidOne, UidTwo, EmptyUid, StringOne, StringTwo);
                yield return new TestCaseData(CurrentUserId, UidOne, UidTwo, UidOne, EmptyString, StringTwo);
            }
        }

        [TestCaseSource(nameof(ArgumentTestCases))]
        public void LabelCloneRequest_Argument_Validations(long currentUserId, Guid organizationUid, Guid cloningLabelUid, Guid projectUid,
                                                           string labelKey,string description)
        {
            Assert.Throws<ArgumentException>(() => { new LabelCloneRequest(currentUserId, organizationUid, cloningLabelUid, projectUid, labelKey, description); });
        }
    }
}